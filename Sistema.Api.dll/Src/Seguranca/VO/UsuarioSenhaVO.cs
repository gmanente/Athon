namespace Sistema.Api.dll.Src.Seguranca.VO
{
    [System.Serializable]
    public class UsuarioSenhaVO : AbstractVO
    {
        public long IdUsuario { get; set; }
        public string Senha { get; set; }
        public System.DateTime? DataCadastro { get; set; }
        public System.DateTime? DataTermino { get; set; }

    }
}