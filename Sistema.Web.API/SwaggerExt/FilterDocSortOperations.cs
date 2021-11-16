using Swashbuckle.Swagger;
using System.Linq;
using System.Web.Http.Description;

namespace Sistema.Web.API.SwaggerExt
{
    public class FilterDocSortOperations : IDocumentFilter
    {
        /// <summary>
        /// Applies an ordering to the operations based on the path name of the operation.
        /// </summary>
        public void Apply(SwaggerDocument swaggerDoc, SchemaRegistry schemaRegistry, IApiExplorer apiExplorer)
        {
            // make operations a-z based on the controller name, then the path details
            var paths = swaggerDoc.paths.OrderBy(e => this.GetOperationFilterString(e.Key, e.Value)).ToList();

            swaggerDoc.paths = paths.ToDictionary(e => e.Key, e => e.Value);
        }

        /// <summary>
        /// Gets a string value that represents the name of an operation filter based on logic we will use for sorting.
        ///     All operations should be sorted by the group by name/controller name, then by the operation path (which is the
        ///     default)
        /// </summary>
        private string GetOperationFilterString(string key, PathItem value)
        {
            var groupByName = string.Empty;

            // grab the controller name or group by name from the first tag, if we have one
            if (value.get?.tags != null)
            {
                groupByName = value.get.tags[0];
            }
            else if (value.delete?.tags != null)
            {
                groupByName = value.delete.tags[0];
            }
            else if (value.patch?.tags != null)
            {
                groupByName = value.patch.tags[0];
            }
            else if (value.post?.tags != null)
            {
                groupByName = value.post.tags[0];
            }
            else if (value.put?.tags != null)
            {
                groupByName = value.put.tags[0];
            }

            // check if we have anything
            if (!string.IsNullOrWhiteSpace(groupByName))
            {
                return groupByName + '-' + key;
            }

            // we reach here if we have nothing, which we shouldn't, unless there is another http verb we need to catch above..
            return value.ToString();
        }
    }
}