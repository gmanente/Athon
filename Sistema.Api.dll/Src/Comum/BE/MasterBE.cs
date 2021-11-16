using Sistema.Api.dll.Src.Comum.DAO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Sistema.Api.dll.Src.Comum.BE
{
    public class MasterBE : AbstractBE //, IBE<MasterVO>
    {
        public MasterBE()
            : base()
        {
        }

        public MasterBE(SqlCommand sqlComm)
            : base(sqlComm)
        {
        }


        // ExecuteQuery
        public Dictionary<string, object> ExecuteQuery(string querySql)
        {
            MasterDAO MasterDao = null;
            try
            {
                BeginTransaction();
                MasterDao = new MasterDAO(GetSqlCommand());
                var Dictionary = MasterDao.ExecuteQuery(querySql);
                return Dictionary;
            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
        }

        public void CommitQuery()
        {
            try
            {
                Commit();
            }
            catch (Exception)
            {
                Rollback();
                throw;
            }
        }

        public void RollbackQuery()
        {
            try
            {
                Rollback();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}