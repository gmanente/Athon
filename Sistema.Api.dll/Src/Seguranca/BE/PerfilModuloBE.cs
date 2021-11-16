using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Seguranca.DAO;
using Sistema.Api.dll.Src.Seguranca.VO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Sistema.Api.dll.Src.Seguranca.BE
{
    public class PerfilModuloBE : AbstractBE, IBE<PerfilModuloVO>
    {
        public PerfilModuloBE(SqlCommand sqlComm)
            : base(sqlComm)
        {
        }

        public PerfilModuloBE()
            : base()
        {
        }

        public long Inserir(PerfilModuloVO objVO)
        {
            PerfilModuloDAO perfilModuloDao = null;
            try
            {
                long id;
                BeginTransaction();
                perfilModuloDao = new PerfilModuloDAO(GetSqlCommand());
                id = perfilModuloDao.Inserir(objVO);
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

        public long Alterar(PerfilModuloVO objVO, string where = null)
        {
            PerfilModuloDAO perfilModuloDao = null;
            try
            {
                long id;
                BeginTransaction();
                perfilModuloDao = new PerfilModuloDAO(GetSqlCommand());
                id = perfilModuloDao.Alterar(objVO, where);
                Commit();
                return id;
            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
        }

        public void Deletar(PerfilModuloVO objVO)
        {
            PerfilModuloDAO perfilModuloDao = null;
            try
            {
                BeginTransaction();
                perfilModuloDao = new PerfilModuloDAO(GetSqlCommand());
                perfilModuloDao.Deletar(objVO);
                Commit();
            }
            catch (Exception exception)
            {
                Rollback();
                throw exception;
            }
        }

        public List<PerfilModuloVO> Selecionar(PerfilModuloVO objVO, int top = 0, bool detalhar = false)
        {
            PerfilModuloDAO perfilModuloDao = null;
            try
            {
                perfilModuloDao = new PerfilModuloDAO(GetSqlCommand());
                return perfilModuloDao.Selecionar(objVO, top);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public PerfilModuloVO Consultar(PerfilModuloVO objVO)
        {
            PerfilModuloDAO perfilModuloDao = null;
            try
            {
                perfilModuloDao = new PerfilModuloDAO(GetSqlCommand());
                return perfilModuloDao.Consultar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<PerfilModuloVO> Listar(PerfilModuloVO objVO = null, bool detalhar = false)
        {
            PerfilModuloDAO perfilModuloDao = null;
            try
            {
                perfilModuloDao = new PerfilModuloDAO(GetSqlCommand());
                return perfilModuloDao.Listar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        // TrazerModulosNaoVinculados
        public List<ModuloVO> TrazerModulosNaoVinculados(List<PerfilModuloVO> lstPerfilModuloVO, List<ModuloVO> lstModuloVO)
        {
            List<ModuloVO> lst = lstModuloVO.Where(m => !lstPerfilModuloVO.Any(pm => pm.Modulo.Id == m.Id)).ToList();
            return lst;
        }

        // VincularModulos
        public bool VincularModulos(long idPerfil, int[] modulosSelecionados, bool[] acessos)
        {
            PerfilModuloDAO perfilModuloDao = null;
            try
            {
                BeginTransaction();

                perfilModuloDao = new PerfilModuloDAO(GetSqlCommand());
                var lstPerfilModulo = perfilModuloDao.Listar(new PerfilModuloVO() { Perfil = { Id = idPerfil } });
                perfilModuloDao.DesabilitarModulos(new PerfilModuloVO() { Perfil = { Id = idPerfil } });
                int i = 0;
                foreach (var id in modulosSelecionados)
                {

                    var lst = lstPerfilModulo.Where(pm => pm.Modulo.Id == id);
                    if (lst.Count() == 0)
                    {
                        perfilModuloDao.Inserir(new PerfilModuloVO()
                        {
                            Ativar = true,
                            Modulo = { Id = id },
                            Perfil = { Id = idPerfil },
                            AcessoExterno = acessos[i]
                        });
                    }
                    else
                    {
                        perfilModuloDao.Alterar(new PerfilModuloVO()
                        {
                            Ativar = true,
                            Modulo = { Id = id },
                            Perfil = { Id = idPerfil },
                            AcessoExterno = acessos[i]
                        }, " WHERE IdPerfil = " + idPerfil + " AND IdModulo = " + id);
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