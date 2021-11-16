using Newtonsoft.Json.Linq;
using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Productive.VO;
using Sistema.Api.dll.Src.Productive.DAO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Sistema.Api.dll.Src.Productive.BE
{
    public class RevendaBE : AbstractBE
    {
        public RevendaBE(SqlCommand sqlComm)
            : base(sqlComm)
        {

        }

        public RevendaBE()
        {

        }
        public long Alterar(RevendaVO objVO, string where = null)
        {
            try
            {
                BeginTransaction();
                var RevendaDAO = new RevendaDAO(GetSqlCommand());
                var id = RevendaDAO.Alterar(objVO, where);

                Commit();
                return id;
            }
            catch (Exception)
            {

                Rollback();
                throw;
            }
        }

        public RevendaVO Consultar(RevendaVO objVO)
        {
            var RevendaDAO = new RevendaDAO(GetSqlCommand());
            return RevendaDAO.Consultar(objVO);
        }

        public void Deletar(RevendaVO objVO)
        {
            try
            {
                BeginTransaction();
                var RevendaDAO = new RevendaDAO(GetSqlCommand());
                RevendaDAO.Deletar(objVO);

                Commit();
            }
            catch (Exception)
            {

                Rollback();
                throw;
            }
        }

        public long Inserir(RevendaVO objVO)
        {
            try
            {
                BeginTransaction();
                var RevendaDAO = new RevendaDAO(GetSqlCommand());
                objVO.Id = RevendaDAO.Inserir(objVO);

                Commit();
                return objVO.Id;
            }
            catch (Exception)
            {
                Rollback();
                throw;
            }
        }

        public List<RevendaVO> Listar(RevendaVO objVO = null)
        {
            var RevendaDAO = new RevendaDAO(GetSqlCommand());
            return RevendaDAO.Listar(objVO);
        }

        public List<RevendaVO> Selecionar(RevendaVO objVO)
        {
            var RevendaDAO = new RevendaDAO(GetSqlCommand());
            return RevendaDAO.Selecionar(objVO);
        }

    }
}
