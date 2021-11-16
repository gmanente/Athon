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
    public class EstadoDAO : AbstractDAO, IDAO<EstadoVO>
    {
        public EstadoDAO(SqlCommand sqlComm)
            : base(sqlComm)
        {

        }

        public long Inserir(EstadoVO objVO)
        {
            try
            {
                objSbInsert = new StringBuilder();
                //long CodigoBacen = GetCodigoSequece("DBAthon.dbo.SeqEstado    ");

                objSbInsert.AppendLine(@"INSERT INTO DBAthon.dbo.Estado     ");
                objSbInsert.AppendLine(@"(                                  ");
                objSbInsert.AppendLine(@"             CodigoIbge            ");
                objSbInsert.AppendLine(@"           , Nome                  ");
                objSbInsert.AppendLine(@"           , Sigla                 ");
                objSbInsert.AppendLine(@"           , IdPais                ");
                objSbInsert.AppendLine(@")                                  ");
                objSbInsert.AppendLine(@"     VALUES                        ");
                objSbInsert.AppendLine(@"(                                  ");
                objSbInsert.AppendLine(@"             @CodigoBacen          ");
                objSbInsert.AppendLine(@"           , @Nome                 ");
                objSbInsert.AppendLine(@"           , @Sigla                ");
                objSbInsert.AppendLine(@"           , @IdPais               ");
                objSbInsert.AppendLine(@")                                  ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbInsert.ToString();
                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("CodigoIbge", SqlDbType.Int).Value = objVO.Id;
                GetSqlCommand().Parameters.Add("Nome", SqlDbType.VarChar).Value = objVO.Descricao;
                GetSqlCommand().Parameters.Add("Sigla", SqlDbType.VarChar).Value = objVO.Sigla;
                GetSqlCommand().Parameters.Add("IdPais", SqlDbType.Int).Value = objVO.PaisVO.Id;
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

        public long Alterar(EstadoVO objVO, string where = null)
        {
            try
            {
                objSbUpdate = new StringBuilder();
                objSbUpdate.AppendLine(@"UPDATE DBAthon.dbo.Estado                        ");
                objSbUpdate.AppendLine(@"   SET                                           ");
                objSbUpdate.AppendLine(@"      Nome   = @Nome                             ");
                objSbUpdate.AppendLine(@"      Sigla  = @Sigla                            ");
                objSbUpdate.AppendLine(@"      IdPais = @IdPais                           ");

                if (where == null)
                {
                    objSbUpdate.AppendLine(@"WHERE CodigoIbge = @CodigoIbge               ");
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
                    GetSqlCommand().Parameters.Add("Nome", SqlDbType.VarChar).Value = objVO.Descricao;
                    GetSqlCommand().Parameters.Add("Sigla", SqlDbType.VarChar).Value = objVO.Sigla;
                    GetSqlCommand().Parameters.Add("IdPais", SqlDbType.Int).Value = objVO.PaisVO.Id;
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

        public void Deletar(EstadoVO objVO)
        {
            try
            {
                CheckDelete("DBAthon.dbo.Estado", "IdEstado", objVO.Id);
                objSbDelete = new StringBuilder();

                objSbDelete.AppendLine(@"DELETE FROM                                ");
                objSbDelete.AppendLine(@"   DBAthon.dbo.Estado WHERE                ");
                objSbDelete.AppendLine(@"      CodigoIbge =  @CodigoIbge            ");
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


        public List<EstadoVO> Selecionar(EstadoVO objVO, int top = 0)
        {
            EstadoVO estado = null;
            List<EstadoVO> lstEstado = null;

            try
            {
                objSbSelect = new StringBuilder();
                lstEstado = new List<EstadoVO>();

                objSbSelect.AppendLine(@"SELECT                           ");
                objSbSelect.AppendLine(@"       CodigoIbge                ");
                objSbSelect.AppendLine(@"     , Nome                      ");
                objSbSelect.AppendLine(@"     , Sigla                     ");
                objSbSelect.AppendLine(@"     , IdPais                    ");
                objSbSelect.AppendLine(@"  FROM DBAthon.dbo.Estado        ");
                objSbSelect.AppendLine(@" WHERE 1 = 1                     ");

                if (objVO != null)
                {
                    GetSqlCommand().Parameters.Clear();
                    if (objVO.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND CodigoIbge = @CodigoIbge");
                        GetSqlCommand().Parameters.Add("CodigoIbge", SqlDbType.Int).Value = objVO.Id;
                    }

                    if (!string.IsNullOrEmpty(objVO.Descricao))
                    {
                        objSbSelect.AppendLine(@" AND Nome = @Nome");
                        GetSqlCommand().Parameters.Add("Nome", SqlDbType.VarChar).Value = objVO.Descricao;
                    }

                    if (objVO.PaisVO.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND IdPais = @IdPais");
                        GetSqlCommand().Parameters.Add("IdPais", SqlDbType.Int).Value = objVO.PaisVO.Id;
                    }

                    if (!string.IsNullOrEmpty(objVO.PaisVO.NotInId))
                    {
                        objSbSelect.AppendLine(@" AND IdPais NOT IN("+ objVO.PaisVO.NotInId + ")");
                    }
                }

                objSbSelect.AppendLine(@" ORDER BY   Nome                  ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();

                while (GetSqlDataReader().Read())
                {
                    estado = new EstadoVO();
                    estado.PaisVO = new PaisVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("CodigoIbge"))))
                        estado.Id = Convert.ToInt32(GetSqlDataReader()["CodigoIbge"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Nome"))))
                    {
                        string _estado = Convert.ToString(GetSqlDataReader()["Nome"]);
                        _estado = Regex.Replace(_estado, @"\s(da|de|do|das|dos)\s", delegate (Match match) { return match.ToString().ToLower(); }, RegexOptions.IgnoreCase);
                        estado.Descricao = _estado;
                    }

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Sigla"))))
                        estado.Sigla = Convert.ToString(GetSqlDataReader()["Sigla"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdPais"))))
                        estado.PaisVO.Id = Convert.ToInt32(GetSqlDataReader()["IdPais"]);

                    lstEstado.Add(estado);

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
            return lstEstado;
        }

        public EstadoVO Consultar(EstadoVO objVO)
        {
            try
            {
                List<EstadoVO> lstEstado = Selecionar(objVO);
                return lstEstado.Count > 0 ? (EstadoVO)lstEstado.ToArray().GetValue(0) : null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private List<EstadoVO> GetLista()
        {
            EstadoVO estado = null;
            List<EstadoVO> lstEstadoVO = null;
            try
            {
                lstEstadoVO = new List<EstadoVO>();
                while (GetSqlDataReader().Read())
                {
                    estado = new EstadoVO();
                    estado.PaisVO = new PaisVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("CodigoIbge"))))
                        estado.Id = Convert.ToInt32(GetSqlDataReader()["CodigoIbge"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Nome"))))
                        estado.Descricao = Convert.ToString(GetSqlDataReader()["Nome"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Sigla"))))
                        estado.Sigla = Convert.ToString(GetSqlDataReader()["Sigla"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdPais"))))
                        estado.PaisVO.Id = Convert.ToInt32(GetSqlDataReader()["IdPais"]);

                    lstEstadoVO.Add(estado);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return lstEstadoVO;
        }

        public List<EstadoVO> Selecionar(string sql)
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

        public Dictionary<int, List<EstadoVO>> Paginar(string structs, int inicio, int fim)
        {
            Dictionary<int, List<EstadoVO>> dictionany = null;
            try
            {
                List<EstadoVO> lstEstado;
                dictionany = new Dictionary<int, List<EstadoVO>>();
                var sbPaginar = new StringBuilder();
                int total = GetTotalResgistro(structs);
                sbPaginar.AppendLine(structs);
                sbPaginar.AppendLine("ORDER BY DBAthon.dbo.Estado.Nome ");
                sbPaginar.Append("OFFSET ").Append(inicio).AppendLine(" ROWS");
                sbPaginar.Append("FETCH NEXT ").Append(fim).AppendLine(" ROWS ONLY");
                lstEstado = Selecionar(sbPaginar.ToString());
                dictionany.Add(total, lstEstado);
                return dictionany;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<EstadoVO> Listar(EstadoVO objVO)
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

        private List<EstadoVO> SelecionarComPais(EstadoVO objVO)
        {
            EstadoVO estado = null;
            List<EstadoVO> lstEstado = null;

            try
            {
                objSbSelect = new StringBuilder();
                lstEstado = new List<EstadoVO>();

                objSbSelect.AppendLine(@" SELECT Estado.CodigoIbge
                                               , Estado.Nome AS Estado
	                                           , Estado.Sigla AS SiglaEstado
	                                           , Estado.IdPais
	                                           , Pais.Nome AS Pais
                                          FROM DBAthon.dbo.Estado
                                          INNER JOIN DBAthon.dbo.Pais
	                                              ON Estado.IdPais = Pais.CodigoBacen
                                          WHERE 1 = 1");

                if (objVO != null)
                {
                    GetSqlCommand().Parameters.Clear();
                    if (objVO.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND Estado.CodigoIbge = @CodigoIbge");
                        GetSqlCommand().Parameters.Add("CodigoIbge", SqlDbType.Int).Value = objVO.Id;
                    }

                    if (!string.IsNullOrEmpty(objVO.Descricao))
                    {
                        objSbSelect.AppendLine(@" AND Estado.Nome = @Nome");
                        GetSqlCommand().Parameters.Add("Nome", SqlDbType.VarChar).Value = objVO.Descricao;
                    }

                    if (objVO.PaisVO.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND Estado.IdPais = @IdPais");
                        GetSqlCommand().Parameters.Add("IdPais", SqlDbType.Int).Value = objVO.PaisVO.Id;
                    }

                    if (!string.IsNullOrEmpty(objVO.PaisVO.NotInId))
                    {
                        objSbSelect.AppendLine(@" AND Estado.IdPais NOT IN(" + objVO.PaisVO.NotInId + ")");
                    }

                }

                objSbSelect.AppendLine(@" ORDER BY   Estado.Nome                  ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();

                while (GetSqlDataReader().Read())
                {
                    estado = new EstadoVO();
                    estado.PaisVO = new PaisVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("CodigoIbge"))))
                        estado.Id = Convert.ToInt32(GetSqlDataReader()["CodigoIbge"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Estado"))))
                    {
                        string _estado = Convert.ToString(GetSqlDataReader()["Estado"]);
                        _estado = Regex.Replace(_estado, @"\s(da|de|do|das|dos)\s", delegate (Match match) { return match.ToString().ToLower(); }, RegexOptions.IgnoreCase);
                        estado.Descricao = _estado;
                    }

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("SiglaEstado"))))
                        estado.Sigla = Convert.ToString(GetSqlDataReader()["SiglaEstado"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdPais"))))
                        estado.PaisVO.Id = Convert.ToInt32(GetSqlDataReader()["IdPais"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Pais"))))
                        estado.PaisVO.Descricao = Convert.ToString(GetSqlDataReader()["Pais"]);

                    lstEstado.Add(estado);

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
            return lstEstado;
        }

        public List<EstadoVO> ListarComPais(EstadoVO objVO)
        {
            try
            {
                return SelecionarComPais(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public EstadoVO ConsultarComPais(EstadoVO objVO)
        {
            try
            {
                List<EstadoVO> lstEstado = SelecionarComPais(objVO);
                return lstEstado.Count > 0 ? (EstadoVO)lstEstado.ToArray().GetValue(0) : null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}