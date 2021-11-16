using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;

namespace Sistema.Web.API.SwaggerExt
{
    public class BrowserJsonFormatter : JsonMediaTypeFormatter
    {
        public BrowserJsonFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

            // Resolve Json settings
            SerializerSettings =
                new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    NullValueHandling = NullValueHandling.Ignore,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    DateFormatHandling = DateFormatHandling.IsoDateFormat,
                    Converters = new[] { new StringEnumConverter() }
                };
        }

        public override void SetDefaultContentHeaders(Type type, HttpContentHeaders headers, MediaTypeHeaderValue mediaType)
        {
            base.SetDefaultContentHeaders(type, headers, mediaType);
            headers.ContentType = new MediaTypeHeaderValue("application/json");
        }
    }
}