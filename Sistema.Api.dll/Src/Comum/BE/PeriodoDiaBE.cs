using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Comum.DAO;
using Sistema.Api.dll.Src.Comum.VO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Sistema.Api.dll.Src.Comum.BE
{
    public class PeriodoDiaBE : AbstractBE, IBE<PeriodoDiaVO>
    {
        public PeriodoDiaBE()
            : base()
        {
        }

        public PeriodoDiaBE(SqlCommand sqlComm)
            : base(sqlComm)
        {
        }

        public long Inserir(PeriodoDiaVO objVO)
        {
            throw new NotImplementedException();
        }

        public long Alterar(PeriodoDiaVO objVO, string @where = null)
        {
            throw new NotImplementedException();
        }

        public void Deletar(PeriodoDiaVO objVO)
        {
            throw new NotImplementedException();
        }


        public List<PeriodoDiaVO> Selecionar(PeriodoDiaVO objVO, int top = 0, bool detalhar = false)
        {
            PeriodoDiaDAO periodoDiaDao = null;
            try
            {
                periodoDiaDao = new PeriodoDiaDAO(GetSqlCommand());
                return periodoDiaDao.Selecionar(objVO, top);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public PeriodoDiaVO Consultar(PeriodoDiaVO objVO)
        {
            PeriodoDiaDAO periodoDiaDao = null;
            try
            {
                periodoDiaDao = new PeriodoDiaDAO(GetSqlCommand());
                return periodoDiaDao.Consultar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Dictionary<int, List<PeriodoDiaVO>> Paginar(string sql, int inicio, int fim)
        {
            PeriodoDiaDAO periodoDiaDao = null;

            try
            {
                periodoDiaDao = new PeriodoDiaDAO(GetSqlCommand());
                return periodoDiaDao.Paginar(sql, inicio, fim);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<PeriodoDiaVO> Listar(PeriodoDiaVO objVO = null, bool detalhar = false)
        {
            PeriodoDiaDAO periodoDiaDao = null;
            try
            {
                periodoDiaDao = new PeriodoDiaDAO(GetSqlCommand());
                return periodoDiaDao.Listar(objVO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}