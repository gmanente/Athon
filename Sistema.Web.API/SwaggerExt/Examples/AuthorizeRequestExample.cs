using Sistema.Web.API.SwaggerExt.Models;
using Swashbuckle.Examples;

namespace Sistema.Web.API.SwaggerExt.Examples
{
    internal class AuthorizeRequestExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new AuthorizeRequest
            {
                Login = "SEU_CPF",
                Senha = "SUA_SENHA"
            };
        }
    }
}