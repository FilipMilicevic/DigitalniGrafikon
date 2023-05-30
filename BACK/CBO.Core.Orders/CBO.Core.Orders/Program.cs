using CBO.Common.API.CoreServiceServer;
using CBO.Core.Orders.API.Helpers;
using CBO.Core.Orders.DataAccess;
using CBO.Core.Orders.DataAccess.Csc;
using CBO.Core.Orders.Service;
using FluentValidation;
using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using OpenIddict.Abstractions;
using OpenIddict.Server;
using OpenIddict.Validation.AspNetCore;
using Serilog;
using System.Reflection;
using System.Text.Json.Serialization;
using CBO.Common.API.Configuration;

try
{
    var builder = WebApplication.CreateBuilder(args);

    //var logger = new LoggerConfiguration()
    //    .WriteTo.Console()
    //    .ReadFrom.Configuration(builder.Configuration)
    //    .Enrich.FromLogContext()
    //    .CreateLogger();

    //builder.Logging.ClearProviders();
    //builder.Logging.AddSerilog(logger);
    //Log.Logger = logger;

    var assembly = Assembly.GetExecutingAssembly().GetName();
    Log.Information("Starting {name} application version {version}", assembly.Name, assembly.Version);

    builder.Services
        .AddControllers()
        .AddApplicationPart(Assembly.Load(new AssemblyName("CBO.Common.API")))
        .AddJsonOptions
        (
            options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            }
        );

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen
    (
        options =>
        {
            options.SupportNonNullableReferenceTypes();
            options.UseAllOfToExtendReferenceSchemas();
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
       service => new CBO.Common.CboContext(CBO.Common.Host.Kompas, "Orders API")
    );

    builder.Services.AddScoped<IOrdersDataAccess>
    (
        service => new CscOrdersDataAccess(builder.Configuration)
    );

    builder.Services.AddScoped<IOrdersManagementService>
    (
       service => new OrdersManagementService(service.GetRequiredService<IOrdersManagementService>())
    );


    builder.Services
        .AddOpenIddict()
        .AddServer(options =>
        {
            //#if DEBUG
            options.AddEphemeralEncryptionKey()
                   .AddEphemeralSigningKey()
                   //#else
                   //                    options.AddEncryptionCertificate(certificate)
                   //                           .AddSigningCertificate(certificate)
                   //#endif
                   .EnableDegradedMode()
                   .DisableTokenStorage()
                   .UseAspNetCore();

            options.SetTokenEndpointUris("token")
                   .AllowClientCredentialsFlow()
                   .DisableAccessTokenEncryption()
                   .AllowRefreshTokenFlow()
                   .DisableSlidingRefreshTokenExpiration()
                   .RegisterScopes
                    (
                        new[]
                        {
                            OpenIddictConstants.Scopes.OfflineAccess,
                            "accounts:read",
                            "accounts:write"
                        }
                    )
                   .AddEventHandler<OpenIddictServerEvents.ValidateTokenRequestContext>
                    (
                        b => b.UseScopedHandler<ValidateTokenRequestHandler>()
                    )
                    .AddEventHandler<OpenIddictServerEvents.HandleTokenRequestContext>
                    (
                        b => b.UseScopedHandler<HandleTokenRequestHandler>()
                    )
                    .AddEventHandler<OpenIddictServerEvents.ApplyTokenResponseContext>
                    (
                        b => b.UseScopedHandler<ApplyTokenResponseHandler>()
                    );

            var authSettings = builder.Configuration.GetSection("Auth").Get<AuthConfiguration>();
            if (authSettings != null)
            {
                options
                   .SetAccessTokenLifetime(new TimeSpan(0, 0, authSettings.AccessTokenLifetime))
                   .SetRefreshTokenLifetime(new TimeSpan(0, 0, authSettings.RefreshTokenLifetime));
            }

        })
        .AddValidation
        (
            options =>
            {
                options.UseLocalServer();
                options.UseAspNetCore();
            }
        );

    builder.Services.AddAuthentication(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);


    builder.Services.AddSwaggerGen
    (
        options =>
        {
            options.SwaggerDoc("v1",
                new OpenApiInfo
                {
                    Title = "kompas Orders Management API",
                    Version = "v1"
                }
            );

            options.MapType<decimal>(() => new OpenApiSchema { Type = "number", Format = "decimal" });

            options.SwaggerGeneratorOptions.SortKeySelector = (apiDesc) => apiDesc.GroupName;

            options.CustomSchemaIds(x => x.FullName);

            options.CustomOperationIds(CoreCboServiceHelper.OperationIdSelector);

            options.AddSecurityDefinition("Bearer",
                new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Flows = new OpenApiOAuthFlows
                    {
                        ClientCredentials = new OpenApiOAuthFlow
                        {
                            TokenUrl = new Uri("/token", UriKind.Relative),
                            Scopes = new Dictionary<string, string>
                            {
                                { "accounts:read", string.Empty },
                                { "accounts:write", string.Empty }
                            }
                        }
                    },
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer",
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
        }
    );

    var app = builder.Build();

    app.UseHttpsRedirection();
    app.UseRouting();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();

    //if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

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
