using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Comum.VO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;

namespace Sistema.Api.dll.Src.Comum.DAO
{
    public class CidadeDAO : AbstractDAO, IDAO<CidadeVO>
    {
        public CidadeDAO(SqlCommand sqlComm)
            : base(sqlComm)
        {

        }

        public long Inserir(CidadeVO objVO)
        {
            try
            {
                objSbInsert = new StringBuilder();
                //long CodigoIbge = GetCodigoSequece("DBAthon.dbo.SeqCidade    ");

                objSbInsert.AppendLine(@"INSERT INTO DBAthon.dbo.Cidade     ");
                objSbInsert.AppendLine(@"(                                  ");
                objSbInsert.AppendLine(@"             CodigoIbge            ");
                objSbInsert.AppendLine(@"           , Nome                  ");
                objSbInsert.AppendLine(@"           , IdEstado              ");
                objSbInsert.AppendLine(@")                                  ");
                objSbInsert.AppendLine(@"     VALUES                        ");
                objSbInsert.AppendLine(@"(                                  ");
                objSbInsert.AppendLine(@"             @CodigoIbge           ");
                objSbInsert.AppendLine(@"           , @Nome                 ");
                objSbInsert.AppendLine(@"           , @IdEstado             ");
                objSbInsert.AppendLine(@")                                  ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbInsert.ToString();
                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("CodigoIbge", SqlDbType.Int).Value = objVO.Id;
                GetSqlCommand().Parameters.Add("Nome", SqlDbType.VarChar).Value = objVO.Nome;
                GetSqlCommand().Parameters.Add("IdEstado", SqlDbType.VarChar).Value = objVO.Estado.Id;
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

        public long Alterar(CidadeVO objVO, string where = null)
        {
            try
            {
                objSbUpdate = new StringBuilder();
                objSbUpdate.AppendLine(@"UPDATE DBAthon.dbo.Cidade                         ");
                objSbUpdate.AppendLine(@"   SET                                            ");
                objSbUpdate.AppendLine(@"       Nome = @Nome                               ");
                objSbUpdate.AppendLine(@"     , IdEstado = @IdEstado                       ");

                if (where == null)
                {
                    objSbUpdate.AppendLine(@"WHERE CodigoIbge = @CodigoIbge                ");
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
                    GetSqlCommand().Parameters.Add("CodigoIbge", SqlDbType.Int).Value = objVO.Id;
                    GetSqlCommand().Parameters.Add("Nome", SqlDbType.VarChar).Value = objVO.Nome;
                    GetSqlCommand().Parameters.Add("IdEstado", SqlDbType.VarChar).Value = objVO.Estado.Id;
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

        public void Deletar(CidadeVO objVO)
        {
            try
            {
                CheckDelete("DBAthon.dbo.Cidade", "IdCidade", objVO.Id);
                objSbDelete = new StringBuilder();

                objSbDelete.AppendLine(@"DELETE FROM                               ");
                objSbDelete.AppendLine(@"   DBAthon.dbo.Cidade WHERE               ");
                objSbDelete.AppendLine(@"      CodigoIbge =  @CodigoIbge           ");
                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbDelete.ToString();
                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("CodigoIbge", SqlDbType.Int).Value = objVO.Id;
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

        public List<CidadeVO> Selecionar(CidadeVO objVO, int top = 0)
        {
            CidadeVO cidade = null;
            List<CidadeVO> lstCidade = null;

            try
            {
                objSbSelect = new StringBuilder();
                lstCidade = new List<CidadeVO>();

                string varTop = "";
                if (top > 0)
                {
                    varTop = top.ToString();
                }

                objSbSelect.AppendLine(@"SELECT                                                                     ");
                objSbSelect.AppendLine(@"       DBAthon.dbo.Cidade.CodigoIbge                                       ");
                objSbSelect.AppendLine(@"     , DBAthon.dbo.Cidade.Nome                                             ");
                objSbSelect.AppendLine(@"	  , DBAthon.dbo.Cidade.IdEstado                                         ");
                objSbSelect.AppendLine(@"	  , DBAthon.dbo.Estado.Nome Estado                                      ");
                objSbSelect.AppendLine(@"	  , DBAthon.dbo.Estado.Sigla EstadoSigla                                ");
                objSbSelect.AppendLine(@"	  , DBAthon.dbo.Pais.Nome AS Pais                                       ");
                objSbSelect.AppendLine(@"  FROM DBAthon.dbo.Cidade                                                  ");
                objSbSelect.AppendLine(@"INNER JOIN DBAthon.dbo.Estado                                              ");
                objSbSelect.AppendLine(@"        ON DBAthon.dbo.Estado.CodigoIbge = DBAthon.dbo.Cidade.IdEstado     ");
                objSbSelect.AppendLine(@"INNER JOIN DBAthon.dbo.Pais                                                ");
                objSbSelect.AppendLine(@"        ON DBAthon.dbo.Pais.CodigoBacen = DBAthon.dbo.Estado.IdPais        ");
                objSbSelect.AppendLine(@" WHERE 1 = 1                                                               ");

                if (objVO != null)
                {
                    GetSqlCommand().Parameters.Clear();
                    if (objVO.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.Cidade.CodigoIbge = @CodigoIbge");
                        GetSqlCommand().Parameters.Add("CodigoIbge", SqlDbType.Int).Value = objVO.Id;
                    }
                    if (!string.IsNullOrEmpty(objVO.Nome))
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.Cidade.Nome LIKE  @Nome + '%'");
                        GetSqlCommand().Parameters.Add("Nome", SqlDbType.VarChar).Value = objVO.Nome;
                    }
                    else if (!string.IsNullOrEmpty(objVO.NomeExato))
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.Cidade.Nome = @NomeExato ");
                        GetSqlCommand().Parameters.Add("NomeExato", SqlDbType.VarChar).Value = objVO.NomeExato;
                    }

                    if (objVO.Estado.PaisVO.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.Pais.CodigoBacen = @CodigoBacen");
                        GetSqlCommand().Parameters.Add("CodigoBacen", SqlDbType.Int).Value = objVO.Estado.PaisVO.Id;
                    }

                    if (objVO.Estado.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.Cidade.IdEstado = @IdEstado");
                        GetSqlCommand().Parameters.Add("IdEstado", SqlDbType.Int).Value = objVO.Estado.Id;
                    }

                    if (objVO.Estado.Sigla != null)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.Estado.Sigla   = @SiglaEstado");
                        GetSqlCommand().Parameters.Add("SiglaEstado", SqlDbType.VarChar).Value = objVO.Estado.Sigla;
                    }
                }

                objSbSelect.AppendLine(@"  ORDER BY DBAthon.dbo.Cidade.Nome, DBAthon.dbo.Estado.Nome                ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();

                while (GetSqlDataReader().Read())
                {
                    cidade = new CidadeVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("CodigoIbge"))))
                        cidade.Id = Convert.ToInt32(GetSqlDataReader()["CodigoIbge"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Nome"))))
                    {
                        string _cidade = Convert.ToString(GetSqlDataReader()["Nome"]);
                        _cidade = Regex.Replace(_cidade, @"\s(d|da|de|do|das|dos)\s", delegate (Match match) { return match.ToString().ToLower(); }, RegexOptions.IgnoreCase);
                        cidade.Nome = _cidade;
                    }

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdEstado"))))
                        cidade.Estado.Id = Convert.ToInt32(GetSqlDataReader()["IdEstado"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Estado"))))
                        cidade.Estado.Descricao = Convert.ToString(GetSqlDataReader()["Estado"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Pais"))))
                        cidade.Estado.PaisVO.Descricao = Convert.ToString(GetSqlDataReader()["Pais"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("EstadoSigla"))))
                        cidade.Estado.Sigla = Convert.ToString(GetSqlDataReader()["EstadoSigla"]);

                    lstCidade.Add(cidade);
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
            return lstCidade;
        }

        public CidadeVO Consultar(CidadeVO objVO)
        {
            try
            {
                List<CidadeVO> lstCidade = Selecionar(objVO);

                return lstCidade.Count > 0 ? (CidadeVO)lstCidade.ToArray().GetValue(0) : null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private List<CidadeVO> GetLista()
        {
            CidadeVO cidade = null;
            List<CidadeVO> lstCidadeVO = null;
            try
            {
                lstCidadeVO = new List<CidadeVO>();
                while (GetSqlDataReader().Read())
                {
                    cidade = new CidadeVO();
                    cidade.Estado = new EstadoVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("CodigoIbge"))))
                        cidade.Id = Convert.ToInt32(GetSqlDataReader()["CodigoIbge"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Nome"))))
                        cidade.Nome = Convert.ToString(GetSqlDataReader()["Nome"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdEstado"))))
                        cidade.Estado.Id = Convert.ToInt32(GetSqlDataReader()["IdEstado"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Estado"))))
                        cidade.Estado.Descricao = Convert.ToString(GetSqlDataReader()["Estado"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("EstadoSigla"))))
                        cidade.Estado.Sigla = Convert.ToString(GetSqlDataReader()["EstadoSigla"]);

                    lstCidadeVO.Add(cidade);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return lstCidadeVO;
        }

        public List<CidadeVO> Selecionar(string sql)
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

        public Dictionary<int, List<CidadeVO>> Paginar(string structs, int inicio, int fim)
        {
            Dictionary<int, List<CidadeVO>> dictionany = null;
            try
            {
                List<CidadeVO> lstCidade;
                dictionany = new Dictionary<int, List<CidadeVO>>();
                var sbPaginar = new StringBuilder();
                int total = GetTotalResgistro(structs);
                sbPaginar.AppendLine(structs);
                sbPaginar.AppendLine("ORDER BY DBAthon.dbo.Cidade.Nome ");
                sbPaginar.Append("OFFSET ").Append(inicio).AppendLine(" ROWS");
                sbPaginar.Append("FETCH NEXT ").Append(fim).AppendLine(" ROWS ONLY");
                lstCidade = Selecionar(sbPaginar.ToString());
                dictionany.Add(total, lstCidade);
                return dictionany;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<CidadeVO> Listar(CidadeVO objVO)
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