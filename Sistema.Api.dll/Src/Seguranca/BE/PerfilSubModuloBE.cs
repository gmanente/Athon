using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Seguranca.DAO;
using Sistema.Api.dll.Src.Seguranca.VO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Sistema.Api.dll.Src.Seguranca.BE
{
    public class PerfilSubModuloBE : AbstractBE, IBE<PerfilSubModuloVO>
    {
        public PerfilSubModuloBE(SqlCommand sqlComm)
            : base(sqlComm)
        {
        }

        public PerfilSubModuloBE()
            : base()
        {
        }

        public long Inserir(PerfilSubModuloVO objVO)
        {
            PerfilSubModuloDAO perfilSubModuloDao = null;
            try
            {
                long id;
                BeginTransaction();
                perfilSubModuloDao = new PerfilSubModuloDAO(GetSqlCommand());
                id = perfilSubModuloDao.Inserir(objVO);
                objVO.Id = id;
                Commit();
                return id;
            }
            catch (Exception ex)
            {
                Rollback();
                throw ex;
            }
        }

        public long Alterar(PerfilSubModuloVO objVO, string where = null)
        {
            PerfilSubModuloDAO perfilSubModuloDao = null;
            try
            {
                long id;
                BeginTransaction();
                perfilSubModuloDao = new PerfilSubModuloDAO(GetSqlCommand());
                id = perfilSubModuloDao.Alterar(objVO, where);
                Commit();
                return id;
            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
        }

        public void Deletar(PerfilSubModuloVO objVO)
        {
            PerfilSubModuloDAO perfilSubModuloDao = null;
            try
            {
                BeginTransaction();
                perfilSubModuloDao = new PerfilSubModuloDAO(GetSqlCommand());
                perfilSubModuloDao.Deletar(objVO);
                Commit();
            }
            catch (Exception exception)
            {
                Rollback();
                throw exception;
            }
        }

        public List<PerfilSubModuloVO> Selecionar(PerfilSubModuloVO objVO, int top = 0, bool detalhar = false)
        {
            PerfilSubModuloDAO perfilSubModuloDao = null;
            try
            {
                perfilSubModuloDao = new PerfilSubModuloDAO(GetSqlCommand());
                return perfilSubModuloDao.Selecionar(objVO, top);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public PerfilSubModuloVO Consultar(PerfilSubModuloVO objVO)
        {
            PerfilSubModuloDAO perfilSubModuloDao = null;
            try
            {
                perfilSubModuloDao = new PerfilSubModuloDAO(GetSqlCommand());
                return perfilSubModuloDao.Consultar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<PerfilSubModuloVO> Listar(PerfilSubModuloVO objVO = null, bool detalhar = false)
        {
            PerfilSubModuloDAO perfilSubModuloDao = null;
            try
            {
                perfilSubModuloDao = new PerfilSubModuloDAO(GetSqlCommand());
                return perfilSubModuloDao.Listar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        // TrazerSubModulosNaoVinculados
        public List<SubmoduloVO> TrazerSubModulosNaoVinculados(List<PerfilSubModuloVO> lstPerfilSubModuloVO, List<SubmoduloVO> lstSubModuloVO)
        {
            List<SubmoduloVO> lst = lstSubModuloVO.Where(m => !lstPerfilSubModuloVO.Any(pm => pm.SubModulo.Id == m.Id)).ToList();
            return lst;
        }

        // VincularSubModulos
        public bool VincularSubModulos(long idPerfilModulo, int[] subModulosSelecionados, bool[] acessos)
        {
            PerfilSubModuloDAO perfilSubModuloDao = null;
            try
            {
                BeginTransaction();

                perfilSubModuloDao = new PerfilSubModuloDAO(GetSqlCommand());
                var lstPerfilModulo = perfilSubModuloDao.Listar(new PerfilSubModuloVO() { PerfilModulo = { Id = idPerfilModulo } });
                perfilSubModuloDao.DesabilitarsSubModulos(new PerfilSubModuloVO() { PerfilModulo = { Id = idPerfilModulo } });

                int i = 0;
                foreach (var id in subModulosSelecionados)
                {
                    var lst = lstPerfilModulo.Where(pm => pm.SubModulo.Id == id);
                    if (lst.Count() == 0)
                    {
                        perfilSubModuloDao.Inserir(new PerfilSubModuloVO()
                        {
                            Ativar = true,
                            PerfilModulo = { Id = idPerfilModulo },
                            SubModulo = { Id = id },
                            AcessoExterno = acessos[i]
                        });
                    }
                    else
                    {
                        perfilSubModuloDao.Alterar(new PerfilSubModuloVO()
                        {
                            Ativar = true,
                            PerfilModulo = { Id = idPerfilModulo },
                            SubModulo = { Id = id },
                            AcessoExterno = acessos[i]

                        }, " WHERE IdPerfilModulo = " + idPerfilModulo + " AND IdSubModulo = " + id);
                    }
                    i++;
                }

                Commit();
                return true;
            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }

        }
    }
}