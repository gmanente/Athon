using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Seguranca.DAO;
using Sistema.Api.dll.Src.Seguranca.VO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Sistema.Api.dll.Src.Seguranca.BE
{
    public class MenuRapidoBE : AbstractBE, IBE<MenuRapidoVO>
    {
        public MenuRapidoBE(SqlCommand sqlComm)
            : base(sqlComm)
        {

        }

        public MenuRapidoBE()
            : base()
        {

        }

        public long Inserir(MenuRapidoVO objVO)
        {
            MenuRapidoDAO menuRapidoDAO = null;
            try
            {
                long id;
                BeginTransaction();
                menuRapidoDAO = new MenuRapidoDAO(GetSqlCommand());
                id = menuRapidoDAO.Inserir(objVO);
                Commit();
                return id;
            }
            catch (Exception ex)
            {
                Rollback();
                throw ex;
            }
        }

        public long Alterar(MenuRapidoVO objVO, string where = null)
        {
            MenuRapidoDAO menuRapidoDAO = null;
            try
            {
                long id;
                BeginTransaction();
                menuRapidoDAO = new MenuRapidoDAO(GetSqlCommand());
                id = menuRapidoDAO.Alterar(objVO, where);
                Commit();
                return id;
            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
        }

        public void Deletar(MenuRapidoVO objVO)
        {
            MenuRapidoDAO menuRapidoDAO = null;

            try
            {
                BeginTransaction();
                menuRapidoDAO = new MenuRapidoDAO(GetSqlCommand());
                menuRapidoDAO.Deletar(objVO);
                Commit();
            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
        }

        public void DeletarMenuRapido(MenuRapidoVO objVO)
        {
            MenuRapidoDAO menuRapidoDAO = null;
            MenuRapidoItemDAO menuRapidoItemDAO = null;

            try
            {
                menuRapidoDAO = new MenuRapidoDAO(GetSqlCommand());
                menuRapidoItemDAO = new MenuRapidoItemDAO(GetSqlCommand());

                BeginTransaction();

                menuRapidoItemDAO.DeletarPorMenuRapido(new MenuRapidoItemVO { MenuRapido = { Id = objVO.Id } });

                menuRapidoDAO.Deletar(objVO);

                Commit();
            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
        }

        public List<MenuRapidoVO> Selecionar(MenuRapidoVO objVO, int top = 0, bool detalhar = false)
        {
            MenuRapidoDAO menuRapidoDao = null;
            List<MenuRapidoVO> lstMenuRapido = null;

            try
            {
                menuRapidoDao = new MenuRapidoDAO(GetSqlCommand());

                lstMenuRapido = menuRapidoDao.Selecionar(objVO);

                return lstMenuRapido;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<MenuRapidoVO> SelecionarMenuRapido(MenuRapidoVO objVO)
        {
            MenuRapidoDAO menuRapidoDao = null;
            List<MenuRapidoVO> lstMenuRapido = null;

            try
            {
                menuRapidoDao = new MenuRapidoDAO(GetSqlCommand());

                lstMenuRapido = menuRapidoDao.SelecionarMenuRapido(objVO);

                return lstMenuRapido;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public MenuRapidoVO Consultar(MenuRapidoVO objVO)
        {
            MenuRapidoDAO menuRapidoDao = null;
            MenuRapidoVO menuRapidoVO = null;

            try
            {
                menuRapidoDao = new MenuRapidoDAO(GetSqlCommand());

                menuRapidoVO = menuRapidoDao.Consultar(objVO);

                return menuRapidoVO;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public List<MenuRapidoVO> Listar(MenuRapidoVO objVO = null, bool detalhar = false)
        {
            MenuRapidoDAO menuRapidoDao = null;
            List<MenuRapidoVO> lstMenuRapido = null;

            try
            {
                menuRapidoDao = new MenuRapidoDAO(GetSqlCommand());

                lstMenuRapido = menuRapidoDao.Listar(objVO);

                return lstMenuRapido;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}