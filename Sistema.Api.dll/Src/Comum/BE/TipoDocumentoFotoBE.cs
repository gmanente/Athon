using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Comum.DAO;
using Sistema.Api.dll.Src.Comum.VO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Sistema.Api.dll.Src.Comum.BE
{
    public class TipoDocumentoFotoBE : AbstractBE, IBE<TipoDocumentoFotoVO>
    {
        public TipoDocumentoFotoBE()
            : base()
        {
        }

        public TipoDocumentoFotoBE(SqlCommand sqlConn)
            : base(sqlConn)
        {
        }

        public long Inserir(TipoDocumentoFotoVO objVO)
        {
            throw new NotImplementedException();
        }

        public long Alterar(TipoDocumentoFotoVO objVO, string where = null)
        {
            throw new NotImplementedException();
        }

        public void Deletar(TipoDocumentoFotoVO objVO)
        {
            throw new NotImplementedException();
        }


        public List<TipoDocumentoFotoVO> Selecionar(TipoDocumentoFotoVO objVO, int top = 0, bool detalhar = false)
        {
            TipoDocumentoFotoDAO tipoDocumentoDao = null;
            try
            {
                tipoDocumentoDao = new TipoDocumentoFotoDAO(GetSqlCommand());
                return tipoDocumentoDao.Selecionar(objVO, top);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public TipoDocumentoFotoVO Consultar(TipoDocumentoFotoVO objVO)
        {
            TipoDocumentoFotoDAO tipoDocumentoDao = null;
            try
            {
                tipoDocumentoDao = new TipoDocumentoFotoDAO(GetSqlCommand());
                return tipoDocumentoDao.Consultar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<TipoDocumentoFotoVO> Listar(TipoDocumentoFotoVO objVO = null, bool detalhar = false)
        {
            TipoDocumentoFotoDAO tipoDocumentoDao = null;
            try
            {
                tipoDocumentoDao = new TipoDocumentoFotoDAO(GetSqlCommand());
                return tipoDocumentoDao.Listar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}