using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Description;

namespace Sistema.Web.API.SwaggerExt
{
    public class AddOperationConsumes : IOperationFilter
    {
        [AttributeUsage(AttributeTargets.Method)]
        public class AttributeConsumes : Attribute
        {
            public IEnumerable<string> ContentTypes { get; }

            public AttributeConsumes(params string[] contentTypes)
            {
                ContentTypes = contentTypes;
            }
        }

        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            var attribute = apiDescription.GetControllerAndActionAttributes<AttributeConsumes>().SingleOrDefault();
            if (attribute == null)
                return;

            operation.consumes.Clear();
            operation.consumes = attribute.ContentTypes.ToList();
        }
    }
}