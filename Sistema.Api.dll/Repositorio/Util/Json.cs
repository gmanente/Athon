using System;
using System.Data;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Sistema.Api.dll.Repositorio.Util
{
    public class Json
    {
        public static string Serialize<T>(T value) where T : class
        {
            try
            {
                Type type = value.GetType();
                var json = new Newtonsoft.Json.JsonSerializer();

                //ignorando propriedades nulas
                json.NullValueHandling = NullValueHandling.Ignore;

                // ignorar referencias ciclicas em objetos
                json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                // formato da data, utilizar o iso
                json.DateFormatHandling = DateFormatHandling.IsoDateFormat;

                if (type == typeof(DataTable))
                    json.Converters.Add(new DataTableConverter());
                else if (type == typeof(DataSet))
                    json.Converters.Add(new DataSetConverter());

                var sw = new StringWriter();
                var writer = new JsonTextWriter(sw);

                writer.Formatting = Formatting.None;

                writer.QuoteChar = '"';
                json.Serialize(writer, value);

                string output = sw.ToString();
                writer.Close();
                sw.Close();

                return output;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static T DeSerialize<T>(string value) where T : class
        {
            try
            {
                var settings = new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.IsoDateFormat,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };

                // se alguma propriedade nao existir no objeto, ignorar
                return JsonConvert.DeserializeObject<T>(value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}