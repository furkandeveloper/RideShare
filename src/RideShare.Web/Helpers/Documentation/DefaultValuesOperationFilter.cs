﻿using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RideShare.Web.Helpers.Documentation
{
    public class DefaultValuesOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
            {
                return;
            }
            foreach (var parameter in operation.Parameters)
            {
                var description = context.ApiDescription.ParameterDescriptions.FirstOrDefault(p => p.Name == parameter.Name);
                var routeInfo = description?.RouteInfo;

                if (parameter.Description == null)
                {
                    parameter.Description = description?.ModelMetadata?.Description;
                }

                if (routeInfo == null)
                {
                    continue;
                }

                if (parameter.Schema?.Default == null)
                {
                    parameter.Schema.Default = new OpenApiString(routeInfo.DefaultValue?.ToString());
                }

                parameter.Required |= !routeInfo.IsOptional;
            }
        }
    }
}
