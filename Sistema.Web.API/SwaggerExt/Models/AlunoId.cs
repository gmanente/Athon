using System.Runtime.Serialization;

namespace Sistema.Web.API.SwaggerExt.Models
{
    [DataContract(Namespace = "")]
    public partial class AlunoId
    {
        [DataMember]
        public long IdAluno { get; set; }

    }
}