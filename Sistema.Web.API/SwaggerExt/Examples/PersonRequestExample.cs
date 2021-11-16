using Sistema.Web.API.SwaggerExt.Models;
using Swashbuckle.Examples;

namespace Sistema.Web.API.SwaggerExt.Examples
{
    internal class PersonRequestExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new PersonRequest
            {
                FirstName = "John"
            };
        }
    }
}