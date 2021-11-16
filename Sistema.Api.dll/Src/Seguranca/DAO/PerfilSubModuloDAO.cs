using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Seguranca.VO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Sistema.Api.dll.Src.Seguranca.DAO
{
    public class PerfilSubModuloDAO : AbstractDAO, IDAO<PerfilSubModuloVO>
    {
        public PerfilSubModuloDAO(SqlCommand sqlComm)
            : base(sqlComm)
        {
        }

        public long Inserir(PerfilSubModuloVO objVO)
        {
            try
            {
                objSbInsert = new StringBuilder();
                long IdPerfilSubModulo = GetCodigoSequece("DBAthon.dbo.SeqPerfilSubModulo");
                objSbInsert.AppendLine(@"INSERT INTO  DBAthon.dbo.PerfilSubModulo                  ");
                objSbInsert.AppendLine(@"(                                                                                             ");
                objSbInsert.AppendLine(@"             IdPerfilSubModulo          ");
                objSbInsert.AppendLine(@"          ,  IdPerfilModulo             ");
                objSbInsert.AppendLine(@"          ,  IdSubModulo                ");
                objSbInsert.AppendLine(@"          ,  Ativar                     ");
                objSbInsert.AppendLine(@"          ,  AcessoExterno                     ");
                objSbInsert.AppendLine(@")");
                objSbInsert.AppendLine(@"VALUES ");
                objSbInsert.AppendLine(@"(");
                objSbInsert.AppendLine(@"             @IdPerfilSubModulo         ");
                objSbInsert.AppendLine(@"          ,  @IdPerfilModulo            ");
                objSbInsert.AppendLine(@"          ,  @IdSubModulo               ");
                objSbInsert.AppendLine(@"          ,  @Ativar                    ");
                objSbInsert.AppendLine(@"          ,  @AcessoExterno                     ");
                objSbInsert.AppendLine(@")                                       ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbInsert.ToString();
                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdPerfilSubModulo", SqlDbType.Int).Value = IdPerfilSubModulo;
                GetSqlCommand().Parameters.Add("IdPerfilModulo", SqlDbType.Int).Value = objVO.PerfilModulo.Id;
                GetSqlCommand().Parameters.Add("IdSubModulo", SqlDbType.Int).Value = objVO.SubModulo.Id;
                GetSqlCommand().Parameters.Add("Ativar", SqlDbType.VarChar).Value = objVO.Ativar;
                GetSqlCommand().Parameters.Add("AcessoExterno", SqlDbType.VarChar).Value = objVO.AcessoExterno;

                GetSqlCommand().ExecuteNonQuery();



                return IdPerfilSubModulo;

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

        public long Alterar(PerfilSubModuloVO objVO, string where = null)
        {
            try
            {
                objSbUpdate = new StringBuilder();
                objSbUpdate.AppendLine("UPDATE DBAthon.dbo.PerfilSubModulo ");
                objSbUpdate.AppendLine("   SET IdPerfilModulo = @IdPerfilModulo ");
                objSbUpdate.AppendLine("     , IdSubModulo = @IdSubModulo");
                objSbUpdate.AppendLine("     , Ativar = @Ativar");
                objSbUpdate.AppendLine("     , AcessoExterno = @AcessoExterno");

                if (where == null)
                {
                    objSbUpdate.AppendLine(" WHERE IdPerfilSubModulo = @IdPerfilSubModulo");
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
                        GetSqlCommand().Parameters.Add("IdPerfilSubModulo", SqlDbType.Int).Value = objVO.Id;

                    GetSqlCommand().Parameters.Add("IdPerfilModulo", SqlDbType.Int).Value = objVO.PerfilModulo.Id;
                    GetSqlCommand().Parameters.Add("IdSubModulo", SqlDbType.Int).Value = objVO.SubModulo.Id;
                    GetSqlCommand().Parameters.Add("Ativar", SqlDbType.VarChar).Value = objVO.Ativar;
                    GetSqlCommand().Parameters.Add("AcessoExterno", SqlDbType.VarChar).Value = objVO.AcessoExterno;
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

        public long DesabilitarsSubModulos(PerfilSubModuloVO objVO)
        {
            try
            {
                objSbUpdate = new StringBuilder();
                objSbUpdate.AppendLine("UPDATE DBAthon.dbo.PerfilSubModulo      ");
                objSbUpdate.AppendLine("   SET Ativar = 0                           ");
                objSbUpdate.AppendLine("     , AcessoExterno = 0                           ");
                objSbUpdate.AppendLine(" WHERE IdPerfilModulo = @IdPerfilModulo     ");

                if (objVO != null)
                {
                    GetSqlCommand().CommandText = "";
                    GetSqlCommand().CommandText = objSbUpdate.ToString();
                    GetSqlCommand().Parameters.Clear();
                    GetSqlCommand().Parameters.Add("IdPerfilModulo", SqlDbType.Int).Value = objVO.PerfilModulo.Id;

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

        public void Deletar(PerfilSubModuloVO objVO)
        {
            try
            {
                objSbDelete = new StringBuilder();
                objSbDelete.AppendLine("DELETE FROM DBAthon.dbo.PerfilSubModulo ");
                objSbDelete.AppendLine(" WHERE IdPerfilSubModulo = @IdPerfilSubModulo");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbDelete.ToString();
                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdPerfilSubModulo", SqlDbType.Int).Value = objVO.Id;

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

        public List<PerfilSubModuloVO> Listar(PerfilSubModuloVO objVO)
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

        public List<PerfilSubModuloVO> Selecionar(string sql)
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

        public PerfilSubModuloVO Consultar(PerfilSubModuloVO objVO)
        {
            try
            {
                List<PerfilSubModuloVO> lstPerfilSubModuloVO = Selecionar(objVO);
                return lstPerfilSubModuloVO.Count > 0 ? (PerfilSubModuloVO)lstPerfilSubModuloVO.ToArray().GetValue(0) : null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<PerfilSubModuloVO> GetLista()
        {
            PerfilSubModuloVO perfilSubModulo = null;
            List<PerfilSubModuloVO> lstPerfilSubModuloVO = null;
            try
            {
                lstPerfilSubModuloVO = new List<PerfilSubModuloVO>();
                while (GetSqlDataReader().Read())
                {
                    perfilSubModulo = new PerfilSubModuloVO();


                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdPerfilSubModulo"))))
                        perfilSubModulo.Id = Convert.ToInt64(GetSqlDataReader()["IdPerfilSubModulo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("AtivarPerfilSubModulo"))))
                        perfilSubModulo.Ativar = Convert.ToBoolean(GetSqlDataReader()["IdPerfilSubModulo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Nome"))))
                        perfilSubModulo.SubModulo.Nome = Convert.ToString(GetSqlDataReader()["Nome"]);


                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdSubModulo"))))
                        perfilSubModulo.SubModulo.Id = Convert.ToInt64(GetSqlDataReader()["IdSubModulo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("AcessoExterno"))))
                        perfilSubModulo.AcessoExterno = Convert.ToBoolean(GetSqlDataReader()["AcessoExterno"]);

                    lstPerfilSubModuloVO.Add(perfilSubModulo);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return lstPerfilSubModuloVO;
        }

        public List<PerfilSubModuloVO> Selecionar(PerfilSubModuloVO objVO, int top = 0)
        {

            List<PerfilSubModuloVO> lstPerfilSubModulo = null;

            try
            {
                objSbSelect = new StringBuilder();
                lstPerfilSubModulo = new List<PerfilSubModuloVO>();

                string varTop = "";
                if (top > 0)
                {
                    varTop = top.ToString();
                }

                objSbSelect.AppendLine(@"SELECT ");
                objSbSelect.AppendLine(@"        PerfilSubModulo.IdPerfilSubModulo");
                objSbSelect.AppendLine(@"      , PerfilSubModulo.IdPerfilModulo");
                objSbSelect.AppendLine(@"      , PerfilSubModulo.IdSubModulo");
                objSbSelect.AppendLine(@"      , PerfilSubModulo.AcessoExterno");
                objSbSelect.AppendLine(@"      , PerfilSubModulo.Ativar AS AtivarPerfilSubModulo");
                objSbSelect.AppendLine(@"      , PerfilModulo.IdModulo");
                objSbSelect.AppendLine(@"      , PerfilModulo.Ativar AS AtivarPerfilModulo");
                objSbSelect.AppendLine(@"      , PerfilModulo.IdPerfil");
                objSbSelect.AppendLine(@"      , SubModulo.Nome");
                objSbSelect.AppendLine(@"  FROM DBAthon.dbo.PerfilSubModulo");
                objSbSelect.AppendLine(@"INNER JOIN DBAthon.dbo.PerfilModulo");
                objSbSelect.AppendLine(@"        ON DBAthon.dbo.PerfilModulo.IdPerfilModulo = DBAthon.dbo.PerfilSubModulo.IdPerfilModulo");
                objSbSelect.AppendLine(@"INNER JOIN DBAthon.dbo.SubModulo");
                objSbSelect.AppendLine(@"        ON DBAthon.dbo.SubModulo.IdSubModulo = DBAthon.dbo.PerfilSubModulo.IdSubModulo");
                objSbSelect.AppendLine(@"WHERE 1 = 1                                                               ");

                if (objVO != null)
                {
                    GetSqlCommand().Parameters.Clear();
                    if (objVO.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.PerfilSubModulo.IdPerfilSubModulo = @IdPerfilSubModulo");
                        GetSqlCommand().Parameters.Add("IdPerfilSubModulo", SqlDbType.Int).Value = objVO.Id;
                    }
                    if (objVO.PerfilModulo.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.PerfilSubModulo.IdPerfilModulo = @IdPerfilModulo");
                        GetSqlCommand().Parameters.Add("IdPerfilModulo", SqlDbType.Int).Value = objVO.PerfilModulo.Id;
                    }
                    if (objVO.SubModulo.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.PerfilSubModulo.IdSubModulo = @IdSubModulo");
                        GetSqlCommand().Parameters.Add("IdSubModulo", SqlDbType.Int).Value = objVO.SubModulo.Id;
                    }
                    if (objVO.PerfilModulo.Modulo.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.PerfilSubModulo.IdModulo = @IdModulo");
                        GetSqlCommand().Parameters.Add("IdModulo", SqlDbType.Int).Value = objVO.PerfilModulo.Modulo.Id;
                    }
                    if (objVO.PerfilModulo.Perfil.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND PerfilModulo.IdPerfil = @IdPerfil");
                        GetSqlCommand().Parameters.Add("IdPerfil", SqlDbType.Int).Value = objVO.PerfilModulo.Perfil.Id;
                    }
                    if (objVO.Ativar != null)
                    {
                        objSbSelect.AppendLine(@" AND PerfilSubModulo.Ativar = @AtivarPerfilSubModulo");
                        GetSqlCommand().Parameters.Add("AtivarPerfilSubModulo", SqlDbType.Bit).Value = objVO.Ativar;
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