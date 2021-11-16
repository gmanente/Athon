using System;

namespace Sistema.Web.UI.PortalSistema.Util
{
    [Serializable]
    public class SessionPortalSistema
    {
        public long IdUsuario { get; set; }
        public string NomeUsuario { get; set; }
        public string EmailUsuario { get; set; }
        public long IdAuditoria { get; set; }
        public bool AcessoExterno { get; set; }
        public string IdsCampus { get; set; }
        public long IdProfessor { get; set; }
        public long IdCampus { get; set; }
        public string HostName { get; set; }
        public bool Portal { get; set; }

        public long IdModuloLogado { get; set; }
        public long IdModuloNormal { get; set; }
        public long IdModuloMedicina { get; set; }
    }
}