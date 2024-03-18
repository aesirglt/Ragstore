namespace Totten.Solution.Ragstore.WebApi.Behaviors;

using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
/// <summary>
/// 
/// </summary>
public class RemoveAntiforgeryHeaderOperationFilter : IOperationFilter
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="operation"></param>
    /// <param name="context"></param>
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var filterPipeline = context.ApiDescription.ActionDescriptor.FilterDescriptors;
        var hasAntiforgeryFilter = filterPipeline
            .Select(filterInfo => filterInfo.Filter)
            .Any(filter => filter is AutoValidateAntiforgeryTokenAttribute);

        if (hasAntiforgeryFilter)
        {
            List<OpenApiParameter> parameters = new();

            for (var i = 0; i < operation.Parameters.Count; i++)
            {
                var param = operation.Parameters[i];

                if (param.Name == "__RequestVerificationToken")
                    parameters.Add(param);
            }

            foreach (var param in parameters)
                operation.Parameters.Remove(param);
        }
    }
}