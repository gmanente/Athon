using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Comum.VO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Sistema.Api.dll.Src.Comum.DAO
{
    public class RegimeDAO : AbstractDAO, IDAO<RegimeVO>
    {
        public RegimeDAO(SqlCommand sqlComm)
            : base(sqlComm)
        {

        }

        public long Inserir(RegimeVO objVO)
        {
            try
            {
                objSbInsert = new StringBuilder();
                long idRegime = GetCodigoSequece("DBAthon.dbo.SeqRegime");

                objSbInsert.AppendLine(@"INSERT INTO DBAthon.dbo.Regime     ");
                objSbInsert.AppendLine(@"(                                  ");
                objSbInsert.AppendLine(@"             IdRegime              ");
                objSbInsert.AppendLine(@"           , Descricao             ");
                objSbInsert.AppendLine(@")                                  ");
                objSbInsert.AppendLine(@"     VALUES                        ");
                objSbInsert.AppendLine(@"(                                  ");
                objSbInsert.AppendLine(@"             @IdRegime              ");
                objSbInsert.AppendLine(@"           , @Descricao            ");
                objSbInsert.AppendLine(@")                                  ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbInsert.ToString();
                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdRegime", SqlDbType.Int).Value = idRegime;
                GetSqlCommand().Parameters.Add("Descricao", SqlDbType.VarChar).Value = objVO.Descricao;
                GetSqlCommand().ExecuteNonQuery();
                return idRegime;
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

        public long Alterar(RegimeVO objVO, string where = null)
        {
            try
            {
                objSbUpdate = new StringBuilder();
                objSbUpdate.AppendLine(@"UPDATE DBAthon.dbo.Regime              ");
                objSbUpdate.AppendLine(@"  SET                                  ");
                objSbUpdate.AppendLine(@"      Descricao = @Descricao           ");
                if (where == null)
                {
                    objSbUpdate.AppendLine(@"WHERE IdRegime =  @IdRegime        ");
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
                    GetSqlCommand().Parameters.Add("IdRegime", SqlDbType.Int).Value = objVO.Id;
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

        public void Deletar(RegimeVO objVO)
        {
            try
            {
                CheckDelete("DBAthon.dbo.Regime", "IdRegime", objVO.Id);
                objSbDelete = new StringBuilder();

                objSbDelete.AppendLine(@"DELETE FROM                                  ");
                objSbDelete.AppendLine(@"   DBAthon.dbo.Regime WHERE                  ");
                objSbDelete.AppendLine(@"      IdRegime =  @IdRegime                  ");
                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbDelete.ToString();
                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdRegime", SqlDbType.Int).Value = objVO.Id;
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


        public List<RegimeVO> Selecionar(RegimeVO objVO, int top = 0)
        {
            RegimeVO regime = null;
            List<RegimeVO> lstRegime = null;

            try
            {
                objSbSelect = new StringBuilder();
                lstRegime = new List<RegimeVO>();

                string varTop = "";
                if (top > 0)
                {
                    varTop = top.ToString();
                }

                objSbSelect.AppendLine(@"SELECT          ").Append(varTop);
                objSbSelect.AppendLine(@"        IdRegime               ");
                objSbSelect.AppendLine(@"      , Descricao              ");
                objSbSelect.AppendLine(@"  FROM DBAthon.dbo.Regime      ");
                objSbSelect.AppendLine(@"WHERE 1 = 1                    ");

                if (objVO != null)
                {
                    GetSqlCommand().Parameters.Clear();
                    if (objVO.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND IdRegime = @IdRegime");
                        GetSqlCommand().Parameters.Add("IdRegime", SqlDbType.Int).Value = objVO.Id;
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
                    regime = new RegimeVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdRegime"))))
                        regime.Id = Convert.ToInt32(GetSqlDataReader()["IdRegime"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Descricao"))))
                        regime.Descricao = Convert.ToString(GetSqlDataReader()["Descricao"]);

                    lstRegime.Add(regime);

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
            return lstRegime;
        }

        public RegimeVO Consultar(RegimeVO objVO)
        {
            try
            {
                return (RegimeVO)Selecionar(objVO).ToArray().GetValue(0);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<RegimeVO> Listar(RegimeVO objVO)
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

        private List<RegimeVO> GetLista()
        {
            RegimeVO regimeVO = null;
            List<RegimeVO> lstRegimeVO = null;
            try
            {


                lstRegimeVO = new List<RegimeVO>();
                while (GetSqlDataReader().Read())
                {
                    regimeVO = new RegimeVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdRegime"))))
                        regimeVO.Id = Convert.ToInt32(GetSqlDataReader()["IdRegime"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Descricao"))))
                        regimeVO.Descricao = Convert.ToString(GetSqlDataReader()["Descricao"]);

                    lstRegimeVO.Add(regimeVO);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return lstRegimeVO;
        }

        public List<RegimeVO> Selecionar(string sql)
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

        public Dictionary<int, List<RegimeVO>> Paginar(string structs, int inicio, int fim)
        {
            Dictionary<int, List<RegimeVO>> dictionany = null;
            try
            {
                List<RegimeVO> lstRegime;
                dictionany = new Dictionary<int, List<RegimeVO>>();
                var sbPaginar = new StringBuilder();
                int total = GetTotalResgistro(structs);
                sbPaginar.AppendLine(structs);
                sbPaginar.AppendLine("ORDER BY DBAthon.dbo.Regime.Descricao ");
                sbPaginar.Append("OFFSET ").Append(inicio).AppendLine(" ROWS");
                sbPaginar.Append("FETCH NEXT ").Append(fim).AppendLine(" ROWS ONLY");
                lstRegime = Selecionar(sbPaginar.ToString());
                dictionany.Add(total, lstRegime);
                return dictionany;

            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}