using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Seguranca.DAO;
using Sistema.Api.dll.Src.Seguranca.VO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Sistema.Api.dll.Src.Seguranca.BE
{
    public class UsuarioDepartamentoBE : AbstractBE, IBE<UsuarioDepartamentoVO>
    {
        //UsuarioDepartamentoBE
        public UsuarioDepartamentoBE(SqlCommand sqlComm)
            : base(sqlComm)
        {
        }

        //UsuarioDepartamentoBE
        public UsuarioDepartamentoBE()
            : base()
        {
        }

        //Alterar
        public long Alterar(UsuarioDepartamentoVO usuarioDepartamentoVo, string where = null)
        {
            throw new NotImplementedException();
        }

        //Deletar
        public void Deletar(UsuarioDepartamentoVO usuarioDepartamentoVo)
        {
            UsuarioDepartamentoDAO usuarioDepartamentoDao = null;
            try
            {
                usuarioDepartamentoDao = new UsuarioDepartamentoDAO(GetSqlCommand());
                usuarioDepartamentoDao.Deletar(usuarioDepartamentoVo);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //Inserir
        public long Inserir(UsuarioDepartamentoVO usuarioDepartamentoVo)
        {
            //throw new NotImplementedException();
            UsuarioDepartamentoDAO usuarioDepartamentoDao = null;
            try
            {
                long id;
                BeginTransaction();
                usuarioDepartamentoDao = new UsuarioDepartamentoDAO(GetSqlCommand());
                id = usuarioDepartamentoDao.Inserir(usuarioDepartamentoVo);
                Commit();
                return id;
            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
        }

        //Selecionar
        public List<UsuarioDepartamentoVO> Selecionar(UsuarioDepartamentoVO objVO, int top = 0, bool detalhar = false)
        {
            UsuarioDepartamentoDAO usuarioDepartamentoDao = null;
            try
            {
                usuarioDepartamentoDao = new UsuarioDepartamentoDAO(GetSqlCommand());

                return usuarioDepartamentoDao.Selecionar(objVO, top);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //SelecionarUsuarioDepartamento (Nivel Alçada)
        public List<UsuarioDepartamentoVO> SelecionarUsuarioDepartamento(UsuarioDepartamentoVO objVO, int top = 0, bool detalhar = false)
        {
            UsuarioDepartamentoDAO usuarioDepartamentoDao = null;
            try
            {
                usuarioDepartamentoDao = new UsuarioDepartamentoDAO(GetSqlCommand());

                return usuarioDepartamentoDao.SelecionarUsuarioDepartamento(objVO, top);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool InsertOrUpdateOrDelete(UsuarioDepartamentoVO objVO, long IdUsuarioDepartamento, bool Deletar)
        {
            UsuarioDepartamentoDAO usuarioDepartamentoDao = null;
            UsuarioDepartamentoVO UsuarioDepartamentoVO = null;
            try
            {
                usuarioDepartamentoDao = new UsuarioDepartamentoDAO(GetSqlCommand());

                if (IdUsuarioDepartamento > 0 && Deletar)
                {
                    objVO.Id = IdUsuarioDepartamento;
                    usuarioDepartamentoDao.Deletar(objVO);
                    return true;
                }
                else if (IdUsuarioDepartamento > 0 && !Deletar)
                {
                    UsuarioDepartamentoVO = usuarioDepartamentoDao.Consultar(new UsuarioDepartamentoVO() { Usuario = { Id = objVO.Usuario.Id }, Departamento = { Id = objVO.Departamento.Id }, Ativar = objVO.Ativar });
                    if (UsuarioDepartamentoVO == null)
                    {
                        objVO.Id = IdUsuarioDepartamento;
                        usuarioDepartamentoDao.Alterar(objVO);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    UsuarioDepartamentoVO = usuarioDepartamentoDao.Consultar(new UsuarioDepartamentoVO() { Usuario = { Id = objVO.Usuario.Id }, Departamento = { Id = objVO.Departamento.Id }});
                    if (UsuarioDepartamentoVO == null)
                    {
                        usuarioDepartamentoDao.Inserir(objVO);
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


        //Consultar
        public UsuarioDepartamentoVO Consultar(UsuarioDepartamentoVO usuarioDepartamentoVo)
        {
            UsuarioDepartamentoDAO usuarioDepartamentoDao = null;
            UsuarioDepartamentoVO usuarioDepartamento = null;
            try
            {
                usuarioDepartamentoDao = new UsuarioDepartamentoDAO(GetSqlCommand());

                usuarioDepartamento = usuarioDepartamentoDao.Consultar(usuarioDepartamentoVo);
            }
            catch (Exception e)
            {
                throw e;
            }

            return usuarioDepartamento;
        }

        //Listar
        public List<UsuarioDepartamentoVO> Listar(UsuarioDepartamentoVO usuarioDepartamentoVo = null, bool detalhar = false)
        {
            UsuarioDepartamentoDAO usuarioDepartamentoDao = null;

            List<UsuarioDepartamentoVO> lstUsuarioDepartamento = null;
            try
            {
                usuarioDepartamentoDao = new UsuarioDepartamentoDAO(GetSqlCommand());

                lstUsuarioDepartamento = usuarioDepartamentoDao.Listar(usuarioDepartamentoVo);
            }
            catch (Exception e)
            {
                throw e;
            }

            return lstUsuarioDepartamento;
        }

        public Dictionary<int, List<UsuarioDepartamentoVO>> Paginar(UsuarioDepartamentoVO objVO)
        {
            UsuarioDepartamentoDAO UsuarioDepartamentoDAO = null;
            try
            {
                UsuarioDepartamentoDAO = new UsuarioDepartamentoDAO(GetSqlCommand());
                return UsuarioDepartamentoDAO.Paginar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
