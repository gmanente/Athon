using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Seguranca.DAO;
using Sistema.Api.dll.Src.Seguranca.VO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Sistema.Api.dll.Src.Seguranca.BE
{
    public class SistemaBE : AbstractBE, IBE<SistemaVO>
    {
        public SistemaBE(SqlCommand sqlComm)
            : base(sqlComm)
        {

        }

        public SistemaBE()
            : base()
        {

        }

        public long Inserir(SistemaVO sistemaVo)
        {
            throw new NotImplementedException();
        }

        public long Alterar(SistemaVO sistemaVo, string where = null)
        {
            throw new NotImplementedException();
        }

        public void Deletar(SistemaVO sistemaVo)
        {
            throw new NotImplementedException();
        }

        public List<SistemaVO> Selecionar(SistemaVO sistemaVo = null, int top = 0, bool detalhar = false)
        {
            SistemaDAO sistemaDao = new SistemaDAO(GetSqlCommand());
            try
            {
                return sistemaDao.Selecionar(sistemaVo, top);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public SistemaVO Consultar(SistemaVO sistemaVo)
        {
            SistemaDAO sistemaDao = new SistemaDAO(GetSqlCommand());
            try
            {
                return sistemaDao.Consultar(sistemaVo);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<SistemaVO> Listar(SistemaVO sistemaVo = null, bool detalhar = false)
        {
            SistemaDAO sistemaDao = new SistemaDAO(GetSqlCommand());
            try
            {
                return sistemaDao.Listar(sistemaVo);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}