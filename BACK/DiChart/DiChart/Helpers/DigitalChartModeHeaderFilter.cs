using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CBO.DigitalChart.API.Helpers
{
    public class DigitalChartModeHeaderFilter : IOperationFilter
    {
        public const string DigitalChartHeader = "X-DigitalChart-AppMode";

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
            {
                operation.Parameters = new List<OpenApiParameter>();
            }

            operation.Parameters.Add
            (
                new OpenApiParameter
                {
                    Name = DigitalChartHeader,
                    In = ParameterLocation.Header,
                    Description = "DigitalChart Application Mode",
                    Examples =
                    {
                        {
                            "Office",
                            new OpenApiExample
                            {
                                Value = new OpenApiString("Office")
                            }
                        },
                        {
                            "On point",
                            new OpenApiExample
                            {
                                Value = new OpenApiString("OnPoint")
                            }
                        }
                    },
                    Required = false,
                    Schema = new OpenApiSchema
                    {
                        Type = "string",
                        Nullable = true,
                        Default = new OpenApiString("Office")
                    }
                }
            );
        }
    }
}
