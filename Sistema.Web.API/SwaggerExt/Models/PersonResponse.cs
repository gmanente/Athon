using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Sistema.Web.API.SwaggerExt.Models
{
    [DataContract]
    public class PersonResponse
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public Title Title { get; set; }

        [DataMember(Name = "First")]
        [Description("The first name of the person")]
        public string FirstName { get; set; }

        [JsonProperty("Last")]
        [Description("The last name of the person")]
        public string LastName { get; set; }

    }
}