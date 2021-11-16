using System;

namespace Sistema.Api.dll.Src.Seguranca.VO
{
    [Serializable]
    public class UsuarioVO : AbstractVO
    {
        public DateTime? DataCadastro { get; set; }

        public DateTime DataAtual = DateTime.Now;

        public string NomeLogin { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public bool? Ativo { get; set; }
        public string Foto { get; set; }
        public string ListaDepartamentoOperar { get; set; }
        public string StrConsulta { get; set; }
        public long MoodleUserId { get; set; }
        public string MoodleUsername { get; set; }
        public string MoodleName { get; set; }


        private UsuarioSenhaVO usuarioSenha;

        public UsuarioSenhaVO UsuarioSenha
        {
            set
            {
                usuarioSenha = value;
            }
            get
            {
                if (usuarioSenha == null && IsInstantiable())
                    usuarioSenha = new UsuarioSenhaVO();

                return usuarioSenha;
            }
        }


        private UsuarioDepartamentoVO usuarioDepartamento;

        public UsuarioDepartamentoVO UsuarioDepartamento
        {
            set
            {
                usuarioDepartamento = value;
            }
            get
            {
                if (usuarioDepartamento == null && IsInstantiable())
                    usuarioDepartamento = new UsuarioDepartamentoVO();

                return usuarioDepartamento;
            }
        }


        private UsuarioCampusVO usuarioCampus;

        public UsuarioCampusVO UsuarioCampus
        {
            set
            {
                usuarioCampus = value;
            }
            get
            {
                if (usuarioCampus == null && IsInstantiable())
                    usuarioCampus = new UsuarioCampusVO();

                return usuarioCampus;
            }
        }


        private UsuarioPerfilVO usuarioPerfil;

        public UsuarioPerfilVO UsuarioPerfil
        {
            set
            {
                usuarioPerfil = value;
            }
            get
            {
                if (usuarioPerfil == null && IsInstantiable())
                    usuarioPerfil = new UsuarioPerfilVO();

                return usuarioPerfil;
            }
        }
    }
}