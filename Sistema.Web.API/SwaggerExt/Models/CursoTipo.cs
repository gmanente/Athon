using System.Runtime.Serialization;

namespace Sistema.Web.API.SwaggerExt.Models
{
    [DataContract(Namespace = "")]
    public partial class CursoTipo
    {
        [DataMember]
        public long IdCursoTipo { get; set; }
        [DataMember]
        public string CursoTipoDescricao { get; set; }

    }
}