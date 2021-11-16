using Sistema.Api.dll.Repositorio.Util.Componentes;
using Sistema.Api.dll.Src.Comum.VO;
using Sistema.Api.dll.Src.Repositorio.DAO;
using Sistema.Api.dll.Src.Seguranca.VO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Sistema.Api.dll.Src.Repositorio.BE
{
    public class ConsultaBE : AbstractBE
    {
        public ConsultaBE()
           : base()
        {}

        public ConsultaBE(SqlCommand sqlConn)
           : base(sqlConn)
        {}

        /// <summary>
        /// GetSelectField
        /// </summary>
        /// <param name="arrStr"></param>
        /// <param name="filtroCampoVo"></param>
        /// <returns></returns>
        public SelectField GetSelectField(string[] arrStr, ConsultaCampoVO filtroCampoVo)
        {
            try
            {
                var consultaDAO = new ConsultaDAO(GetSqlCommand());
                return consultaDAO.GetSelectField(arrStr, filtroCampoVo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// GetIdSubModulo
        /// </summary>
        /// <param name="urlSubModulo"></param>
        /// <returns></returns>
        public long GetIdSubModulo(string urlSubModulo)
        {
            try
            {
                ConsultaDAO dao = new ConsultaDAO(GetSqlCommand());

                return dao.GetIdSubModulo(urlSubModulo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// GetIdModulo
        /// </summary>
        /// <param name="idSubModulo"></param>
        /// <returns></returns>
        public long GetIdModulo(long idSubModulo)
        {
            try
            {
                var consultaDAO = new ConsultaDAO(GetSqlCommand());
                return consultaDAO.GetIdModulo(idSubModulo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// GetUsuario
        /// </summary>
        /// <param name="cpfUsuario"></param>
        /// <returns></returns>
        public List<UsuarioVO> GetUsuario(long cpfUsuario)
        {
            try
            {
                var consultaDAO = new ConsultaDAO(GetSqlCommand());
                return consultaDAO.GetUsuario(cpfUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// GetIdProfessor
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        public long GetIdProfessor(long idUsuario)
        {
            try
            {
                var consultaDAO = new ConsultaDAO(GetSqlCommand());
                return consultaDAO.GetIdProfessor(idUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// GetLoginSenhaUsuario
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        public string[] GetLoginSenhaUsuario(long idUsuario)
        {
            try
            {
                var consultaDAO = new ConsultaDAO(GetSqlCommand());
                return consultaDAO.GetLoginSenhaUsuario(idUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}