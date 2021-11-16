﻿using Swashbuckle.Swagger;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace Sistema.Web.API.SwaggerExt
{
    public class AddOperationSecurity : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            // Correspond each "Authorize" role to an oauth2 scope
            var scopes = apiDescription.ActionDescriptor.GetFilterPipeline()
                .Select(filterInfo => filterInfo.Instance)
                .OfType<AuthorizeAttribute>()
                .SelectMany(attr => attr.Roles.Split(','))
                .Distinct();

            if (scopes.Any())
            {
                if (operation.security == null)
                    operation.security = new List<IDictionary<string, IEnumerable<string>>>();

                var oAuthRequirements = new Dictionary<string, IEnumerable<string>>
                {
                    { "oauth2", scopes }
                };

                operation.security.Add(oAuthRequirements);
            }
        }
    }
}