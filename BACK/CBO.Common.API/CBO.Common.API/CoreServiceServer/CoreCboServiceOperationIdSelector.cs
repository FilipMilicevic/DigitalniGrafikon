using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace CBO.Common.API.CoreServiceServer
{
    public static class CoreCboServiceHelper
    {
        public static string OperationIdSelector(ApiDescription apiDescription)
        {
            var operation = apiDescription.ActionDescriptor;

            string? operationId = operation.AttributeRouteInfo?.Name ?? operation.Id;

            if (operation is ControllerActionDescriptor controllerAction)
            {
                string? method = apiDescription.HttpMethod;
                if (method != null)
                {
                    method = $"{method[0].ToString().ToUpper()}{method.Substring(1).ToLower()}";
                }

                string action = controllerAction.MethodInfo.Name;

                //string controller = controllerActionDescriptor.ControllerName;
                //if (controller != null)
                //{
                //    // all controllers will be named XyzController by convention
                //    controller = controller.Replace("Controller", String.Empty);
                //}

                // special case: Data Source Requests POSTed but retrieve data: POST XyzsList GetXyzs
                if (method == "Post" && action.EndsWith("List"))
                {
                    method = "Get";
                    action = action[0..^4];
                }

                // special case: POST XyzAdd for adding a new entry -> AddXyz
                if (method == "Post" && action.EndsWith("Add"))
                {
                    method = "Add";
                    action = action[0..^3];
                }

                // special case: POST XyzUpdate for updating a new entry -> UpdateXyz
                if (method == "Post" && action.EndsWith("Update"))
                {
                    method = "Update";
                    action = action[0..^6];
                }

                // special case: POST xyzToAbcExport -> ExportXyzToAbc
                if (method == "Post" && action.EndsWith("Export"))
                {
                    method = "Export";
                    action = action[0..^6];
                }

                //desc = $"{method}{controller}{action}";
                operationId = $"{method}{action}";
            }

            return operationId;
        }
    }
}
