using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Comum.VO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Sistema.Api.dll.Src.Comum.DAO
{
    public class FeriadoTipoDAO : AbstractDAO, IDAO<FeriadoTipoVO>
    {
        public FeriadoTipoDAO(SqlCommand sqlComm)
            : base(sqlComm)
        {

        }

        public long Inserir(FeriadoTipoVO objVO)
        {
            try
            {
                objSbInsert = new StringBuilder();

                long IdFeriadoTipo = GetCodigoSequece("DBAthon.dbo.SeqFeriadoTipo");

                objSbInsert.AppendLine(@"INSERT INTO DBAthon.dbo.FeriadoTipo ");
                objSbInsert.AppendLine(@"(                                   ");
                objSbInsert.AppendLine(@"             IdFeriadoTipo          ");
                objSbInsert.AppendLine(@"           , Nome                   ");
                objSbInsert.AppendLine(@")                                   ");
                objSbInsert.AppendLine(@"     VALUES                         ");
                objSbInsert.AppendLine(@"(                                   ");
                objSbInsert.AppendLine(@"             @IdFeriadoTipo         ");
                objSbInsert.AppendLine(@"           , @Nome                  ");
                objSbInsert.AppendLine(@")                                   ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbInsert.ToString();

                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdFeriadoTipo", SqlDbType.Int).Value = IdFeriadoTipo;
                GetSqlCommand().Parameters.Add("Nome", SqlDbType.VarChar).Value = objVO.Nome;

                GetSqlCommand().ExecuteNonQuery();

                return IdFeriadoTipo;
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

        public long Alterar(FeriadoTipoVO objVO, string where = null)
        {
            try
            {
                objSbUpdate = new StringBuilder();

                objSbUpdate.AppendLine(@"UPDATE DBAthon.dbo.FeriadoTipo ");
                objSbUpdate.AppendLine(@"   SET                         ");
                objSbUpdate.AppendLine(@"      Nome = @Nome             ");

                if (where == null)
                {
                    objSbUpdate.AppendLine(@"WHERE IdNome = @IdNome ");
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
                    GetSqlCommand().Parameters.Add("IdFeriadoTipo", SqlDbType.Int).Value = objVO.Id;
                    GetSqlCommand().Parameters.Add("Nome", SqlDbType.VarChar).Value = objVO.Nome;

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

        public void Deletar(FeriadoTipoVO objVO)
        {
            try
            {
                objSbDelete = new StringBuilder();

                objSbDelete.AppendLine(@"DELETE FROM DBAthon.dbo.FeriadoTipo WHERE IdFeriadoTipo = @IdFeriadoTipo ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbDelete.ToString();

                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdFeriadoTipo", SqlDbType.Int).Value = objVO.Id;

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


        public List<FeriadoTipoVO> Selecionar(FeriadoTipoVO objVO, int top = 0)
        {
            FeriadoTipoVO FeriadoTipo = null;
            List<FeriadoTipoVO> lstFeriadoTipo = null;

            try
            {
                objSbSelect = new StringBuilder();

                lstFeriadoTipo = new List<FeriadoTipoVO>();

                string varTop = "";
                if (top > 0)
                {
                    varTop = top.ToString();
                }

                objSbSelect.AppendLine(@"SELECT                         ").Append(varTop);
                objSbSelect.AppendLine(@"         IdFeriadoTipo         ");
                objSbSelect.AppendLine(@"       , Nome                  ");
                objSbSelect.AppendLine(@"  FROM DBAthon.dbo.FeriadoTipo ");
                objSbSelect.AppendLine(@" WHERE 1 = 1                   ");

                if (objVO != null)
                {
                    GetSqlCommand().Parameters.Clear();

                    if (objVO.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND IdFeriadoTipo = @IdFeriadoTipo");
                        GetSqlCommand().Parameters.Add("IdFeriadoTipo", SqlDbType.Int).Value = objVO.Id;
                    }
                }

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();

                while (GetSqlDataReader().Read())
                {
                    FeriadoTipo = new FeriadoTipoVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdFeriadoTipo"))))
                        FeriadoTipo.Id = Convert.ToInt32(GetSqlDataReader()["IdFeriadoTipo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Nome"))))
                        FeriadoTipo.Nome = Convert.ToString(GetSqlDataReader()["Nome"]);

                    lstFeriadoTipo.Add(FeriadoTipo);
                }
            }
            catch (Exception e)
            {
                throw e;

            }
            finally
            {
                if (objSbSelect != null)
                    objSbSelect = null;

                Close();
            }

            return lstFeriadoTipo;
        }

        public FeriadoTipoVO Consultar(FeriadoTipoVO objVO)
        {
            try
            {
                List<FeriadoTipoVO> lstFeriadoTipoVO = Selecionar(objVO);

                return lstFeriadoTipoVO.Count > 0 ? (FeriadoTipoVO)lstFeriadoTipoVO.ToArray().GetValue(0) : null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private List<FeriadoTipoVO> GetLista()
        {
            FeriadoTipoVO feriadoTipoVO = null;
            List<FeriadoTipoVO> lstFeriadoTipoVO = null;

            try
            {
                lstFeriadoTipoVO = new List<FeriadoTipoVO>();

                while (GetSqlDataReader().Read())
                {
                    feriadoTipoVO = new FeriadoTipoVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdFeriadoTipo"))))
                        feriadoTipoVO.Id = Convert.ToInt32(GetSqlDataReader()["IdFeriadoTipo"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Nome"))))
                        feriadoTipoVO.Nome = Convert.ToString(GetSqlDataReader()["Nome"]);

                    lstFeriadoTipoVO.Add(feriadoTipoVO);
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return lstFeriadoTipoVO;
        }

        public List<FeriadoTipoVO> Selecionar(string sql)
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
                    objSbSelect = null;

                Close();
            }
        }

        public Dictionary<int, List<FeriadoTipoVO>> Paginar(string structs, int inicio, int fim)
        {
            Dictionary<int, List<FeriadoTipoVO>> dictionany = null;
            try
            {
                List<FeriadoTipoVO> lstFeriadoTipoVO;

                dictionany = new Dictionary<int, List<FeriadoTipoVO>>();

                var sbPaginar = new StringBuilder();

                int total = GetTotalResgistro(structs);

                sbPaginar.AppendLine(structs);
                sbPaginar.AppendLine("ORDER BY DBAthon.dbo.FeriadoTipo.Nome ");
                sbPaginar.Append("OFFSET ").Append(inicio).AppendLine(" ROWS");
                sbPaginar.Append("FETCH NEXT ").Append(fim).AppendLine(" ROWS ONLY");

                lstFeriadoTipoVO = Selecionar(sbPaginar.ToString());

                dictionany.Add(total, lstFeriadoTipoVO);

                return dictionany;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<FeriadoTipoVO> Listar(FeriadoTipoVO objVO)
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