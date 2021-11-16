using Sistema.Api.dll.Src.Seguranca.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Api.dll.Src.Datanorte.VO
{
    [System.Serializable]
    public class Contato_LancamentoVO : AbstractVO
    {
        public int Contato_Lancamento_Id { get; set; }
        
        public int Contato_Id  { get; set; }
        
        public DateTime DataOperacao  { get; set; }

        private ContatoVO contato;
        
        public ContatoVO Contato
        {
            set
            {
                contato = value;
            }

            get
            {
                if (contato == null && IsInstantiable())
                    contato = new ContatoVO();

                return contato;
            }
        }

        private UsuarioVO usuario;
        
        public UsuarioVO Usuario
        {
            set
            {
                usuario = value;
            }

            get
            {
                if (usuario == null && IsInstantiable())
                    usuario = new UsuarioVO();

                return usuario;
            }
        }

        public string Observacao { get; set; }
    }
}
