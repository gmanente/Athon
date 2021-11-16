using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Seguranca.DAO;
using Sistema.Api.dll.Src.Seguranca.VO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Sistema.Api.dll.Src.Seguranca.BE
{
    public class PerfilDepartamentoBE : AbstractBE, IBE<PerfilDepartamentoVO>
    {
        public PerfilDepartamentoBE(SqlCommand sqlComm)
            : base(sqlComm)
        {
        }

        public PerfilDepartamentoBE()
            : base()
        {
        }

        public long Inserir(PerfilDepartamentoVO perfilDepartamentoVo)
        {
            //throw new NotImplementedException();
            PerfilDepartamentoDAO perfilDepartamentoDao = null;
            try
            {
                long id;
                BeginTransaction();
                perfilDepartamentoDao = new PerfilDepartamentoDAO(GetSqlCommand());
                id = perfilDepartamentoDao.Inserir(perfilDepartamentoVo);
                Commit();
                return id;
            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
        }

        public long Alterar(PerfilDepartamentoVO perfilDepartamentoVo, string where = null)
        {
            throw new NotImplementedException();
        }

        public void Deletar(PerfilDepartamentoVO perfilDepartamentoVo)
        {
            PerfilDepartamentoDAO perfilDepartamentoDao = null;
            try
            {
                perfilDepartamentoDao = new PerfilDepartamentoDAO(GetSqlCommand());
                perfilDepartamentoDao.Deletar(perfilDepartamentoVo);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // InsertOrUpdateOrDelete
        public bool InsertOrUpdateOrDelete(PerfilDepartamentoVO objVO, long IdPerfilDepartamento, bool Deletar)
        {
            PerfilDepartamentoDAO perfilDepartamentoDao = null;
            PerfilDepartamentoVO PerfilDepartamentoVO = null;
            try
            {
                perfilDepartamentoDao = new PerfilDepartamentoDAO(GetSqlCommand());

                if (IdPerfilDepartamento > 0 && Deletar)
                {
                    objVO.Id = IdPerfilDepartamento;
                    perfilDepartamentoDao.Deletar(objVO);
                    return true;
                }
                else if (IdPerfilDepartamento > 0 && !Deletar)
                {
                    PerfilDepartamentoVO = perfilDepartamentoDao.Consultar(new PerfilDepartamentoVO() { Perfil = { Id = objVO.Perfil.Id }, Departamento = { Id = objVO.Departamento.Id }, Ativar = objVO.Ativar });
                    if (PerfilDepartamentoVO == null)
                    {
                        objVO.Id = IdPerfilDepartamento;
                        perfilDepartamentoDao.Alterar(objVO);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    PerfilDepartamentoVO = perfilDepartamentoDao.Consultar(new PerfilDepartamentoVO() { Perfil = { Id = objVO.Perfil.Id }, Departamento = { Id = objVO.Departamento.Id } });
                    if (PerfilDepartamentoVO == null)
                    {
                        perfilDepartamentoDao.Inserir(objVO);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public List<PerfilDepartamentoVO> Selecionar(PerfilDepartamentoVO objVO, int top = 0, bool detalhar = false)
        {
            PerfilDepartamentoDAO perfilDepartamentoDao = null;

            try
            {
                perfilDepartamentoDao = new PerfilDepartamentoDAO(GetSqlCommand());
                return perfilDepartamentoDao.Selecionar(objVO, top);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public PerfilDepartamentoVO Consultar(PerfilDepartamentoVO perfilDepartamentoVo)
        {
            PerfilDepartamentoDAO perfilDepartamentoDao = null;
            PerfilDepartamentoVO perfilDepartamento = null;

            try
            {
                perfilDepartamentoDao = new PerfilDepartamentoDAO(GetSqlCommand());
                perfilDepartamento = perfilDepartamentoDao.Consultar(perfilDepartamentoVo);
            }
            catch (Exception e)
            {
                throw e;
            }

            return perfilDepartamento;
        }

        public List<PerfilDepartamentoVO> Listar(PerfilDepartamentoVO perfilDepartamentoVo = null, bool detalhar = false)
        {
            PerfilDepartamentoDAO perfilDepartamentoDao = null;
            List<PerfilDepartamentoVO> lstPerfilDepartamento = null;

            try
            {
                perfilDepartamentoDao = new PerfilDepartamentoDAO(GetSqlCommand());
                lstPerfilDepartamento = perfilDepartamentoDao.Listar(perfilDepartamentoVo);
            }
            catch (Exception e)
            {
                throw e;
            }

            return lstPerfilDepartamento;
        }

        public Dictionary<int, List<PerfilDepartamentoVO>> Paginar(PerfilDepartamentoVO objVO)
        {
            PerfilDepartamentoDAO PerfilDepartamentoDAO = null;

            try
            {
                PerfilDepartamentoDAO = new PerfilDepartamentoDAO(GetSqlCommand());
                return PerfilDepartamentoDAO.Paginar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}