using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Datanorte.DAO;
using Sistema.Api.dll.Src.Datanorte.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Api.dll.Src.Datanorte.BE
{
    public class ContatoBE : AbstractBE, IBE<ContatoVO>
    {
        public long Alterar(ContatoVO objVO, string where = null)
        {
            throw new NotImplementedException();
        }

        public ContatoVO Consultar(ContatoVO objVO)
        {
            throw new NotImplementedException();
        }

        public void Deletar(ContatoVO objVO)
        {
            throw new NotImplementedException();
        }

        public long Inserir(ContatoVO objVO)
        {
            throw new NotImplementedException();
        }

        public List<ContatoVO> Listar(ContatoVO objVO, bool detalhar = false)
        {
            throw new NotImplementedException();
        }

        public List<ContatoVO> Selecionar(ContatoVO objVO, int top = 0, bool detalhar = false)
        {
            ContatoDAO ContatoDAO = null;
            try
            {
                ContatoDAO = new ContatoDAO(GetSqlCommand());
                return ContatoDAO.Selecionar(objVO);

            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
