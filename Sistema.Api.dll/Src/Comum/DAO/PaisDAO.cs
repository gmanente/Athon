using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Comum.VO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Sistema.Api.dll.Src.Comum.DAO
{
    public class PaisDAO : AbstractDAO, IDAO<PaisVO>
    {
        public PaisDAO(SqlCommand sqlComm)
            : base(sqlComm)
        {

        }

        public long Inserir(PaisVO objVO)
        {
            try
            {
                objSbInsert = new StringBuilder();
                //long CodigoBacen = GetCodigoSequece("DBAthon.dbo.SeqPais    ");

                objSbInsert.AppendLine(@"INSERT INTO DBAthon.dbo.Pais       ");
                objSbInsert.AppendLine(@"(                                  ");
                objSbInsert.AppendLine(@"             CodigoBacen           ");
                objSbInsert.AppendLine(@"           , Nome                  ");
                objSbInsert.AppendLine(@")                                  ");
                objSbInsert.AppendLine(@"     VALUES                        ");
                objSbInsert.AppendLine(@"(                                  ");
                objSbInsert.AppendLine(@"             @CodigoBacen          ");
                objSbInsert.AppendLine(@"           , @Nome                 ");
                objSbInsert.AppendLine(@")                                  ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbInsert.ToString();
                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("CodigoBacen", SqlDbType.Int).Value = objVO.Id;
                GetSqlCommand().Parameters.Add("Nome", SqlDbType.VarChar).Value = objVO.Descricao;
                GetSqlCommand().ExecuteNonQuery();
                return objVO.Id;
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

        public long Alterar(PaisVO objVO, string where = null)
        {
            try
            {
                objSbUpdate = new StringBuilder();
                objSbUpdate.AppendLine(@"UPDATE DBAthon.dbo.Pais                          ");
                objSbUpdate.AppendLine(@"   SET                                           ");
                objSbUpdate.AppendLine(@"      Nome = @Nome                               ");

                if (where == null)
                {
                    objSbUpdate.AppendLine(@"WHERE CodigoBacen = @CodigoBacen             ");
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
                    GetSqlCommand().Parameters.Add("CodigoBacen", SqlDbType.Int).Value = objVO.Id;
                    GetSqlCommand().Parameters.Add("Nome", SqlDbType.VarChar).Value = objVO.Descricao;
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

        public void Deletar(PaisVO objVO)
        {
            try
            {
                CheckDelete("DBAthon.dbo.Pais", "IdPais", objVO.Id);
                objSbDelete = new StringBuilder();

                objSbDelete.AppendLine(@"DELETE FROM                                ");
                objSbDelete.AppendLine(@"   DBAthon.dbo.Pais WHERE                  ");
                objSbDelete.AppendLine(@"      CodigoBacen =  @CodigoBacen          ");
                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbDelete.ToString();
                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("CodigoBacen", SqlDbType.Int).Value = objVO.Id;
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


        public List<PaisVO> Selecionar(PaisVO objVO, int top = 0)
        {
            PaisVO Pais = null;
            List<PaisVO> lstPais = null;

            try
            {
                objSbSelect = new StringBuilder();
                lstPais = new List<PaisVO>();

                string varTop = "";
                if (top > 0)
                {
                    varTop = top.ToString();
                }

                objSbSelect.AppendLine(@"SELECT                           ").Append(varTop);
                objSbSelect.AppendLine(@"       CodigoBacen               ");
                objSbSelect.AppendLine(@"     , Nome                      ");
                objSbSelect.AppendLine(@"  FROM DBAthon.dbo.Pais          ");
                objSbSelect.AppendLine(@" WHERE 1 = 1                     ");

                if (objVO != null)
                {
                    GetSqlCommand().Parameters.Clear();
                    if (objVO.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND CodigoBacen = @CodigoBacen");
                        GetSqlCommand().Parameters.Add("CodigoBacen", SqlDbType.Int).Value = objVO.Id;
                    }
                    if (!string.IsNullOrEmpty(objVO.Descricao))
                    {
                        objSbSelect.AppendLine(@" AND Nome = @Nome");
                        GetSqlCommand().Parameters.Add("Nome", SqlDbType.VarChar).Value = objVO.Descricao;
                    }


                }
                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();

                while (GetSqlDataReader().Read())
                {
                    Pais = new PaisVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("CodigoBacen"))))
                        Pais.Id = Convert.ToInt32(GetSqlDataReader()["CodigoBacen"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Nome"))))
                        Pais.Descricao = Convert.ToString(GetSqlDataReader()["Nome"]);

                    lstPais.Add(Pais);

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
            return lstPais;
        }

        public PaisVO Consultar(PaisVO objVO)
        {
            try
            {
                List<PaisVO> lstPais = Selecionar(objVO);
                return lstPais.Count > 0 ? (PaisVO)lstPais.ToArray().GetValue(0) : null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private List<PaisVO> GetLista()
        {
            PaisVO paisVO = null;
            List<PaisVO> lstPaisVO = null;
            try
            {
                lstPaisVO = new List<PaisVO>();
                while (GetSqlDataReader().Read())
                {
                    paisVO = new PaisVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("CodigoBacen"))))
                        paisVO.Id = Convert.ToInt32(GetSqlDataReader()["CodigoBacen"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Nome"))))
                        paisVO.Descricao = Convert.ToString(GetSqlDataReader()["Nome"]);

                    lstPaisVO.Add(paisVO);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return lstPaisVO;
        }

        public List<PaisVO> Selecionar(string sql)
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

        public Dictionary<int, List<PaisVO>> Paginar(string structs, int inicio, int fim)
        {
            Dictionary<int, List<PaisVO>> dictionany = null;
            try
            {
                List<PaisVO> lstPais;
                dictionany = new Dictionary<int, List<PaisVO>>();
                var sbPaginar = new StringBuilder();
                int total = GetTotalResgistro(structs);
                sbPaginar.AppendLine(structs);
                sbPaginar.AppendLine("ORDER BY DBAthon.dbo.Pais.Nome ");
                sbPaginar.Append("OFFSET ").Append(inicio).AppendLine(" ROWS");
                sbPaginar.Append("FETCH NEXT ").Append(fim).AppendLine(" ROWS ONLY");
                lstPais = Selecionar(sbPaginar.ToString());
                dictionany.Add(total, lstPais);
                return dictionany;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<PaisVO> Listar(PaisVO objVO)
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