using Sistema.Api.dll.Src.Comum.VO;
using Sistema.Api.dll.Src.Seguranca.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Api.dll.Src.Productive.VO
{
    public class RevendaVO : AbstractVO
    {

        public string NomeRevenda { get; set; }
        public string NomeContato { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public string Observacao { get; set; }
        public DateTime DataInclusao  { get; set; }
        public DateTime DataAlteracao { get; set; }
        public bool Ativar { get; set; }

        private UsuarioVO usuario { get; set; }
        public UsuarioVO Usuario
        {
            set { usuario = value; }
            get
            {
                if (usuario == null && IsInstantiable())
                    usuario = new UsuarioVO();

                return usuario;
            }
        }
        private CidadeVO cidade { get; set; }
        public CidadeVO Cidade
        {
            set { cidade = value; }
            get
            {
                if (cidade == null && IsInstantiable())
                    cidade = new CidadeVO();

                return cidade;
            }
        }

        private UsuarioVO usuarioInclusao { get; set; }
        public UsuarioVO UsuarioInclusao
        {
            set { usuarioInclusao = value; }
            get
            {
                if (usuarioInclusao == null && IsInstantiable())
                    usuarioInclusao = new UsuarioVO();

                return usuarioInclusao;
            }
        }
        private UsuarioVO usuarioAlteracao { get; set; }
        public UsuarioVO UsuarioAlteracao
        {
            set { usuarioAlteracao = value; }
            get
            {
                if (usuarioAlteracao == null && IsInstantiable())
                    usuarioAlteracao = new UsuarioVO();

                return usuarioAlteracao;
            }
        }

        public RevendaVO()
        {
            NomeTabela = "DBAthon.dbo.Revenda";
        }
    }
}