using System.Runtime.Serialization;

namespace Sistema.Web.API.SwaggerExt.Models
{
    [DataContract(Namespace = "")]
    public partial class Modalidade
    {
        [DataMember]
        public long IdModalidade { get; set; }
        [DataMember]
        public string ModalidadeDescricao { get; set; }

    }
}