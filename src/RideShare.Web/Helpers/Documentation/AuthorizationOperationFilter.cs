using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RideShare.Web.Helpers.Documentation
{
    public class AuthorizationOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (context.ApiDescription.ActionDescriptor.EndpointMetadata.FirstOrDefault(x => x is AuthorizeAttribute) is AuthorizeAttribute filter)
            {
                operation.Security = (filter.AuthenticationSchemes ?? filter.Policy).Split(',')
                    .Select(s => new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = s,
                                }
                            },
                            new string[] { }
                        }
                    })
                    .ToList();
            }
        }
    }
}
