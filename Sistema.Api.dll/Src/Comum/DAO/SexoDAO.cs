using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Comum.VO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Sistema.Api.dll.Src.Comum.DAO
{
    public class SexoDAO : AbstractDAO, IDAO<SexoVO>
    {
        public SexoDAO(SqlCommand sqlComm)
            : base(sqlComm)
        {

        }

        public long Inserir(SexoVO objVO)
        {
            try
            {
                objSbInsert = new StringBuilder();
                long idSexo = GetCodigoSequece("DBAthon.dbo.SeqSexo");

                objSbInsert.AppendLine(@"INSERT INTO DBAthon.dbo.Sexo       ");
                objSbInsert.AppendLine(@"(                                  ");
                objSbInsert.AppendLine(@"             IdSexo                ");
                objSbInsert.AppendLine(@"           , Descricao             ");
                objSbInsert.AppendLine(@"           , Sigla                 ");
                objSbInsert.AppendLine(@")                                  ");
                objSbInsert.AppendLine(@"     VALUES                        ");
                objSbInsert.AppendLine(@"(                                  ");
                objSbInsert.AppendLine(@"             @IdSexo               ");
                objSbInsert.AppendLine(@"           , @Descricao            ");
                objSbInsert.AppendLine(@"           , @Sigla                ");
                objSbInsert.AppendLine(@")                                  ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbInsert.ToString();
                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdSexo", SqlDbType.Int).Value = idSexo;
                GetSqlCommand().Parameters.Add("Descricao", SqlDbType.VarChar).Value = objVO.Descricao;
                GetSqlCommand().ExecuteNonQuery();
                return idSexo;
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

        public long Alterar(SexoVO objVO, string where = null)
        {
            try
            {
                objSbUpdate = new StringBuilder();
                objSbUpdate.AppendLine(@"UPDATE DBAthon.dbo.Sexo               ");
                objSbUpdate.AppendLine(@"  SET                                  ");
                objSbUpdate.AppendLine(@"      Descricao = @Descricao           ");

                if (where == null)
                {
                    objSbUpdate.AppendLine(@"WHERE IdSexo = @IdSexo           ");
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
                    GetSqlCommand().Parameters.Add("IdSexo", SqlDbType.Int).Value = objVO.Id;
                    GetSqlCommand().Parameters.Add("Descricao", SqlDbType.VarChar).Value = objVO.Descricao;
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
                {
                    objSbUpdate = null;
                }
            }
        }

        public void Deletar(SexoVO objVO)
        {
            try
            {
                CheckDelete("DBAthon.dbo.Sexo", "IdSexo", objVO.Id);
                objSbDelete = new StringBuilder();

                objSbDelete.AppendLine(@"DELETE FROM                                ");
                objSbDelete.AppendLine(@"   DBAthon.dbo.Sexo WHERE                  ");
                objSbDelete.AppendLine(@"      IdSexo =  @IdSexo                    ");
                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbDelete.ToString();
                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdSexo", SqlDbType.Int).Value = objVO.Id;
                GetSqlCommand().ExecuteNonQuery();
            }
            catch (SqlException e)
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


        public List<SexoVO> Selecionar(SexoVO objVO, int top = 0)
        {
            SexoVO sexo = null;
            List<SexoVO> lstSexo = null;

            try
            {
                objSbSelect = new StringBuilder();
                lstSexo = new List<SexoVO>();

                string varTop = "";
                if (top > 0)
                {
                    varTop = top.ToString();
                }

                objSbSelect.AppendLine(@"SELECT                           ").Append(varTop);
                objSbSelect.AppendLine(@"        IdSexo                   ");
                objSbSelect.AppendLine(@"      , Descricao                ");
                objSbSelect.AppendLine(@"      , Sigla                    ");
                objSbSelect.AppendLine(@"  FROM DBAthon.dbo.Sexo          ");
                objSbSelect.AppendLine(@"WHERE 1 = 1                      ");

                if (objVO != null)
                {
                    GetSqlCommand().Parameters.Clear();
                    if (objVO.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND IdSexo = @IdSexo");
                        GetSqlCommand().Parameters.Add("IdSexo", SqlDbType.Int).Value = objVO.Id;
                    }
                    if (!string.IsNullOrEmpty(objVO.Descricao))
                    {
                        objSbSelect.AppendLine(@" AND Descricao = @Descricao");
                        GetSqlCommand().Parameters.Add("Descricao", SqlDbType.VarChar).Value = objVO.Descricao;
                    }


                }
                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();

                while (GetSqlDataReader().Read())
                {
                    sexo = new SexoVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdSexo"))))
                        sexo.Id = Convert.ToInt32(GetSqlDataReader()["IdSexo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Descricao"))))
                        sexo.Descricao = Convert.ToString(GetSqlDataReader()["Descricao"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Sigla"))))
                        sexo.Sigla = Convert.ToString(GetSqlDataReader()["Sigla"]);

                    lstSexo.Add(sexo);

                }

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
            return lstSexo;
        }

        public SexoVO Consultar(SexoVO objVO)
        {
            try
            {
                List<SexoVO> lstSexo = Selecionar(objVO);
                return lstSexo.Count > 0 ? (SexoVO)lstSexo.ToArray().GetValue(0) : null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private List<SexoVO> GetLista()
        {
            SexoVO sexoVO = null;
            List<SexoVO> lstSexoVO = null;
            try
            {
                lstSexoVO = new List<SexoVO>();
                while (GetSqlDataReader().Read())
                {
                    sexoVO = new SexoVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdSexo"))))
                        sexoVO.Id = Convert.ToInt32(GetSqlDataReader()["IdSexo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Descricao"))))
                        sexoVO.Descricao = Convert.ToString(GetSqlDataReader()["Descricao"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Sigla"))))
                        sexoVO.Sigla = Convert.ToString(GetSqlDataReader()["Sigla"]);

                    lstSexoVO.Add(sexoVO);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return lstSexoVO;
        }

        public List<SexoVO> Selecionar(string sql)
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

        public Dictionary<int, List<SexoVO>> Paginar(string structs, int inicio, int fim)
        {
            Dictionary<int, List<SexoVO>> dictionany = null;
            try
            {
                List<SexoVO> lstSexo;
                dictionany = new Dictionary<int, List<SexoVO>>();
                var sbPaginar = new StringBuilder();
                int total = GetTotalResgistro(structs);
                sbPaginar.AppendLine(structs);
                sbPaginar.AppendLine("ORDER BY DBAthon.dbo.Sexo.Descricao ");
                sbPaginar.Append("OFFSET ").Append(inicio).AppendLine(" ROWS");
                sbPaginar.Append("FETCH NEXT ").Append(fim).AppendLine(" ROWS ONLY");
                lstSexo = Selecionar(sbPaginar.ToString());
                dictionany.Add(total, lstSexo);
                return dictionany;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<SexoVO> Listar(SexoVO objVO)
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
    }
}