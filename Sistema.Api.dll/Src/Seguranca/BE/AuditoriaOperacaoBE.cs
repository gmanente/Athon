using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Seguranca.DAO;
using Sistema.Api.dll.Src.Seguranca.VO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Sistema.Api.dll.Src.Seguranca.BE
{
    public class AuditoriaOperacaoBE : AbstractBE, IBE<AuditoriaOperacaoVO>
    {
        public AuditoriaOperacaoBE(SqlCommand sqlComm)
        : base(sqlComm)
        {

        }

        public AuditoriaOperacaoBE()
        : base()
        {

        }

        public long Inserir(AuditoriaOperacaoVO objVO)
        {
            AuditoriaOperacaoDAO AuditoriaOperacaoDAO = null;
            try
            {
                long id;

                BeginTransaction();
                AuditoriaOperacaoDAO = new AuditoriaOperacaoDAO(GetSqlCommand());
                id = AuditoriaOperacaoDAO.Inserir(objVO);
                objVO.Id = id;

                Commit();
                return id;
            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
        }
        public long Inserir(AuditoriaOperacaoVO objVO, bool openTran = true)
        {
            AuditoriaOperacaoDAO AuditoriaOperacaoDAO = null;
            try
            {
                long id;
                //if (openTran)
                //    BeginTransaction();
                AuditoriaOperacaoDAO = new AuditoriaOperacaoDAO(GetSqlCommand());
                id = AuditoriaOperacaoDAO.Inserir(objVO);
                objVO.Id = id;
                //if (openTran)
                //    Commit();
                return id;
            }
            catch (Exception e)
            {
                //if (openTran)
                //    Rollback();
                throw e;
            }
        }

        public long Alterar(AuditoriaOperacaoVO objVO, string where = null)
        {
            AuditoriaOperacaoDAO AuditoriaOperacaoDAO = null;
            try
            {
                long id;

                BeginTransaction();
                AuditoriaOperacaoDAO = new AuditoriaOperacaoDAO(GetSqlCommand());
                id = AuditoriaOperacaoDAO.Alterar(objVO, where);

                Commit();
                return id;
            }
            catch (Exception e)
            {

                Rollback();
                throw e;
            }
        }
        public long Alterar(AuditoriaOperacaoVO objVO, bool openTran = true)
        {
            AuditoriaOperacaoDAO AuditoriaOperacaoDAO = null;
            try
            {
                long id;
                if (openTran)
                    BeginTransaction();
                AuditoriaOperacaoDAO = new AuditoriaOperacaoDAO(GetSqlCommand());
                id = AuditoriaOperacaoDAO.Alterar(objVO);
                if (openTran)
                    Commit();
                return id;
            }
            catch (Exception e)
            {
                if (openTran)
                    Rollback();
                throw e;
            }
        }

        public void Deletar(AuditoriaOperacaoVO objVO)
        {
            AuditoriaOperacaoDAO AuditoriaOperacaoDAO = null;
            try
            {

                BeginTransaction();
                AuditoriaOperacaoDAO = new AuditoriaOperacaoDAO(GetSqlCommand());
                AuditoriaOperacaoDAO.Deletar(objVO);

                Commit();
            }
            catch (Exception e)
            {

                Rollback();
                throw e;
            }
        }
        public void Deletar(AuditoriaOperacaoVO objVO, bool openTran = true)
        {
            AuditoriaOperacaoDAO AuditoriaOperacaoDAO = null;
            try
            {
                if (openTran)
                    BeginTransaction();
                AuditoriaOperacaoDAO = new AuditoriaOperacaoDAO(GetSqlCommand());
                AuditoriaOperacaoDAO.Deletar(objVO);
                if (openTran)
                    Commit();
            }
            catch (Exception e)
            {
                if (openTran)
                    Rollback();
                throw e;
            }
        }

        public AuditoriaOperacaoVO Consultar(AuditoriaOperacaoVO objVO)
        {
            AuditoriaOperacaoDAO AuditoriaOperacaoDAO = null;
            try
            {
                AuditoriaOperacaoDAO = new AuditoriaOperacaoDAO(GetSqlCommand());
                return AuditoriaOperacaoDAO.Consultar(objVO);

            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public List<AuditoriaOperacaoVO> Listar(AuditoriaOperacaoVO objVO, bool detalhar = false)
        {
            AuditoriaOperacaoDAO AuditoriaOperacaoDAO = null;
            try
            {
                AuditoriaOperacaoDAO = new AuditoriaOperacaoDAO(GetSqlCommand());
                return AuditoriaOperacaoDAO.Listar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<AuditoriaOperacaoVO> Selecionar(AuditoriaOperacaoVO objVO, int top = 0, bool detalhar = false)
        {
            AuditoriaOperacaoDAO AuditoriaOperacaoDAO = null;
            try
            {
                AuditoriaOperacaoDAO = new AuditoriaOperacaoDAO(GetSqlCommand());
                return AuditoriaOperacaoDAO.Selecionar(objVO);

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        
        public List<UsuarioFuncionalidadeVO> lstUsuarioFuncionalidade { get; set; }
    }
}