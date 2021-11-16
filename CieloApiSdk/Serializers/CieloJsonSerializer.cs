using Cielo.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RestSharp.Serializers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cielo.Serializers
{
    /// <summary>
    /// Serealização Padrão JSON para request bodies
    /// Doesn't currently use the SerializeAs attribute, defers to Newtonsoft's attributes
    /// </summary>
    internal class CieloJsonSerializer : ISerializer
    {
        protected Newtonsoft.Json.JsonSerializer Serializer { get; set; }

        /// <summary>
        /// Serialização padrão
        /// </summary>
        public CieloJsonSerializer()
        {
            ContentType = "application/json";

            Serializer = Newtonsoft.Json.JsonSerializer.Create();

            Serializer.NullValueHandling = NullValueHandling.Ignore;
        }

        /// <summary>
        /// Serialização padrão sobrescrevendo para permitir a configuração personalizada do Json.NET
        /// </summary>
        public CieloJsonSerializer(Newtonsoft.Json.JsonSerializer serializer)
        {
            ContentType = "application/json";
            Serializer = serializer;
        }

        /// <summary>
        /// Unused for JSON Serialization
        /// </summary>
        public virtual string DateFormat { get; set; }

        /// <summary>
        /// Unused for JSON Serialization
        /// </summary>
        public virtual string RootElement { get; set; }

        /// <summary>
        /// Unused for JSON Serialization
        /// </summary>
        public virtual string Namespace { get; set; }

        /// <summary>
        /// Tipo de Conteúdo para serialização
        /// </summary>
        public virtual string ContentType { get; set; }


        /// <summary>
        /// Serializa o objeto para JSON
        /// </summary>
        /// <param name="obj">Objeto a serializar</param>
        /// <returns>JSON como String</returns>
        public virtual string Serialize(object obj)
        {
            using (var stringWriter = new StringWriter())
            using (var jsonTextWriter = new JsonTextWriter(stringWriter))
            {
#if DEBUG
                jsonTextWriter.Formatting = Formatting.Indented;
#endif

                Serializer.Serialize(jsonTextWriter, obj);

                return stringWriter.ToString();
            }
        }
    }
}
