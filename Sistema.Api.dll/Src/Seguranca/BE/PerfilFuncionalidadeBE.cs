using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Seguranca.DAO;
using Sistema.Api.dll.Src.Seguranca.VO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Sistema.Api.dll.Src.Seguranca.BE
{
    public class PerfilFuncionalidadeBE : AbstractBE, IBE<PerfilFuncionalidadeVO>
    {
        public PerfilFuncionalidadeBE(SqlCommand sqlComm)
            : base(sqlComm)
        {
        }

        public PerfilFuncionalidadeBE()
            : base()
        {
        }

        public long Inserir(PerfilFuncionalidadeVO objVO)
        {
            PerfilFuncionalidadeDAO perfilFuncionalidadeDao = null;
            try
            {
                long id;
                BeginTransaction();
                perfilFuncionalidadeDao = new PerfilFuncionalidadeDAO(GetSqlCommand());
                id = perfilFuncionalidadeDao.Inserir(objVO);
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

        public long Alterar(PerfilFuncionalidadeVO objVO, string where = null)
        {
            PerfilFuncionalidadeDAO perfilFuncionalidadeDao = null;
            try
            {
                long id;
                BeginTransaction();
                perfilFuncionalidadeDao = new PerfilFuncionalidadeDAO(GetSqlCommand());
                id = perfilFuncionalidadeDao.Alterar(objVO, where);
                Commit();
                return id;
            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
        }

        public void Deletar(PerfilFuncionalidadeVO objVO)
        {
            PerfilFuncionalidadeDAO perfilFuncionalidadeDao = null;
            try
            {
                BeginTransaction();
                perfilFuncionalidadeDao = new PerfilFuncionalidadeDAO(GetSqlCommand());
                perfilFuncionalidadeDao.Deletar(objVO);
                Commit();
            }
            catch (Exception exception)
            {
                Rollback();
                throw exception;
            }
        }

        public List<PerfilFuncionalidadeVO> Selecionar(PerfilFuncionalidadeVO objVO, int top = 0, bool detalhar = false)
        {
            PerfilFuncionalidadeDAO perfilFuncionalidadeDao = null;
            try
            {
                perfilFuncionalidadeDao = new PerfilFuncionalidadeDAO(GetSqlCommand());
                return perfilFuncionalidadeDao.Selecionar(objVO, top);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public PerfilFuncionalidadeVO Consultar(PerfilFuncionalidadeVO objVO)
        {
            PerfilFuncionalidadeDAO perfilFuncionalidadeDao = null;
            try
            {
                perfilFuncionalidadeDao = new PerfilFuncionalidadeDAO(GetSqlCommand());
                return perfilFuncionalidadeDao.Consultar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<PerfilFuncionalidadeVO> Listar(PerfilFuncionalidadeVO objVO = null, bool detalhar = false)
        {
            PerfilFuncionalidadeDAO perfilFuncionalidadeDao = null;
            try
            {
                perfilFuncionalidadeDao = new PerfilFuncionalidadeDAO(GetSqlCommand());
                return perfilFuncionalidadeDao.Listar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        // TrazerFuncionalidadeNaoVinculados
        public List<FuncionalidadeVO> TrazerFuncionalidadeNaoVinculados(List<PerfilFuncionalidadeVO> lstPerfilFuncionalidadeVO, List<FuncionalidadeVO> lstFuncionalidadeVO)
        {
            List<FuncionalidadeVO> lst = lstFuncionalidadeVO.Where(m => !lstPerfilFuncionalidadeVO.Any(pm => pm.Funcionalidade.Id == m.Id)).ToList();
            return lst;
        }

        // VincularFuncionalidade
        public bool VincularFuncionalidade(long idPerfilSubModulo, int[] funcionalidadeSelecionadas, bool[] acesso)
        {
            PerfilFuncionalidadeDAO perfilPerfilFuncionalidadeDao = null;
            try
            {
                BeginTransaction();

                perfilPerfilFuncionalidadeDao = new PerfilFuncionalidadeDAO(GetSqlCommand());
                var lstPerfilModulo = perfilPerfilFuncionalidadeDao.Listar(new PerfilFuncionalidadeVO() { PerfilSubModulo = { Id = idPerfilSubModulo } });
                perfilPerfilFuncionalidadeDao.DesabilitarFuncionalidades(new PerfilFuncionalidadeVO() { PerfilSubModulo = { Id = idPerfilSubModulo } });

                int i = 0;
                foreach (var id in funcionalidadeSelecionadas)
                {
                    var lst = lstPerfilModulo.Where(pm => pm.Funcionalidade.Id == id);
                    if (lst.Count() == 0)
                    {
                        perfilPerfilFuncionalidadeDao.Inserir(new PerfilFuncionalidadeVO()
                        {
                            Ativar = true,
                            PerfilSubModulo = { Id = idPerfilSubModulo },
                            Funcionalidade = { Id = id },
                            AcessoExterno = acesso[i]
                        });
                    }
                    else
                    {
                        perfilPerfilFuncionalidadeDao.Alterar(new PerfilFuncionalidadeVO()
                        {
                            Ativar = true,
                            PerfilSubModulo = { Id = idPerfilSubModulo },
                            Funcionalidade = { Id = id },
                            AcessoExterno = acesso[i]
                        }, " WHERE IdPerfilSubModulo = " + idPerfilSubModulo + " AND IdFuncionalidade = " + id);
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