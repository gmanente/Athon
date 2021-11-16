using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Seguranca.VO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Sistema.Api.dll.Src.Seguranca.DAO
{
    public class PerfilModuloDAO : AbstractDAO, IDAO<PerfilModuloVO>
    {
        public PerfilModuloDAO(SqlCommand sqlComm)
            : base(sqlComm)
        {
        }

        public long Inserir(PerfilModuloVO objVO)
        {
            try
            {
                objSbInsert = new StringBuilder();
                long IdPerfilModulo = GetCodigoSequece("DBAthon.dbo.SeqPerfilModulo");
                objSbInsert.AppendLine(@"INSERT INTO  DBAthon.dbo.PerfilModulo       ");
                objSbInsert.AppendLine(@"(                                                                                             ");
                objSbInsert.AppendLine(@"             IdPerfilModulo      ");
                objSbInsert.AppendLine(@"          ,  IdModulo            ");
                objSbInsert.AppendLine(@"          ,  Ativar              ");
                objSbInsert.AppendLine(@"          ,  IdPerfil            ");
                objSbInsert.AppendLine(@"          ,  AcessoExterno           ");
                objSbInsert.AppendLine(@")");
                objSbInsert.AppendLine(@"VALUES ");
                objSbInsert.AppendLine(@"(");
                objSbInsert.AppendLine(@"             @IdPerfilModulo     ");
                objSbInsert.AppendLine(@"          ,  @IdModulo           ");
                objSbInsert.AppendLine(@"          ,  @Ativar             ");
                objSbInsert.AppendLine(@"          ,  @IdPerfil           ");
                objSbInsert.AppendLine(@"          ,  @AcessoExterno           ");
                objSbInsert.AppendLine(@")");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbInsert.ToString();
                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdPerfilModulo", SqlDbType.Int).Value = IdPerfilModulo;
                GetSqlCommand().Parameters.Add("IdModulo", SqlDbType.Int).Value = objVO.Modulo.Id;
                GetSqlCommand().Parameters.Add("Ativar", SqlDbType.VarChar).Value = objVO.Ativar;
                GetSqlCommand().Parameters.Add("IdPerfil", SqlDbType.Int).Value = objVO.Perfil.Id;
                GetSqlCommand().Parameters.Add("AcessoExterno", SqlDbType.Bit).Value = objVO.AcessoExterno;
                GetSqlCommand().ExecuteNonQuery();
                return IdPerfilModulo;

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (objSbInsert != null)
                {
                    objSbInsert = null;
                }
            }
        }

        public long Alterar(PerfilModuloVO objVO, string where = null)
        {
            try
            {
                objSbUpdate = new StringBuilder();
                objSbUpdate.AppendLine("UPDATE DBAthon.dbo.PerfilModulo ");
                objSbUpdate.AppendLine("   SET IdModulo = @IdModulo ");
                objSbUpdate.AppendLine("     , Ativar = @Ativar");
                objSbUpdate.AppendLine("     , IdPerfil = @IdPerfil");
                objSbUpdate.AppendLine("     , AcessoExterno = @AcessoExterno");


                if (where == null)
                {
                    objSbUpdate.AppendLine(" WHERE IdPerfilModulo = @IdPerfilModulo");
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
                    if (where == null)
                        GetSqlCommand().Parameters.Add("IdPerfilModulo", SqlDbType.Int).Value = objVO.Id;
                    GetSqlCommand().Parameters.Add("IdModulo", SqlDbType.Int).Value = objVO.Modulo.Id;
                    GetSqlCommand().Parameters.Add("Ativar", SqlDbType.VarChar).Value = objVO.Ativar;
                    GetSqlCommand().Parameters.Add("IdPerfil", SqlDbType.Int).Value = objVO.Perfil.Id;
                    GetSqlCommand().Parameters.Add("AcessoExterno", SqlDbType.Bit).Value = objVO.AcessoExterno;

                }
                GetSqlCommand().ExecuteNonQuery();
                return objVO.Id;

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (objSbUpdate != null)
                {
                    objSbUpdate = null;
                }
            }
        }

        public void Deletar(PerfilModuloVO objVO)
        {
            try
            {
                objSbDelete = new StringBuilder();
                objSbDelete.AppendLine("DELETE FROM DBAthondbo.PerfilModulo ");
                objSbDelete.AppendLine(" WHERE IdPerfilModulo = @IdPerfilModulo");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbDelete.ToString();
                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdPerfilModulo", SqlDbType.Int).Value = objVO.Id;

                GetSqlCommand().ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (objSbDelete != null)
                {
                    objSbDelete = null;
                }
            }
        }

        public long DesabilitarModulos(PerfilModuloVO objVO)
        {
            try
            {
                objSbUpdate = new StringBuilder();
                objSbUpdate.AppendLine("UPDATE DBAthon.dbo.PerfilModulo ");
                objSbUpdate.AppendLine("   SET Ativar = 0                   ");
                objSbUpdate.AppendLine("     , AcessoExterno = 0            ");
                objSbUpdate.AppendLine(" WHERE IdPerfil = @IdPerfil         ");

                if (objVO != null)
                {
                    GetSqlCommand().CommandText = "";
                    GetSqlCommand().CommandText = objSbUpdate.ToString();
                    GetSqlCommand().Parameters.Clear();
                    GetSqlCommand().Parameters.Add("IdPerfil", SqlDbType.Int).Value = objVO.Perfil.Id;

                }
                GetSqlCommand().ExecuteNonQuery();
                return objVO.Id;

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (objSbUpdate != null)
                {
                    objSbUpdate = null;
                }
            }
        }

        public List<PerfilModuloVO> Listar(PerfilModuloVO objVO)
        {
            try
            {
                return Selecionar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<PerfilModuloVO> Selecionar(string sql)
        {
            try
            {
                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = sql;
                return GetLista();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (objSbSelect != null)
                {
                    objSbSelect = null;
                }
                Close();

            }

        }

        public PerfilModuloVO Consultar(PerfilModuloVO objVO)
        {
            try
            {
                List<PerfilModuloVO> lstPerfilModuloVO = Selecionar(objVO);
                return lstPerfilModuloVO.Count > 0 ? (PerfilModuloVO)lstPerfilModuloVO.ToArray().GetValue(0) : null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<PerfilModuloVO> GetLista()
        {
            PerfilModuloVO perfilModulo = null;
            List<PerfilModuloVO> lstPerfilModuloVO = null;
            try
            {
                lstPerfilModuloVO = new List<PerfilModuloVO>();
                while (GetSqlDataReader().Read())
                {
                    perfilModulo = new PerfilModuloVO();


                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdPerfilModulo"))))
                        perfilModulo.Id = Convert.ToInt64(GetSqlDataReader()["IdPerfilModulo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Ativar"))))
                        perfilModulo.Ativar = Convert.ToBoolean(GetSqlDataReader()["Ativar"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Descricao"))))
                        perfilModulo.Perfil.Descricao = Convert.ToString(GetSqlDataReader()["Descricao"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Nome"))))
                        perfilModulo.Modulo.Nome = Convert.ToString(GetSqlDataReader()["Nome"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdModulo"))))
                        perfilModulo.Modulo.Id = Convert.ToInt64(GetSqlDataReader()["IdModulo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("AcessoExterno"))))
                        perfilModulo.AcessoExterno = Convert.ToBoolean(GetSqlDataReader()["AcessoExterno"]);

                    lstPerfilModuloVO.Add(perfilModulo);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return lstPerfilModuloVO;
        }

        public List<PerfilModuloVO> Selecionar(PerfilModuloVO objVO, int top = 0)
        {
            List<PerfilModuloVO> lstPerfilModulo = null;

            try
            {
                objSbSelect = new StringBuilder();
                lstPerfilModulo = new List<PerfilModuloVO>();

                string varTop = "";
                if (top > 0)
                {
                    varTop = top.ToString();
                }

                objSbSelect.AppendLine(@"SELECT                                                 ");
                objSbSelect.AppendLine(@"        PerfilModulo.IdPerfilModulo                    ");
                objSbSelect.AppendLine(@"      , PerfilModulo.IdModulo                                                      ");
                objSbSelect.AppendLine(@"      , PerfilModulo.AcessoExterno                                                      ");
                objSbSelect.AppendLine(@"      , PerfilModulo.Ativar                                                        ");
                objSbSelect.AppendLine(@"      , PerfilModulo.IdPerfil                                                      ");
                objSbSelect.AppendLine(@"	   , Perfil.Descricao                                                            ");
                objSbSelect.AppendLine(@"	   , Modulo.Nome                                                                 ");
                objSbSelect.AppendLine(@"  FROM DBAthon.dbo.PerfilModulo");
                objSbSelect.AppendLine(@"INNER JOIN DBAthon.dbo.Perfil");
                objSbSelect.AppendLine(@"        ON DBAthon.dbo.Perfil.IdPerfil = DBAthon.dbo.PerfilModulo.IdPerfil");
                objSbSelect.AppendLine(@"INNER JOIN DBAthon.dbo.Modulo");
                objSbSelect.AppendLine(@"        ON DBAthon.dbo.Modulo.IdModulo = DBAthon.dbo.PerfilModulo.IdModulo ");
                objSbSelect.AppendLine(@"WHERE 1 = 1                                                               ");

                if (objVO != null)
                {
                    GetSqlCommand().Parameters.Clear();
                    if (objVO.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.PerfilModulo.IdPerfilModulo = @IdPerfilModulo");
                        GetSqlCommand().Parameters.Add("IdPerfilModulo", SqlDbType.Int).Value = objVO.Id;
                    }
                    if (objVO.Modulo.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.PerfilModulo.IdModulo = @IdModulo");
                        GetSqlCommand().Parameters.Add("IdModulo", SqlDbType.Int).Value = objVO.Modulo.Id;
                    }
                    if (objVO.Perfil.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.PerfilModulo.IdPerfil = @IdPerfil");
                        GetSqlCommand().Parameters.Add("IdPerfil", SqlDbType.Int).Value = objVO.Perfil.Id;
                    }
                    if (objVO.Ativar != null)
                    {
                        objSbSelect.AppendLine(@" AND PerfilModulo.Ativar = @Ativar");
                        GetSqlCommand().Parameters.Add("Ativar", SqlDbType.Bit).Value = objVO.Ativar;
                    }
                    if (!string.IsNullOrEmpty(objVO.Perfil.Descricao))
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.PerfilModulo.Descricao = @Descricao");
                        GetSqlCommand().Parameters.Add("Descricao", SqlDbType.VarChar).Value = objVO.Perfil.Descricao;
                    }
                    if (!string.IsNullOrEmpty(objVO.Modulo.Nome))
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.PerfilModulo.Nome = @Nome");
                        GetSqlCommand().Parameters.Add("Nome", SqlDbType.VarChar).Value = objVO.Modulo.Nome;
                    }
                }

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();
                return GetLista();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objSbSelect != null)
                {
                    objSbSelect = null;
                }
                Close();
            }
        }
    }
}