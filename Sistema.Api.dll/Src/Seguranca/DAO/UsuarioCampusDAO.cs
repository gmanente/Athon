using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Sistema.Api.dll.Src.Comum.VO;
using Sistema.Api.dll.Src.Seguranca.VO;

namespace Sistema.Api.dll.Src.Seguranca.DAO
{
    public class UsuarioCampusDAO : AbstractDAO
    {
        public UsuarioCampusDAO(SqlCommand sqlConn) : base(sqlConn)
        {
        }


        /// <summary>
        /// Inserir
        /// </summary>
        /// <param name="objVO"></param>
        /// <returns></returns>
        public long Inserir(UsuarioCampusVO objVO)
        {
            try
            {
                long idUsuarioCampus = GetCodigoSequece("DBAthon.dbo.SeqUsuarioCampus");

                objSbInsert = new StringBuilder();

                objSbInsert.AppendLine(@"INSERT INTO DBAthon.dbo.UsuarioCampus  ");
                objSbInsert.AppendLine(@"(                                          ");
                objSbInsert.AppendLine(@"             IdUsuarioCampus               ");
                objSbInsert.AppendLine(@"           , IdUsuario                     ");
                objSbInsert.AppendLine(@"           , IdCampus                      ");
                objSbInsert.AppendLine(@"           , Ativar                        ");
                objSbInsert.AppendLine(@"           , AcessoExterno                 ");
                objSbInsert.AppendLine(@" )                                         ");
                objSbInsert.AppendLine(@"     VALUES                                ");
                objSbInsert.AppendLine(@"(                                          ");
                objSbInsert.AppendLine(@"             @IdUsuarioCampus              ");
                objSbInsert.AppendLine(@"           , @IdUsuario                    ");
                objSbInsert.AppendLine(@"           , @IdCampus                     ");
                objSbInsert.AppendLine(@"           , @Ativar                       ");
                objSbInsert.AppendLine(@"           , @AcessoExterno                ");
                objSbInsert.AppendLine(@" )                                         ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbInsert.ToString();

                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdUsuarioCampus", SqlDbType.Int).Value = idUsuarioCampus;
                GetSqlCommand().Parameters.Add("IdUsuario", SqlDbType.Int).Value = objVO.Usuario.Id;
                GetSqlCommand().Parameters.Add("IdCampus", SqlDbType.Int).Value = objVO.Campus.Id;
                GetSqlCommand().Parameters.Add("Ativar", SqlDbType.Bit).Value = objVO.Ativar;
                GetSqlCommand().Parameters.Add("AcessoExterno", SqlDbType.Bit).Value = objVO.AcessoExterno;

                GetSqlCommand().ExecuteNonQuery();

                return idUsuarioCampus;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (objSbInsert != null)
                    objSbInsert = null;
            }
        }


        /// <summary>
        /// Alterar
        /// </summary>
        /// <param name="objVO"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public long Alterar(UsuarioCampusVO objVO, string where = null)
        {
            try
            {
                objSbUpdate = new StringBuilder();

                objSbUpdate.AppendLine(@"UPDATE DBAthon.dbo.UsuarioCampus ");
                objSbUpdate.AppendLine(@"  SET                                ");
                objSbUpdate.AppendLine(@"     IdUsuario = @IdUsuario          ");
                objSbUpdate.AppendLine(@"    ,IdCampus  = @IdCampus           ");
                objSbUpdate.AppendLine(@"    ,Ativar    = @Ativar             ");
                objSbUpdate.AppendLine(@"    ,AcessoExterno  = @AcessoExterno ");

                if (where == null)
                {
                    objSbUpdate.AppendLine(@"WHERE IdUsuario = @IdUsuario     ");
                    objSbUpdate.AppendLine(@"AND  IdCampus = @IdCampus        ");
                }
                else
                {
                    objSbUpdate.AppendLine(where);
                }

                if (objVO != null)
                {
                    GetSqlCommand().CommandText = "";
                    GetSqlCommand().CommandText = objSbUpdate.ToString();
                    GetSqlCommand().Parameters.Clear();
                    GetSqlCommand().Parameters.Add("IdUsuario", SqlDbType.Int).Value = objVO.Usuario.Id;
                    GetSqlCommand().Parameters.Add("IdCampus", SqlDbType.Int).Value = objVO.Campus.Id;
                    GetSqlCommand().Parameters.Add("Ativar", SqlDbType.Bit).Value = objVO.Ativar;
                    GetSqlCommand().Parameters.Add("AcessoExterno", SqlDbType.Bit).Value = objVO.AcessoExterno;

                    GetSqlCommand().ExecuteNonQuery();
                }
                return objVO.Id;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (objSbUpdate != null)
                    objSbUpdate = null;
            }
        }


        /// <summary>
        /// Deletar
        /// </summary>
        /// <param name="objVO"></param>
        public void Deletar(UsuarioCampusVO objVO)
        {
            try
            {
                objSbDelete = new StringBuilder();

                objSbDelete.AppendLine(" DELETE FROM DBAthon.dbo.UsuarioCampus WHERE 1 = 1 ");

                GetSqlCommand().Parameters.Clear();

                if (objVO.Campus.Id > 0)
                {
                    objSbDelete.AppendLine(" AND IdCampus = @IdCampus ");

                    GetSqlCommand().Parameters.Add("IdCampus", SqlDbType.Int).Value = objVO.Campus.Id;
                }

                if (objVO.Id > 0)
                {
                    objSbDelete.AppendLine(" AND IdUsuarioCampus = @IdUsuarioCampus ");

                    GetSqlCommand().Parameters.Add("IdUsuarioCampus", SqlDbType.Int).Value = objVO.Id;
                }
                else
                {
                    objSbDelete.AppendLine(" AND IdUsuario = @IdUsuario ");

                    GetSqlCommand().Parameters.Add("IdUsuario", SqlDbType.Int).Value = objVO.Usuario.Id;
                }

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbDelete.ToString();

                GetSqlCommand().ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                throw e;
            }
            finally
            {
                if (objSbDelete != null)
                    objSbDelete = null;
            }
        }
            


        /// <summary>
        /// Selecionar
        /// </summary>
        /// <param name="objVO"></param>
        /// <param name="top"></param>
        /// <returns></returns>
        public List<UsuarioCampusVO> Selecionar(UsuarioCampusVO objVO = null, int top = 0)
        {
            try
            {
                objSbSelect = new StringBuilder();

                objSbSelect.AppendLine(@"
                    SELECT   Usuario.IdUsuario
                           , UsuarioCampus.IdUsuarioCampus
                           , UsuarioCampus.IdCampus
                           , UsuarioCampus.Ativar
                           , UsuarioCampus.AcessoExterno

                           , Campus.Nome
                           , Campus.Sigla    AS CampusSigla
                           , Usuario.Nome    AS NomeUsuario
                           , Usuario.DataCadastro
                           , Usuario.Email
                           , Usuario.NomeLogin
                           , Campus.IpFixo
                           , SS.DataTermino
                           , SS.Senha
                           , Empresa.Nome AS NomeEmpresa

                        FROM DBAthon.dbo.UsuarioCampus  WITH (NOLOCK)

                  INNER JOIN DBAthon.dbo.Campus             WITH (NOLOCK) ON Campus.IdCampus = UsuarioCampus.IdCampus

                  INNER JOIN DBAthon.dbo.Usuario        WITH (NOLOCK) ON Usuario.IdUsuario = UsuarioCampus.IdUsuario

                  INNER JOIN DBAthon.dbo.Empresa            WITH (NOLOCK) ON Empresa.IdEmpresa = Campus.IdEmpresa

                  INNER JOIN (SELECT  UsuarioSenha.DataTermino
                                    , UsuarioSenha.Senha
                                    , UsuarioSenha.IdUsuario
                                    , UsuarioSenha.IdUsuarioSenha
                                 FROM DBAthon.dbo.UsuarioSenha WITH (NOLOCK)) AS SS
                          ON SS.IdUsuario = Usuario.IdUsuario
                         AND SS.IdUsuarioSenha = (SELECT MAX(US.IdUsuarioSenha) FROM DBAthon.dbo.UsuarioSenha US  WITH (NOLOCK)
                                                   WHERE US.IdUsuario = Usuario.IdUsuario)

                       WHERE 1 = 1 ");

                GetSqlCommand().Parameters.Clear();

                if (objVO != null)
                {
                    if (objVO.Id > 0)
                    {
                        objSbSelect.AppendLine(" AND IdUsuarioCampus = @IdUsuarioCampus ");
                        GetSqlCommand().Parameters.Add("IdUsuarioCampus", SqlDbType.Int).Value = objVO.Id;
                    }

                    if (objVO.Usuario.Id > 0)
                    {
                        objSbSelect.AppendLine(" AND UsuarioCampus.IdUsuario = @IdUsuario ");
                        GetSqlCommand().Parameters.Add("IdUsuario", SqlDbType.Int).Value = objVO.Usuario.Id;
                    }

                    if (objVO.Campus.Id > 0)
                    {
                        objSbSelect.AppendLine(" AND UsuarioCampus.IdCampus = @IdCampus ");
                        GetSqlCommand().Parameters.Add("IdCampus", SqlDbType.Int).Value = objVO.Campus.Id;
                    }

                    if (objVO.Ativar != null)
                    {
                        objSbSelect.AppendLine(" AND UsuarioCampus.Ativar  = @Ativar ");
                        GetSqlCommand().Parameters.Add("Ativar", SqlDbType.Bit).Value = objVO.Ativar;
                    }

                    if (objVO.AcessoExterno != null)
                    {
                        objSbSelect.AppendLine(" AND UsuarioCampus.AcessoExterno = IIF(@AcessoExterno = 0 , UsuarioCampus.AcessoExterno, 1) ");
                        GetSqlCommand().Parameters.Add("AcessoExterno", SqlDbType.Bit).Value = objVO.AcessoExterno;
                    }

                    if (!string.IsNullOrEmpty(objVO.Usuario.Cpf))
                    {
                        objSbSelect.AppendLine(" AND Usuario.Cpf  = @Cpf ");
                        GetSqlCommand().Parameters.Add("Cpf", SqlDbType.VarChar).Value = objVO.Usuario.Cpf;
                    }
                }

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();

                var lst = new List<UsuarioCampusVO>();

                while (GetSqlDataReader().Read())
                {
                    var item = new UsuarioCampusVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdUsuarioCampus"))))
                        item.Id = Convert.ToInt32(GetSqlDataReader()["IdUsuarioCampus"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdUsuario"))))
                        item.Usuario.Id = Convert.ToInt32(GetSqlDataReader()["IdUsuario"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdCampus"))))
                        item.Campus.Id = Convert.ToInt32(GetSqlDataReader()["IdCampus"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IpFixo"))))
                        item.Campus.IpFixo = Convert.ToString(GetSqlDataReader()["IpFixo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Nome"))))
                        item.Campus.Nome = Convert.ToString(GetSqlDataReader()["Nome"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("CampusSigla"))))
                        item.Campus.Sigla = Convert.ToString(GetSqlDataReader()["CampusSigla"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("NomeUsuario"))))
                        item.Usuario.Nome = Convert.ToString(GetSqlDataReader()["NomeUsuario"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("DataCadastro"))))
                        item.Usuario.DataCadastro = Convert.ToDateTime(GetSqlDataReader()["DataCadastro"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("NomeLogin"))))
                        item.Usuario.NomeLogin = Convert.ToString(GetSqlDataReader()["NomeLogin"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Email"))))
                        item.Usuario.Email = Convert.ToString(GetSqlDataReader()["Email"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Ativar"))))
                        item.Ativar = Convert.ToBoolean(GetSqlDataReader()["Ativar"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("AcessoExterno"))))
                        item.AcessoExterno = Convert.ToBoolean(GetSqlDataReader()["AcessoExterno"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("DataTermino"))))
                        item.Usuario.UsuarioSenha.DataTermino = Convert.ToDateTime(GetSqlDataReader()["DataTermino"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("NomeEmpresa"))))
                        item.Campus.Empresa.Nome = Convert.ToString(GetSqlDataReader()["NomeEmpresa"]);

                    lst.Add(item);
                }

                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objSbSelect != null)
                    objSbSelect = null;

                Close();
            }
        }


        /// <summary>
        /// Listar
        /// </summary>
        /// <param name="objVO"></param>
        /// <returns></returns>
        public List<UsuarioCampusVO> Listar(UsuarioCampusVO objVO)
        {
            try
            {
                return Selecionar(objVO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Consultar
        /// </summary>
        /// <param name="objVO"></param>
        /// <returns></returns>
        public UsuarioCampusVO Consultar(UsuarioCampusVO objVO)
        {
            try
            {
                var lstVO = Selecionar(objVO);

                return lstVO.Count() > 0 ? (UsuarioCampusVO)lstVO.ToArray().GetValue(0) : null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// ListarCampusModalidade
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <param name="idsModalidades"></param>
        /// <returns></returns>
        public List<CampusVO> ListarCampusModalidade(long idUsuario, string idsModalidades)
        {
            try
            {
                objSbSelect = new StringBuilder();

                objSbSelect.AppendLine(@"
                 SELECT  Campus.IdCampus
                        ,Campus.Nome

                    FROM DBAthon.dbo.UsuarioCampus WITH (NOLOCK)

                    JOIN DBAthon.dbo.Campus WITH (NOLOCK)
                      ON Campus.IdCampus = UsuarioCampus.IdCampus ");

                if (idsModalidades != "")
                {
                    objSbSelect.AppendLine(@"
                     JOIN DBAthon.dbo.GradeConsepe WITH (NOLOCK)
                       ON GradeConsepe.IdGradeConsepe =
                                (select top 1 gc.IdGradeConsepe 
                                  from DBAthon.dbo.GradeConsepe gc WITH (NOLOCK)
                                 where gc.IdCampus = Campus.IdCampus
                                   and gc.IdModalidade in(" + idsModalidades + @") ) ");
                }

                objSbSelect.AppendLine(@"
                    WHERE UsuarioCampus.Ativar = 1
                      AND UsuarioCampus.IdUsuario = @IdUsuario ");

                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdUsuario", SqlDbType.Int).Value = idUsuario;

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();

                var lst = new List<CampusVO>();

                while (GetSqlDataReader().Read())
                {
                    var item = new CampusVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdCampus"))))
                        item.Id = Convert.ToInt32(GetSqlDataReader()["IdCampus"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Nome"))))
                        item.Nome = Convert.ToString(GetSqlDataReader()["Nome"]);

                    lst.Add(item);
                }

                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objSbSelect != null)
                    objSbSelect = null;

                Close();
            }
        }


        /// <summary>
        /// ListarCampusSubModuloModalidade
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <param name="idModulo"></param>
        /// <param name="idSubModulo"></param>
        /// <param name="idsModalidades"></param>
        /// <returns></returns>
        public List<CampusVO> ListarCampusSubModuloModalidade(long idUsuario, long idModulo, long idSubModulo, bool acessoExterno, string idsModalidades)
        {
            try
            {
                objSbSelect = new StringBuilder();

                objSbSelect.AppendLine(@"
                   SELECT   Campus.IdCampus
                           ,Campus.Nome AS NomeCampus
						   ,Empresa.Nome AS NomeEmpresa

                       FROM DBAthon.dbo.UsuarioCampus  WITH (NOLOCK)

                 JOIN DBAthon.dbo.UsuarioPerfil  WITH (NOLOCK)
                   ON UsuarioPerfil.IdUsuarioPerfil =
				                (select top 1 up.IdUsuarioPerfil
                                   from DBAthon.dbo.UsuarioPerfil up WITH (NOLOCK)
				             inner join DBAthon.dbo.PerfilModulo WITH (NOLOCK)
						             on PerfilModulo.IdModulo = @IdModulo
						            and PerfilModulo.IdPerfil = up.IdPerfil 
                                    and PerfilModulo.Ativar = 1
				             inner join DBAthon.dbo.PerfilSubModulo WITH (NOLOCK)
						             on PerfilSubModulo.IdSubModulo = @IdSubModulo
								    and PerfilSubModulo.IdPerfilModulo = PerfilModulo.IdPerfilModulo
                                    and PerfilSubModulo.Ativar = 1
                                    and PerfilSubModulo.AcessoExterno = IIF(@AcessoExterno = 0, PerfilSubModulo.AcessoExterno,1) 
				                  where up.IdUsuarioCampus = UsuarioCampus.IdUsuarioCampus
                                    and GETDATE() between up.DataInicio and up.DataTermino )

                 JOIN DBAthon.dbo.Campus WITH (NOLOCK)
                   ON Campus.IdCampus = UsuarioCampus.IdCampus

				 JOIN DBAthon.dbo.Empresa WITH (NOLOCK)
                   ON Empresa.Idempresa = Campus.IdEmpresa ");

                if (idsModalidades != "")
                {
                    objSbSelect.AppendLine(@"
                        JOIN DBAthon.dbo.GradeConsepe WITH (NOLOCK)
                          ON GradeConsepe.IdGradeConsepe =
                                    (select top 1 gc.IdGradeConsepe 
                                       from DBAthon.dbo.GradeConsepe gc WITH (NOLOCK)
                                      where gc.IdCampus = Campus.IdCampus
                                        and gc.IdModalidade in(" + idsModalidades + @") ) ");
                }

                objSbSelect.AppendLine(@"
                    WHERE UsuarioCampus.Ativar = 1
                      AND UsuarioCampus.IdUsuario = @IdUsuario
                      AND UsuarioCampus.AcessoExterno = IIF(@AcessoExterno = 0, UsuarioCampus.AcessoExterno, 1) ");

                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdUsuario", SqlDbType.Int).Value = idUsuario;
                GetSqlCommand().Parameters.Add("IdModulo", SqlDbType.Int).Value = idModulo;
                GetSqlCommand().Parameters.Add("IdSubModulo", SqlDbType.Int).Value = idSubModulo;
                GetSqlCommand().Parameters.Add("AcessoExterno", SqlDbType.Bit).Value = acessoExterno;

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();

                var lst = new List<CampusVO>();

                while (GetSqlDataReader().Read())
                {
                    var item = new CampusVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdCampus"))))
                        item.Id = Convert.ToInt32(GetSqlDataReader()["IdCampus"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("NomeCampus"))))
                        item.Nome = Convert.ToString(GetSqlDataReader()["NomeCampus"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("NomeEmpresa"))))
                        item.Empresa.Nome = Convert.ToString(GetSqlDataReader()["NomeEmpresa"]);

                    lst.Add(item);
                }

                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objSbSelect != null)
                    objSbSelect = null;

                Close();
            }
        }


        public Dictionary<int, List<UsuarioCampusVO>> Paginar(UsuarioCampusVO objVO)
        {
            Dictionary<int, List<UsuarioCampusVO>> dictionany = null;
            try
            {
                List<UsuarioCampusVO> lstUsuarioCampus;
                dictionany = new Dictionary<int, List<UsuarioCampusVO>>();
                var sbPaginar = new StringBuilder();
                lstUsuarioCampus = Selecionar(objVO);
                dictionany.Add(lstUsuarioCampus.Count(), lstUsuarioCampus);
                return dictionany;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}