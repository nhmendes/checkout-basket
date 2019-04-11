namespace BasketService.Presentation.WebApi.Configurations
{
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using Swashbuckle.AspNetCore.Swagger;
    using Swashbuckle.AspNetCore.SwaggerGen;

    [ExcludeFromCodeCoverage]
    internal sealed class SwaggerDefaultValues : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            foreach (var parameter in operation.Parameters.OfType<NonBodyParameter>())
            {
                var description = context.ApiDescription.ParameterDescriptions.FirstOrDefault(p => p.Name == parameter.Name);

                if (description != null)
                {
                    if (parameter.Description == null)
                    {
                        parameter.Description = description.ModelMetadata?.Description;
                    }

                    if (parameter.Default == null)
                    {
                        parameter.Default = description.RouteInfo?.DefaultValue;
                    }

                    parameter.Required |= !description.RouteInfo?.IsOptional ?? false;
                }
            }
        }
    }
}