using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Seguranca.VO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Sistema.Api.dll.Src.Seguranca.DAO
{
    public class PerfilFuncionalidadeDAO : AbstractDAO, IDAO<PerfilFuncionalidadeVO>
    {
        public PerfilFuncionalidadeDAO(SqlCommand sqlComm)
            : base(sqlComm)
        {
        }

        public long Inserir(PerfilFuncionalidadeVO objVO)
        {
            try
            {
                objSbInsert = new StringBuilder();
                long IdPerfilFuncionalidade = GetCodigoSequece("DBAthon.dbo.SeqPerfilFuncionalidade");
                objSbInsert.AppendLine(@"INSERT INTO  DBAthon.dbo.PerfilFuncionalidade               ");
                objSbInsert.AppendLine(@"(                                                                                             ");
                objSbInsert.AppendLine(@"             IdPerfilFuncionalidade      ");
                objSbInsert.AppendLine(@"          ,  IdPerfilSubModulo           ");
                objSbInsert.AppendLine(@"          ,  IdFuncionalidade            ");
                objSbInsert.AppendLine(@"          ,  Ativar                      ");
                objSbInsert.AppendLine(@"          ,  AcessoExterno              ");
                objSbInsert.AppendLine(@")");
                objSbInsert.AppendLine(@"VALUES ");
                objSbInsert.AppendLine(@"(");
                objSbInsert.AppendLine(@"             @IdPerfilFuncionalidade     ");
                objSbInsert.AppendLine(@"          ,  @IdPerfilSubModulo          ");
                objSbInsert.AppendLine(@"          ,  @IdFuncionalidade           ");
                objSbInsert.AppendLine(@"          ,  @Ativar                     ");
                objSbInsert.AppendLine(@"          ,  @AcessoExterno              ");
                objSbInsert.AppendLine(@")");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbInsert.ToString();
                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdPerfilFuncionalidade", SqlDbType.Int).Value = IdPerfilFuncionalidade;
                GetSqlCommand().Parameters.Add("IdPerfilSubModulo", SqlDbType.Int).Value = objVO.PerfilSubModulo.Id;
                GetSqlCommand().Parameters.Add("IdFuncionalidade", SqlDbType.Int).Value = objVO.Funcionalidade.Id;
                GetSqlCommand().Parameters.Add("Ativar", SqlDbType.VarChar).Value = objVO.Ativar;
                GetSqlCommand().Parameters.Add("AcessoExterno", SqlDbType.VarChar).Value = objVO.AcessoExterno;
                GetSqlCommand().ExecuteNonQuery();
                return IdPerfilFuncionalidade;

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

        public long Alterar(PerfilFuncionalidadeVO objVO, string where = null)
        {
            try
            {
                objSbUpdate = new StringBuilder();
                objSbUpdate.AppendLine("UPDATE DBAthon.dbo.PerfilFuncionalidade ");
                objSbUpdate.AppendLine("   SET IdPerfilSubModulo = @IdPerfilSubModulo ");
                objSbUpdate.AppendLine("     , IdFuncionalidade = @IdFuncionalidade");
                objSbUpdate.AppendLine("     , Ativar = @Ativar");
                objSbUpdate.AppendLine("     , AcessoExterno = @AcessoExterno");

                if (where == null)
                {
                    objSbUpdate.AppendLine(" WHERE IdPerfilFuncionalidade = @IdPerfilFuncionalidade");
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
                        GetSqlCommand().Parameters.Add("IdPerfilFuncionalidade", SqlDbType.Int).Value = objVO.Id;
                    GetSqlCommand().Parameters.Add("IdPerfilSubModulo", SqlDbType.Int).Value = objVO.PerfilSubModulo.Id;
                    GetSqlCommand().Parameters.Add("IdFuncionalidade", SqlDbType.Int).Value = objVO.Funcionalidade.Id;
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

        public long DesabilitarFuncionalidades(PerfilFuncionalidadeVO objVO)
        {
            try
            {
                objSbUpdate = new StringBuilder();
                objSbUpdate.AppendLine("UPDATE DBAthon.dbo.PerfilFuncionalidade                 ");
                objSbUpdate.AppendLine("   SET Ativar = 0                                           ");
                objSbUpdate.AppendLine("     , AcessoExterno = 0                                    ");
                objSbUpdate.AppendLine(" WHERE IdPerfilSubModulo = @IdPerfilSubModulo     ");

                if (objVO != null)
                {
                    GetSqlCommand().CommandText = "";
                    GetSqlCommand().CommandText = objSbUpdate.ToString();
                    GetSqlCommand().Parameters.Clear();
                    GetSqlCommand().Parameters.Add("IdPerfilSubModulo", SqlDbType.Int).Value = objVO.PerfilSubModulo.Id;

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

        public void Deletar(PerfilFuncionalidadeVO objVO)
        {
            try
            {
                objSbDelete = new StringBuilder();
                objSbDelete.AppendLine("DELETE FROM DBAthondbo.PerfilFuncionalidade ");
                objSbDelete.AppendLine(" WHERE IdPerfilFuncionalidade = @IdPerfilFuncionalidade");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbDelete.ToString();
                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdPerfilFuncionalidade", SqlDbType.Int).Value = objVO.Id;

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

        public List<PerfilFuncionalidadeVO> Listar(PerfilFuncionalidadeVO objVO)
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

        public List<PerfilFuncionalidadeVO> Selecionar(string sql)
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

        public PerfilFuncionalidadeVO Consultar(PerfilFuncionalidadeVO objVO)
        {
            try
            {
                List<PerfilFuncionalidadeVO> lstPerfilFuncionalidadeVO = Selecionar(objVO);
                return lstPerfilFuncionalidadeVO.Count > 0 ? (PerfilFuncionalidadeVO)lstPerfilFuncionalidadeVO.ToArray().GetValue(0) : null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<PerfilFuncionalidadeVO> GetLista()
        {
            PerfilFuncionalidadeVO perfilFuncionalidade = null;
            List<PerfilFuncionalidadeVO> lstPerfilFuncionalidadeVO = null;
            try
            {
                lstPerfilFuncionalidadeVO = new List<PerfilFuncionalidadeVO>();
                while (GetSqlDataReader().Read())
                {
                    perfilFuncionalidade = new PerfilFuncionalidadeVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Ativar"))))
                        perfilFuncionalidade.Ativar = Convert.ToBoolean(GetSqlDataReader()["Ativar"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdPerfilFuncionalidade"))))
                        perfilFuncionalidade.Id = Convert.ToInt64(GetSqlDataReader()["IdPerfilFuncionalidade"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdPerfilSubModulo"))))
                        perfilFuncionalidade.PerfilSubModulo.Id = Convert.ToInt64(GetSqlDataReader()["IdPerfilSubModulo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdFuncionalidade"))))
                        perfilFuncionalidade.Funcionalidade.Id = Convert.ToInt64(GetSqlDataReader()["IdFuncionalidade"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Nome"))))
                        perfilFuncionalidade.Funcionalidade.Nome = Convert.ToString(GetSqlDataReader()["Nome"]);

                    //AcessoExterno
                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("AcessoExterno"))))
                        perfilFuncionalidade.AcessoExterno = Convert.ToBoolean(GetSqlDataReader()["AcessoExterno"]);

                    lstPerfilFuncionalidadeVO.Add(perfilFuncionalidade);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return lstPerfilFuncionalidadeVO;
        }

        public List<PerfilFuncionalidadeVO> Selecionar(PerfilFuncionalidadeVO objVO, int top = 0)
        {
            List<PerfilFuncionalidadeVO> lstPerfilFuncionalidade = null;

            try
            {
                objSbSelect = new StringBuilder();
                lstPerfilFuncionalidade = new List<PerfilFuncionalidadeVO>();

                string varTop = "";
                if (top > 0)
                {
                    varTop = top.ToString();
                }

                objSbSelect.AppendLine(@"SELECT                                                             ");
                objSbSelect.AppendLine(@"        PerfilFuncionalidade.IdPerfilFuncionalidade");
                objSbSelect.AppendLine(@"      , PerfilFuncionalidade.IdPerfilSubModulo");
                objSbSelect.AppendLine(@"      , PerfilFuncionalidade.AcessoExterno");
                objSbSelect.AppendLine(@"      , PerfilFuncionalidade.IdFuncionalidade");
                objSbSelect.AppendLine(@"      , PerfilFuncionalidade.Ativar");
                objSbSelect.AppendLine(@"	  , Funcionalidade.Nome");
                objSbSelect.AppendLine(@"  FROM DBAthon.dbo.PerfilFuncionalidade");
                objSbSelect.AppendLine(@"  INNER JOIN DBAthon.dbo.PerfilSubModulo");
                objSbSelect.AppendLine(@"          ON DBAthon.dbo.PerfilSubModulo.IdPerfilSubModulo = DBAthon.dbo.PerfilFuncionalidade.IdPerfilSubModulo");
                objSbSelect.AppendLine(@"  INNER JOIN DBAthon.dbo.Funcionalidade");
                objSbSelect.AppendLine(@"          ON DBAthon.dbo.Funcionalidade.IdFuncionalidade = DBAthon.dbo.PerfilFuncionalidade.IdFuncionalidade");
                objSbSelect.AppendLine(@"WHERE 1 = 1                                                               ");

                if (objVO != null)
                {
                    GetSqlCommand().Parameters.Clear();
                    if (objVO.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.PerfilFuncionalidade.IdPerfilFuncionalidade = @IdPerfilFuncionalidade");
                        GetSqlCommand().Parameters.Add("IdPerfilFuncionalidade", SqlDbType.Int).Value = objVO.Id;
                    }
                    if (objVO.PerfilSubModulo.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.PerfilFuncionalidade.IdPerfilSubModulo = @IdPerfilSubModulo");
                        GetSqlCommand().Parameters.Add("IdPerfilSubModulo", SqlDbType.Int).Value = objVO.PerfilSubModulo.Id;
                    }
                    if (objVO.Funcionalidade.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.PerfilFuncionalidade.IdFuncionalidade = @IdFuncionalidade");
                        GetSqlCommand().Parameters.Add("IdFuncionalidade", SqlDbType.Int).Value = objVO.Funcionalidade.Id;
                    }
                    if (objVO.Ativar != null)
                    {
                        objSbSelect.AppendLine(@" AND PerfilFuncionalidade.Ativar = @Ativar");
                        GetSqlCommand().Parameters.Add("Ativar", SqlDbType.VarChar).Value = objVO.Ativar;
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