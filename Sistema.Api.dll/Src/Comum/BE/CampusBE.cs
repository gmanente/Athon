using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Comum.DAO;
using Sistema.Api.dll.Src.Comum.VO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Sistema.Api.dll.Src.Comum.BE
{
    public class CampusBE : AbstractBE, IBE<CampusVO>
    {
        public CampusBE()
            : base()
        {
        }

        public CampusBE(SqlCommand sqlConn)
            : base(sqlConn)
        {
        }

        public long Inserir(CampusVO objVO)
        {
            CampusDAO CampusDAO = null;
            try
            {
                long id = 0;
                BeginTransaction();
                CampusDAO = new CampusDAO(GetSqlCommand());
                var CampusValido = CampusDAO.Consultar(objVO) == null;
                if (CampusValido)
                {
                    id = CampusDAO.Inserir(objVO);
                }
                else
                    throw new Exception("Já existe um Campus com esses dados");

                Commit();
                return id;
            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
        }

        public long Alterar(CampusVO objVO, string where = null)
        {
            CampusDAO CampusDAO = null;
            try
            {
                long id;
                BeginTransaction();
                CampusDAO = new CampusDAO(GetSqlCommand());
                id = CampusDAO.Alterar(objVO, where);
                Commit();
                return id;
            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
        }

        public void Deletar(CampusVO objVO)
        {
            CampusDAO CampusDAO = null;
            try
            {
                BeginTransaction();
                CampusDAO = new CampusDAO(GetSqlCommand());
                CampusDAO.Deletar(objVO);
                Commit();
            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
        }

        public CampusVO Consultar(CampusVO campusVo)
        {
            CampusDAO campusDao = null;
            EmpresaDAO empresaDao = null;
            CampusVO campus = null;

            try
            {
                campusDao = new CampusDAO(GetSqlCommand());
                empresaDao = new EmpresaDAO(GetSqlCommand());
                campus = campusDao.Consultar(campusVo);

                if (campus != null)
                {
                    campus.Empresa = empresaDao.Consultar(new EmpresaVO() { Id = campus.Empresa.Id });
                }

            }
            catch (Exception e)
            {
                throw e;
            }
            return campus;
        }

        public List<CampusVO> Listar(CampusVO campusVo = null, bool detalhar = false)
        {
            CampusDAO campusDao = null;
            EmpresaDAO empresaDao = null;
            List<CampusVO> lstCampus = null;

            try
            {
                campusDao = new CampusDAO(GetSqlCommand());
                empresaDao = new EmpresaDAO(GetSqlCommand());
                lstCampus = campusDao.Listar(campusVo);
                if (detalhar)
                {
                    foreach (var campus in lstCampus)
                    {
                        campus.Empresa = empresaDao.Consultar(new EmpresaVO() { Id = campus.Empresa.Id });
                    }
                }

            }
            catch (Exception e)
            {
                throw e;
            }
            return lstCampus;
        }

        public List<EmpresaVO> ListarEmpresa(EmpresaVO EmpresaVo = null, bool detalhar = false)
        {

            EmpresaDAO empresaDao = null;

            try
            {
                empresaDao = new EmpresaDAO(GetSqlCommand());
                return empresaDao.Selecionar();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Autor: Gustavo Martins
        /// Data: 25.07.2015
        /// Descrição: Resonsavel por listar todos os campus validos dentro dos editais validos
        /// </summary>
        /// <param name="objVO"></param>
        /// <returns></returns>
        public List<CampusVO> ListarCampusEditaisValidos()
        {
            CampusDAO campusDao = null;
            List<CampusVO> lstCampus = null;
            try
            {
                campusDao = new CampusDAO(GetSqlCommand());
                lstCampus = campusDao.ListarCampusEditaisValidos(DateTime.Now);
                return lstCampus;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        //ListarCampusUsuario
        /// <summary>
        /// Autor: Marcelo Campaner
        /// Data: 14.01.2015
        /// Descrição: Responsável por Listar Todos os campus no qual o Usuário poderá trabalhar
        /// </summary>
        /// <param name="lstCampus">Lista CampusVO</param>
        /// <param name="idsCampus">Códigos de Campus</param>
        /// <returns>Não há retorno</returns>
        public void ListarCampusUsuario(List<CampusVO> lstCampus, string idsCampus)
        {
            List<string> codigoCampus = null;
            List<CampusVO> lstCampusComparacao = null;
            try
            {
                if (lstCampus != null && !string.IsNullOrEmpty(idsCampus))
                {
                    codigoCampus = new List<string>();
                    codigoCampus.AddRange(idsCampus.Split(','));
                    lstCampusComparacao = new List<CampusVO>();
                    lstCampusComparacao = Listar();
                    var listaCampus = from campus in lstCampusComparacao where codigoCampus.Contains(Convert.ToString(campus.Id)) select campus;
                    lstCampus.Clear();
                    lstCampus.AddRange(listaCampus);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// CampusPorUsuario
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        public string CampusPorUsuario(long idUsuario)
        {
            try
            {
                var dao = new CampusDAO(GetSqlCommand());

                return dao.CampusPorUsuario(idUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// SelecionarPorUsuario
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <param name="campusVO"></param>
        /// <returns></returns>
        public List<CampusVO> SelecionarPorUsuario(long idUsuario, CampusVO campusVO = null)
        {
            try
            {
                var dao = new CampusDAO(GetSqlCommand());

                return dao.SelecionarPorUsuario(idUsuario, campusVO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<CampusVO> Selecionar(CampusVO objVO, int top = 0, bool detalhar = false)
        {
            CampusDAO campusDao = null;
            List<CampusVO> lstCampus = null;

            try
            {
                campusDao = new CampusDAO(GetSqlCommand());
                lstCampus = campusDao.Selecionar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
            return lstCampus;
        }

        public List<CampusVO> SelecionarCampusDisponivelServicoProtocolo(CampusVO objVO, int top = 0, bool detalhar = false)
        {
            CampusDAO campusDao = null;
            List<CampusVO> lstCampus = null;

            try
            {
                campusDao = new CampusDAO(GetSqlCommand());
                lstCampus = campusDao.SelecionarCampusDisponivelServicoProtocolo(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
            return lstCampus;
        }


        public List<CampusVO> ListaCampusComCurso()
        {
            CampusDAO campusDAO = null;
            try
            {
                campusDAO = new CampusDAO(GetSqlCommand());
                return campusDAO.ListaCampusComCurso();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}