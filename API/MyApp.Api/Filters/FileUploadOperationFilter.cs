using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace MyApp.Api.Filters
{
    public class FileUploadOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var formFileContentType = "multipart/form-data";
            
            // Check if the operation has any IFormFile parameters
            if (context.MethodInfo.GetParameters().Any(p => p.ParameterType == typeof(IFormFile) || 
                (p.ParameterType.IsGenericType && p.ParameterType.GetGenericArguments()[0] == typeof(IFormFile))))
            {
                // Set the correct request content type
                operation.RequestBody = new OpenApiRequestBody
                {
                    Content = new Dictionary<string, OpenApiMediaType>
                    {
                        [formFileContentType] = new OpenApiMediaType
                        {
                            Schema = new OpenApiSchema
                            {
                                Type = "object",
                                Properties = context.MethodInfo.GetParameters()
                                    .Where(param => param.ParameterType == typeof(IFormFile) || param.ParameterType == typeof(string))
                                    .ToDictionary(
                                        param => param.Name,
                                        param => param.ParameterType == typeof(IFormFile)
                                            ? new OpenApiSchema
                                            {
                                                Type = "string",
                                                Format = "binary"
                                            }
                                            : new OpenApiSchema
                                            {
                                                Type = "string"
                                            }
                                    ),
                                Required = new HashSet<string>(
                                    context.MethodInfo.GetParameters()
                                        .Where(param => param.ParameterType == typeof(IFormFile) || param.ParameterType == typeof(string))
                                        .Select(param => param.Name)
                                )
                            }
                        }
                    }
                };

                // Remove form parameters as they're now in the request body
                var formParameters = operation.Parameters
                    .Where(p => context.MethodInfo.GetParameters()
                        .Any(mp => mp.Name == p.Name))
                    .ToList();

                foreach (var formParameter in formParameters)
                {
                    operation.Parameters.Remove(formParameter);
                }
            }
        }
    }
}