using Sistema.Api.dll.Src.Seguranca.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Api.dll.Src.Datanorte.VO
{
    public class ContatoHistoricoVO : AbstractVO
    {
        public int Contatos_id { get; set; }
        public string Observacao { get; set; }
        public DateTime Dataoperacao { get; set; }

        public string DataOperacaoString { get; set; }

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
    }
}
