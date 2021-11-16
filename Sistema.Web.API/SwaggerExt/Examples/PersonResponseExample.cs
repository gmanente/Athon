using Sistema.Web.API.SwaggerExt.Models;
using Swashbuckle.Examples;

namespace Sistema.Web.API.SwaggerExt.Examples
{
    internal class PersonResponseExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new PersonResponse
            {
                Title = Title.Mr,
                FirstName = "John",
                LastName = "Sharp"
            };
        }
    }
}