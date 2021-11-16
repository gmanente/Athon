using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Seguranca.DAO;
using Sistema.Api.dll.Src.Seguranca.VO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Sistema.Api.dll.Src.Seguranca.BE
{
    public class MenuRapidoItemBE : AbstractBE, IBE<MenuRapidoItemVO>
    {
        public MenuRapidoItemBE(SqlCommand sqlComm)
            : base(sqlComm)
        {

        }

        public MenuRapidoItemBE()
            : base()
        {

        }

        public long Inserir(MenuRapidoItemVO objVO)
        {
            MenuRapidoItemDAO menuRapidoItemDAO = null;
            try
            {
                long id;
                BeginTransaction();
                menuRapidoItemDAO = new MenuRapidoItemDAO(GetSqlCommand());
                id = menuRapidoItemDAO.Inserir(objVO);
                Commit();
                return id;
            }
            catch (Exception ex)
            {
                Rollback();
                throw ex;
            }
        }

        public long Alterar(MenuRapidoItemVO objVO, string where = null)
        {
            MenuRapidoItemDAO menuRapidoItemDAO = null;
            try
            {
                long id;
                BeginTransaction();
                menuRapidoItemDAO = new MenuRapidoItemDAO(GetSqlCommand());
                id = menuRapidoItemDAO.Alterar(objVO, where);
                Commit();
                return id;
            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
        }

        public void Deletar(MenuRapidoItemVO objVO)
        {
            MenuRapidoItemDAO menuRapidoItemDAO = null;

            try
            {
                BeginTransaction();
                menuRapidoItemDAO = new MenuRapidoItemDAO(GetSqlCommand());
                menuRapidoItemDAO.Deletar(objVO);
                Commit();
            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
        }

        public List<MenuRapidoItemVO> Selecionar(MenuRapidoItemVO objVO, int top = 0, bool detalhar = false)
        {
            MenuRapidoItemDAO menuRapidoItemDAO = null;
            List<MenuRapidoItemVO> lstMenuRapidoItem = null;

            try
            {
                menuRapidoItemDAO = new MenuRapidoItemDAO(GetSqlCommand());

                lstMenuRapidoItem = menuRapidoItemDAO.Selecionar(objVO);

                return lstMenuRapidoItem;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<MenuRapidoItemVO> SelecionarMenuRapidoItem(MenuRapidoItemVO objVO, int top = 0, bool detalhar = false)
        {
            MenuRapidoItemDAO menuRapidoItemDAO = null;
            List<MenuRapidoItemVO> lstMenuRapidoItem = null;

            try
            {
                menuRapidoItemDAO = new MenuRapidoItemDAO(GetSqlCommand());

                lstMenuRapidoItem = menuRapidoItemDAO.SelecionarMenuRapidoItem(objVO);

                return lstMenuRapidoItem;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public MenuRapidoItemVO Consultar(MenuRapidoItemVO objVO)
        {
            MenuRapidoItemDAO menuRapidoItemDAO = null;
            MenuRapidoItemVO menuRapidoItemVO = null;

            try
            {
                menuRapidoItemDAO = new MenuRapidoItemDAO(GetSqlCommand());

                menuRapidoItemVO = menuRapidoItemDAO.Consultar(objVO);

                return menuRapidoItemVO;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public List<MenuRapidoItemVO> Listar(MenuRapidoItemVO objVO = null, bool detalhar = false)
        {
            MenuRapidoItemDAO menuRapidoItemDAO = null;
            List<MenuRapidoItemVO> lstMenuRapidoItem = null;

            try
            {
                menuRapidoItemDAO = new MenuRapidoItemDAO(GetSqlCommand());

                lstMenuRapidoItem = menuRapidoItemDAO.Listar(objVO);

                return lstMenuRapidoItem;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<MenuRapidoItemVO> AutenticarMenuRapido(long idUsuario, long idCampus, bool acessoExterno, bool? portal = null, long idModulo = 0)
        {
            try
            {
                var menuRapidoItemDAO = new MenuRapidoItemDAO(GetSqlCommand());

                var lstMenuRapidoItemVO = menuRapidoItemDAO.AutenticarMenuRapido(idUsuario, idCampus, acessoExterno, portal, idModulo);

                return lstMenuRapidoItemVO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}