using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Sistema.Api.dll.Src.Seguranca.DAO;
using Sistema.Api.dll.Src.Seguranca.VO;

namespace Sistema.Api.dll.Src.Seguranca.BE
{
    public class UsuarioFuncionalidadeBE : AbstractBE
    {

        public UsuarioFuncionalidadeBE(SqlCommand sqlComm)
            : base(sqlComm)
        { }

        public UsuarioFuncionalidadeBE()
            : base()
        { }

        public long Inserir(UsuarioFuncionalidadeVO usuarioFuncionalidadeVo)
        {
            throw new NotImplementedException();
        }

        public long Alterar(UsuarioFuncionalidadeVO usuarioFuncionalidadeVo, string where = null)
        {
            throw new NotImplementedException();
        }

        public void Deletar(UsuarioFuncionalidadeVO usuarioFuncionalidadeVo)
        {
            throw new NotImplementedException();
        }


        public void Inserir(List<UsuarioFuncionalidadeVO> lstUsuarioFuncionalidadeVo, long idUsuario, long idUsuarioCampus, long idUsuarioModulo, long idUsuarioSubmodulo, long idModulo, long idSubmodulo)
        {
            try
            {
                BeginTransaction();

                var usuarioFuncionalidadeDAO = new UsuarioFuncionalidadeDAO(GetSqlCommand());
                usuarioFuncionalidadeDAO.Inserir(lstUsuarioFuncionalidadeVo, idUsuario, idUsuarioCampus, idUsuarioModulo, idUsuarioSubmodulo, idModulo, idSubmodulo);

                Commit();
            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
        }


        public List<UsuarioFuncionalidadeVO> Selecionar(UsuarioFuncionalidadeVO usuarioFuncionalidadeVo = null, int top = 0, bool detalhar = false)
        {
            List<UsuarioFuncionalidadeVO> lslUsuarioFuncionalidade = null;
            try
            {
                var usuarioFuncionalidadeDAO = new UsuarioFuncionalidadeDAO(GetSqlCommand());
                lslUsuarioFuncionalidade = usuarioFuncionalidadeDAO.Selecionar(usuarioFuncionalidadeVo, top);

            }
            catch (Exception e)
            {
                throw e;
            }
            return lslUsuarioFuncionalidade;
        }

        public UsuarioFuncionalidadeVO Consultar(UsuarioFuncionalidadeVO usuarioFuncionalidadeVo)
        {
            UsuarioFuncionalidadeVO usuarioFuncionalidadeVO = null;
            try
            {
                var usuarioFuncionalidadeDAO = new UsuarioFuncionalidadeDAO(GetSqlCommand());
                usuarioFuncionalidadeVO = usuarioFuncionalidadeDAO.Consultar(usuarioFuncionalidadeVo);
            }
            catch (Exception e)
            {
                throw e;
            }
            return usuarioFuncionalidadeVO;
        }


        public List<UsuarioFuncionalidadeVO> AutenticarFuncionalidades(string urlSubModulo, long idUsuario, long idCampus, bool acessoExterno)
        {
            try
            {
                var usuarioFuncionalidadeDAO = new UsuarioFuncionalidadeDAO(GetSqlCommand());
                return usuarioFuncionalidadeDAO.AutenticarFuncionalidade(urlSubModulo, idUsuario, idCampus, acessoExterno);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<UsuarioFuncionalidadeVO> ListarFuncionalidadesUsuario(long idUsuario, long idCampus)
        {
            List<UsuarioFuncionalidadeVO> lslUsuarioFuncionalidade = null;
            try
            {
                var usuarioFuncionalidadeDAO = new UsuarioFuncionalidadeDAO(GetSqlCommand());
                lslUsuarioFuncionalidade = usuarioFuncionalidadeDAO.ListarFuncionalidadesUsuario(idUsuario, idCampus);
            }
            catch (Exception e)
            {
                throw e;
            }
            return lslUsuarioFuncionalidade;
        }

        public List<UsuarioFuncionalidadeVO> Listar(UsuarioFuncionalidadeVO usuarioFuncionalidadeVo = null, bool detalhar = false)
        {
            List<UsuarioFuncionalidadeVO> lslUsuarioFuncionalidade = null;
            try
            {
                var usuarioFuncionalidadeDAO = new UsuarioFuncionalidadeDAO(GetSqlCommand());
                lslUsuarioFuncionalidade = usuarioFuncionalidadeDAO.Listar(usuarioFuncionalidadeVo);
            }
            catch (Exception e)
            {
                throw e;
            }
            return lslUsuarioFuncionalidade;
        }
    }
}
