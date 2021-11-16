namespace Sistema.Api.dll.Repositorio.Util
{
    public class SessaoSistema
    {
        public long IdUsuario { get; set; }
        public long IdUsuarioCampus { get; set; }
        public string NomeUsuario { get; set; }
        public string LoginNome { get; set; }
        public string EmailUsuario { get; set; }
        public long IdCampus { get; set; }
        public string NomeCampus { get; set; }
        public bool AcessoExterno { get; set; }
        public string IdsCampus { get; set; }
        public long IdAuditoria { get; set; }
        public long IdProfessor { get; set; }
        public long IdUsuarioModulo { get; set; }
        public long IdModulo { get; set; }
        public long IdSubModulo { get; set; }
        public string HostName { get; set; }
        public bool Portal { get; set; }
        public bool TrocarSenhaPadrao { get; set; }
    }
}