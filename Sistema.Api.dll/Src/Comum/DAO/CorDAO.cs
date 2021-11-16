using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Comum.VO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Sistema.Api.dll.Src.Comum.DAO
{
    public class CorDAO : AbstractDAO, IDAO<CorVO>
    {
        public CorDAO(SqlCommand sqlComm)
            : base(sqlComm)
        {

        }

        public long Inserir(CorVO objVO)
        {
            try
            {
                objSbInsert = new StringBuilder();
                long IdCor = GetCodigoSequece("DBAthon.dbo.SeqCor          ");

                objSbInsert.AppendLine(@"INSERT INTO DBAthon.dbo.Cor       ");
                objSbInsert.AppendLine(@"(                                 ");
                objSbInsert.AppendLine(@"             IdCor                ");
                objSbInsert.AppendLine(@"           , Descricao            ");
                objSbInsert.AppendLine(@")                                 ");
                objSbInsert.AppendLine(@"     VALUES                       ");
                objSbInsert.AppendLine(@"(                                 ");
                objSbInsert.AppendLine(@"             @IdCor               ");
                objSbInsert.AppendLine(@"           , @Descricao           ");
                objSbInsert.AppendLine(@")                                 ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbInsert.ToString();
                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdCor", SqlDbType.Int).Value = IdCor;
                GetSqlCommand().Parameters.Add("Descricao", SqlDbType.VarChar).Value = objVO.Descricao;
                GetSqlCommand().ExecuteNonQuery();
                return IdCor;
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

        public long Alterar(CorVO objVO, string where = null)
        {
            try
            {
                objSbUpdate = new StringBuilder();
                objSbUpdate.AppendLine(@"UPDATE DBAthon.dbo.Cor                          ");
                objSbUpdate.AppendLine(@"   SET                                          ");
                objSbUpdate.AppendLine(@"      Descricao = @Descricao                    ");

                if (where == null)
                {
                    objSbUpdate.AppendLine(@"WHERE IdCor = @IdCor                        ");
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
                    GetSqlCommand().Parameters.Add("IdCor", SqlDbType.Int).Value = objVO.Id;
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

        public void Deletar(CorVO objVO)
        {
            try
            {
                CheckDelete("DBAthon.dbo.Cor", "IdCor", objVO.Id);
                objSbDelete = new StringBuilder();

                objSbDelete.AppendLine(@"DELETE FROM                               ");
                objSbDelete.AppendLine(@"   DBAthon.dbo.Cor WHERE                  ");
                objSbDelete.AppendLine(@"      IdCor =  @IdCor                     ");
                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbDelete.ToString();
                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdCor", SqlDbType.Int).Value = objVO.Id;
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

        public List<CorVO> Selecionar(CorVO objVO, int top = 0)
        {
            CorVO Cor = null;
            List<CorVO> lstCor = null;

            try
            {
                objSbSelect = new StringBuilder();
                lstCor = new List<CorVO>();

                string varTop = "";
                if (top > 0)
                {
                    varTop = top.ToString();
                }

                objSbSelect.AppendLine(@"SELECT                           ").Append(varTop);
                objSbSelect.AppendLine(@"       IdCor                     ");
                objSbSelect.AppendLine(@"     , Descricao                 ");
                objSbSelect.AppendLine(@"  FROM DBAthon.dbo.Cor           ");
                objSbSelect.AppendLine(@" WHERE 1 = 1                     ");

                if (objVO != null)
                {
                    GetSqlCommand().Parameters.Clear();
                    if (objVO.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND IdCor = @IdCor");
                        GetSqlCommand().Parameters.Add("IdCor", SqlDbType.Int).Value = objVO.Id;
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
                    Cor = new CorVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdCor"))))
                        Cor.Id = Convert.ToInt32(GetSqlDataReader()["IdCor"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Descricao"))))
                        Cor.Descricao = Convert.ToString(GetSqlDataReader()["Descricao"]);

                    lstCor.Add(Cor);

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
            return lstCor;
        }

        public CorVO Consultar(CorVO objVO)
        {
            try
            {
                List<CorVO> lstCor = Selecionar(objVO);
                return lstCor.Count > 0 ? (CorVO)lstCor.ToArray().GetValue(0) : null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private List<CorVO> GetLista()
        {
            CorVO corVO = null;
            List<CorVO> lstCorVO = null;
            try
            {
                lstCorVO = new List<CorVO>();
                while (GetSqlDataReader().Read())
                {
                    corVO = new CorVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdCor"))))
                        corVO.Id = Convert.ToInt32(GetSqlDataReader()["IdCor"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Descricao"))))
                        corVO.Descricao = Convert.ToString(GetSqlDataReader()["Descricao"]);

                    lstCorVO.Add(corVO);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return lstCorVO;
        }

        public List<CorVO> Selecionar(string sql)
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

        public Dictionary<int, List<CorVO>> Paginar(string structs, int inicio, int fim)
        {
            Dictionary<int, List<CorVO>> dictionany = null;
            try
            {
                List<CorVO> lstCor;
                dictionany = new Dictionary<int, List<CorVO>>();
                var sbPaginar = new StringBuilder();
                int total = GetTotalResgistro(structs);
                sbPaginar.AppendLine(structs);
                sbPaginar.AppendLine("ORDER BY DBAthon.dbo.Cor.Descricao ");
                sbPaginar.Append("OFFSET ").Append(inicio).AppendLine(" ROWS");
                sbPaginar.Append("FETCH NEXT ").Append(fim).AppendLine(" ROWS ONLY");
                lstCor = Selecionar(sbPaginar.ToString());
                dictionany.Add(total, lstCor);
                return dictionany;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<CorVO> Listar(CorVO objVO)
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