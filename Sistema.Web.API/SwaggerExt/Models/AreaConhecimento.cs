using System.Runtime.Serialization;

namespace Sistema.Web.API.SwaggerExt.Models
{
    [DataContract(Namespace = "")]
    public partial class AreaConhecimento
    {
        [DataMember]
        public long IdGpa { get; set; }
        [DataMember]
        public string GpaDescricao { get; set; }
        [DataMember]
        public string GpaSigla { get; set; }

        [DataMember]
        public long IdDiretor { get; set; }
        //[DataMember]
        //public string DiretorNome { get; set; }

    }
}