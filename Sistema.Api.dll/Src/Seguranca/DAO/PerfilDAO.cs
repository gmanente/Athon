using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Seguranca.VO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Sistema.Api.dll.Src.Seguranca.DAO
{
    public class PerfilDAO : AbstractDAO, IDAO<PerfilVO>
    {
        public PerfilDAO(SqlCommand sqlComm)
            : base(sqlComm)
        {
        }

        public long Inserir(PerfilVO objVO)
        {
            try
            {
                objSbInsert = new StringBuilder();
                long IdPerfil = GetCodigoSequece("DBAthon.dbo.SeqPerfil");
                objSbInsert.AppendLine(@"INSERT INTO  DBAthon.dbo.Perfil   ");
                objSbInsert.AppendLine(@"(                                                                                             ");
                objSbInsert.AppendLine(@"             IdPerfil       ");
                objSbInsert.AppendLine(@"          ,  Descricao      ");
                objSbInsert.AppendLine(@"          ,  Ativar        ");
                objSbInsert.AppendLine(@")");
                objSbInsert.AppendLine(@"VALUES ");
                objSbInsert.AppendLine(@"(");
                objSbInsert.AppendLine(@"             @IdPerfil      ");
                objSbInsert.AppendLine(@"          ,  @Descricao     ");
                objSbInsert.AppendLine(@"          ,  @Ativar       ");
                objSbInsert.AppendLine(@")");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbInsert.ToString();
                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdPerfil", SqlDbType.Int).Value = IdPerfil;
                GetSqlCommand().Parameters.Add("Descricao", SqlDbType.VarChar).Value = objVO.Descricao;
                GetSqlCommand().Parameters.Add("Ativar", SqlDbType.VarChar).Value = objVO.Ativar;
                GetSqlCommand().ExecuteNonQuery();
                return IdPerfil;

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

        public long Alterar(PerfilVO objVO, string where = null)
        {
            try
            {
                objSbUpdate = new StringBuilder();
                objSbUpdate.AppendLine("UPDATE DBAthon.dbo.Perfil ");
                objSbUpdate.AppendLine("   SET Descricao = @Descricao ");
                objSbUpdate.AppendLine("     , Ativar = @Ativar");

                if (where == null)
                {
                    objSbUpdate.AppendLine(" WHERE IdPerfil = @IdPerfil");
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
                    GetSqlCommand().Parameters.Add("IdPerfil", SqlDbType.Int).Value = objVO.Id;
                    GetSqlCommand().Parameters.Add("Descricao", SqlDbType.VarChar).Value = objVO.Descricao;
                    GetSqlCommand().Parameters.Add("Ativar", SqlDbType.VarChar).Value = objVO.Ativar;
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

        public void Deletar(PerfilVO objVO)
        {
            try
            {
                objSbDelete = new StringBuilder();
                objSbDelete.AppendLine("DELETE FROM DBAthon.dbo.Perfil ");
                objSbDelete.AppendLine(" WHERE IdPerfil = @IdPerfil");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbDelete.ToString();
                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdPerfil", SqlDbType.Int).Value = objVO.Id;

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

        public List<PerfilVO> Listar(PerfilVO objVO = null)
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

        public List<PerfilVO> Selecionar(string sql)
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

        public PerfilVO Consultar(PerfilVO objVO)
        {
            try
            {
                List<PerfilVO> lstPerfilVO = Selecionar(objVO);
                return lstPerfilVO.Count > 0 ? (PerfilVO)lstPerfilVO.ToArray().GetValue(0) : null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<PerfilVO> GetLista()
        {
            PerfilVO perfil = null;
            List<PerfilVO> lstPerfilVO = null;
            try
            {
                lstPerfilVO = new List<PerfilVO>();
                while (GetSqlDataReader().Read())
                {
                    perfil = new PerfilVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdPerfil"))))
                        perfil.Id = Convert.ToInt64(GetSqlDataReader()["IdPerfil"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Descricao"))))
                        perfil.Descricao = Convert.ToString(GetSqlDataReader()["Descricao"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Ativar"))))
                        perfil.Ativar = Convert.ToBoolean(GetSqlDataReader()["Ativar"]);

                    lstPerfilVO.Add(perfil);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return lstPerfilVO;
        }

        public List<PerfilVO> Selecionar(PerfilVO objVO, int top = 0)
        {

            try
            {
                objSbSelect = new StringBuilder();

                string varTop = "";
                if (top > 0)
                {
                    varTop = top.ToString();
                }

                objSbSelect.AppendLine(@"SELECT                                           ");
                objSbSelect.AppendLine(@"       IdPerfil                                  ");
                objSbSelect.AppendLine(@"      ,Descricao                                 ");
                objSbSelect.AppendLine(@"      ,Ativar                                    ");
                objSbSelect.AppendLine(@"  FROM DBAthon.dbo.Perfil                    ");
                objSbSelect.AppendLine(@"WHERE 1 = 1                                       ");

                if (objVO != null)
                {
                    GetSqlCommand().Parameters.Clear();
                    if (objVO.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.Perfil.IdPerfil = @IdPerfil");
                        GetSqlCommand().Parameters.Add("IdPerfil", SqlDbType.Int).Value = objVO.Id;
                    }
                    if (!string.IsNullOrEmpty(objVO.Descricao))
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.Perfil.Descricao = @IdDescricao");
                        GetSqlCommand().Parameters.Add("Descricao", SqlDbType.VarChar).Value = objVO.Descricao;
                    }
                }

                objSbSelect.AppendLine(@" ORDER BY  DBAthon.dbo.Perfil.Descricao ");

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

        public Dictionary<int, List<PerfilVO>> Paginar(string structs, int inicio, int fim)
        {
            Dictionary<int, List<PerfilVO>> dictionany = null;

            try
            {
                List<PerfilVO> lstPerfil;

                dictionany = new Dictionary<int, List<PerfilVO>>();

                var sbPaginar = new StringBuilder();

                int total = GetTotalResgistro(structs);

                sbPaginar.AppendLine(structs);
                sbPaginar.AppendLine("ORDER BY Descricao");
                sbPaginar.Append("OFFSET ").Append(inicio).AppendLine(" ROWS");
                sbPaginar.Append("FETCH NEXT ").Append(fim).AppendLine(" ROWS ONLY");

                lstPerfil = Selecionar(sbPaginar.ToString());

                dictionany.Add(total, lstPerfil);

                return dictionany;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}