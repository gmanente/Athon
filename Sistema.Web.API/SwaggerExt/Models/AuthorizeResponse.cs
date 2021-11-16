#pragma warning disable 1591
namespace Sistema.Web.API.SwaggerExt.Models
{
    public class AuthorizeResponse
    {
        public bool authenticated { get; set; }
        public string created { get; set; }
        public string expiration { get; set; }
        public string access_token { get; set; }
        public string message { get; set; }

    }
}