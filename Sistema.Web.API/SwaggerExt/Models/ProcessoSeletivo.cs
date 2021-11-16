using System;
using System.Runtime.Serialization;

namespace Sistema.Web.API.SwaggerExt.Models
{
    [DataContract(Namespace = "")]
    public partial class ProcessoSeletivo
    {
        [DataMember]
        public long IdCampus { get; set; }
        [DataMember]
        public long IdProcessoSeletivo { get; set; }
        [DataMember]
        public string ProcessoSeletivoDescricao { get; set; }
        [DataMember]
        public DateTime? DataProva { get; set; }

    }
}