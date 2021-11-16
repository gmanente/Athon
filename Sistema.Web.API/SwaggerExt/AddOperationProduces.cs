using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Description;

namespace Sistema.Web.API.SwaggerExt
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AttributeProduces : Attribute
    {
        public IEnumerable<string> ContentTypes { get; }

        public AttributeProduces(params string[] contentTypes)
        {
            ContentTypes = contentTypes;
        }
    }

    public class AddOperationProduces : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            var attribute = apiDescription.GetControllerAndActionAttributes<AttributeProduces>().SingleOrDefault();
            if (attribute == null)
                return;

            operation.produces.Clear();
            operation.produces = attribute.ContentTypes.ToList();
        }
    }
}