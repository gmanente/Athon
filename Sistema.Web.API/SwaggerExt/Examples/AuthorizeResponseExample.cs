using Sistema.Web.API.SwaggerExt.Models;
using Swashbuckle.Examples;

namespace Sistema.Web.API.SwaggerExt.Examples
{
    internal class AuthorizeResponseExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new AuthorizeResponse
            {
                authenticated = true,
                created = "2018-02-01 00:00:00",
                expiration = "2018-02-01 00:02:00",
                access_token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJqdGkiOiJqd3QtaWQtZDY2OWE5NGYtMDM1Yy00ZDg4LTlhYmEtYWZiOTQ3YWI5ZWFiIiwiYXVkIjoiaHR0cDovL2xvY2FsaG9zdDo2MjI5OCIsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6NjIyOTgiLCJpYXQiOjE1MjIwOTc4NDIuMCwiZXhwIjoxNTIyMDk3ODYyLjAsIm5iZiI6MTUyMjA5Nzg0Mi4wLCJzdWIiOiIwMzMzNjg3NDkzOSIsInVzZXIiOiJHZXJtYW5vIE1hbmVudGUgTmV0byAiLCJlbWFpbCI6Imdlcm1hbm9AdW5pdmFnLmVkdS5iciJ9.wX2WFNhmFoXXOhvH-z5XanUDzaEgR7u82VfS8osfW7c",
                message = "OK"
            };
        }
    }
}