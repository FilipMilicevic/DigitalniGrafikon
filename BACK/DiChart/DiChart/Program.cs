using CBO.DigitalChart.API;
using CBO.DigitalChart.API.Configuration;
using CBO.DigitalChart.API.Helpers;
using FluentValidation;
using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using OpenIddict.Abstractions;
using OpenIddict.Server;
using OpenIddict.Validation.AspNetCore;
using Serilog;
using System.Reflection;
using System.Text.Json.Serialization;
using Enums = CBO.DigitalChart.API.Models.Enums;

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    var logger = new LoggerConfiguration().WriteTo.Console().ReadFrom.Configuration(builder.Configuration).Enrich.FromLogContext().CreateLogger();
    builder.Logging.ClearProviders();

    builder.Logging.AddSerilog(logger);
    Log.Logger = logger;

    var assembly = Assembly.GetExecutingAssembly().GetName();
    Log.Information("Starting {name} application version {version}", assembly.Name, assembly.Version);

    builder.Services.AddDistributedMemoryCache();



    builder.Services.AddControllers()
        .AddJsonOptions
        (
            options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                }
        );

    builder.Services.AddEndpointsApiExplorer();


    var cboConfig = builder.Configuration.GetSection("CboCore").Get<CboCoreConfiguration>();
    var crmConfig = builder.Configuration.GetSection("Authorization").Get<ClientAuthorizationConfiguration>();



    string accountsTokenEndpoint = $"{cboConfig!.DrivingChartApiUrl}token";

    builder.Services.AddClientCredentialsTokenManagement()
        .AddClient("accounts.client", client =>
        {
            client.TokenEndpoint = accountsTokenEndpoint;
            client.ClientId = crmConfig!.ClientId;
            client.ClientSecret = crmConfig.ClientSecret;
        }
        );

    //builder.Services.AddHttpClient<IAccountsApiClient, AccountsApiClient>(client =>
    //{
    //    client.BaseAddress = new Uri(cboConfig.AccountsApiUrl);
    //}
    //)
    //.AddClientCredentialsTokenHandler("accounts.client");


    //builder.Services
    //   .AddOpenIddict()
    //   .AddServer(options =>
    //   {
    //       //#if DEBUG
    //       options.AddEphemeralEncryptionKey()
    //              .AddEphemeralSigningKey()
    //              //#else
    //              //                    options.AddEncryptionCertificate(certificate)
    //              //                           .AddSigningCertificate(certificate)
    //              //#endif
    //              .EnableDegradedMode()
    //              .DisableTokenStorage()
    //              .UseAspNetCore();


    //       //var authSettings = builder.Configuration.GetSection("Auth").Get<AuthConfiguration>();

    //       //options.SetTokenEndpointUris("token")
    //       //       .AllowPasswordFlow()
    //       //       .SetAccessTokenLifetime(new TimeSpan(0, 0, authSettings.AccessTokenLifetime))
    //       //       .AllowRefreshTokenFlow()
    //       //       .SetRefreshTokenLifetime(new TimeSpan(0, 0, authSettings.RefreshTokenLifetime))
    //       //       .DisableSlidingRefreshTokenExpiration()
    //       //       .RegisterScopes
    //       //        (
    //       //            new[]
    //       //            {
    //       //                 OpenIddictConstants.Scopes.OfflineAccess
    //       //            }
    //       //        )
    //       //       .AddEventHandler<OpenIddictServerEvents.ValidateTokenRequestContext>
    //       //        (
    //       //            b => b.UseScopedHandler<Helpers.ValidateTokenRequestHandler>()
    //       //        )
    //       //        .AddEventHandler<OpenIddictServerEvents.HandleTokenRequestContext>
    //       //        (
    //       //            b => b.UseScopedHandler<Helpers.HandleTokenRequestHandler>()
    //       //        )
    //       //        .AddEventHandler<OpenIddictServerEvents.ApplyTokenResponseContext>
    //       //        (
    //       //            b => b.UseScopedHandler<Helpers.ApplyTokenResponseHandler>()
    //       //        );
    //   })
    //   .AddValidation
    //   (
    //       options =>
    //       {
    //           options.UseLocalServer();
    //           options.UseAspNetCore();
    //       }
    //   );

    //builder.Services.AddAuthentication(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);

    builder.Services.AddSwaggerGen
(
    options =>
    {
        //options.CustomSchemaIds(x => x.FullName);
        options.SwaggerDoc("v1",
            new OpenApiInfo
            {
                Title = "Digital Chart CBO API",
                Version = "v1"
            }
        );

        options.SwaggerGeneratorOptions.SortKeySelector = (apiDesc) => apiDesc.GroupName;

        options.AddSecurityDefinition("Bearer",
            new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                In = ParameterLocation.Header,
                Name = "Authorization",
                Flows = new OpenApiOAuthFlows
                {
                    Password = new OpenApiOAuthFlow
                    {
                        TokenUrl = new Uri("/token", UriKind.Relative),
                    }
                },
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            }
        );

        options.AddSecurityRequirement
        (
            new OpenApiSecurityRequirement
            {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
            }
        );

        options.OperationFilter<DigitalChartModeHeaderFilter>();
    }
    );

    var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<List<string>>();

    builder.Services.AddCors
    (
        options =>
        {
            options.AddPolicy
            (
                "CorsPolicy",
                builder =>
                {
                    builder.AllowAnyHeader().AllowAnyMethod();

                    if (allowedOrigins.Any(h => h == "*"))
                    {
                        builder.AllowAnyOrigin();
                    }
                    else
                    {
                        builder.WithOrigins(allowedOrigins.ToArray()).AllowCredentials();
                    }

                    builder.WithExposedHeaders("Content-Disposition");
                }
            );
        }
    );

    builder.Services.AddFluentValidationAutoValidation();
    builder.Services.AddFluentValidationClientsideAdapters();

    builder.Services.AddValidatorsFromAssemblyContaining(typeof(Program));
    //builder.Services.AddFluentValidation(x => { x.RegisterValidatorsFromAssemblyContaining<Program>(); });

    builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

    builder.Services.AddFluentValidationRulesToSwagger();

    builder.Services.AddSingleton
    (
        service => new CboContext(Enums.Host.DigitalChart, "TollCRM Application")
    );

    builder.Services.AddHttpContextAccessor();

    var app = builder.Build();

    app.UseHttpsRedirection();

    //app.UseSerilogRequestLogging();

    app.UseRouting();

    app.UseCors("CorsPolicy");

    app.UseAuthentication();
    app.UseAuthorization();

    app.UseEndpoints
(
    endpoints =>
    {
        endpoints.MapControllers();
    }
);

    //if (app.Environment.IsDevelopment())
    //{
    app.UseSwagger();
    app.UseSwaggerUI
    (
        c =>
        {
            c.OAuthClientId("digitalchart-api-ui");
        }
    );
    //// Configure the HTTP request pipeline.
    //if (app.Environment.IsDevelopment())
    //{
    //    app.UseSwagger();
    //    app.UseSwaggerUI();
    //}

    app.Run();

}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shutting down.");
    Log.CloseAndFlush();
}
