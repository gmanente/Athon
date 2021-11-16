using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Comum.VO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Sistema.Api.dll.Src.Comum.DAO
{
    public class EstadoCivilDAO : AbstractDAO, IDAO<EstadoCivilVO>
    {
        public EstadoCivilDAO(SqlCommand sqlComm)
            : base(sqlComm)
        {

        }

        public long Inserir(EstadoCivilVO objVO)
        {
            try
            {
                objSbInsert = new StringBuilder();
                long idEstadoCivil = GetCodigoSequece("DBAthon.dbo.SeqEstadoCivil");

                objSbInsert.AppendLine(@"INSERT INTO DBAthon.dbo.EstadoCivil  ");
                objSbInsert.AppendLine(@"(                                    ");
                objSbInsert.AppendLine(@"             IdEstadoCivil           ");
                objSbInsert.AppendLine(@"           , Descricao               ");
                objSbInsert.AppendLine(@")                                    ");
                objSbInsert.AppendLine(@"     VALUES                          ");
                objSbInsert.AppendLine(@"(                                    ");
                objSbInsert.AppendLine(@"             @IdEstadoCivil          ");
                objSbInsert.AppendLine(@"           , @Descricao              ");
                objSbInsert.AppendLine(@")                                    ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbInsert.ToString();
                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdEstadoCivil", SqlDbType.Int).Value = idEstadoCivil;
                GetSqlCommand().Parameters.Add("Descricao", SqlDbType.VarChar).Value = objVO.Descricao;
                GetSqlCommand().ExecuteNonQuery();
                return idEstadoCivil;
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

        public long Alterar(EstadoCivilVO objVO, string where = null)
        {
            try
            {
                objSbUpdate = new StringBuilder();
                objSbUpdate.AppendLine(@"UPDATE DBAthon.dbo.EstadoCivil         ");
                objSbUpdate.AppendLine(@"  SET                                  ");
                objSbUpdate.AppendLine(@"      Descricao = @Descricao           ");

                if (where == null)
                {
                    objSbUpdate.AppendLine(@"WHERE IdEstadoCivil = @IdEstadoCivil    ");
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
                    GetSqlCommand().Parameters.Add("IdEstadoCivil", SqlDbType.Int).Value = objVO.Id;
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

        public void Deletar(EstadoCivilVO objVO)
        {
            try
            {
                CheckDelete("DBAthon.dbo.EstadoCivil", "IdEstadoCivil", objVO.Id);
                objSbDelete = new StringBuilder();

                objSbDelete.AppendLine(@"DELETE FROM                             ");
                objSbDelete.AppendLine(@"   DBAthon.dbo.EstadoCivil WHERE        ");
                objSbDelete.AppendLine(@"      IdEstadoCivil =  @IdEstadoCivil   ");
                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbDelete.ToString();
                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdEstadoCivil", SqlDbType.Int).Value = objVO.Id;
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


        public List<EstadoCivilVO> Selecionar(EstadoCivilVO objVO, int top = 0)
        {
            EstadoCivilVO estadoCivil = null;
            List<EstadoCivilVO> lstEstadoCivil = null;

            try
            {
                objSbSelect = new StringBuilder();
                lstEstadoCivil = new List<EstadoCivilVO>();

                string varTop = "";
                if (top > 0)
                {
                    varTop = top.ToString();
                }

                objSbSelect.AppendLine(@"SELECT                           ").Append(varTop);
                objSbSelect.AppendLine(@"        IdEstadoCivil            ");
                objSbSelect.AppendLine(@"      , Descricao                ");
                objSbSelect.AppendLine(@"  FROM DBAthon.dbo.EstadoCivil   ");
                objSbSelect.AppendLine(@"WHERE 1 = 1                      ");

                if (objVO != null)
                {
                    GetSqlCommand().Parameters.Clear();
                    if (objVO.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND IdEstadoCivil = @IdEstadoCivil");
                        GetSqlCommand().Parameters.Add("IdEstadoCivil", SqlDbType.Int).Value = objVO.Id;
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
                    estadoCivil = new EstadoCivilVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdEstadoCivil"))))
                        estadoCivil.Id = Convert.ToInt32(GetSqlDataReader()["IdEstadoCivil"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Descricao"))))
                        estadoCivil.Descricao = Convert.ToString(GetSqlDataReader()["Descricao"]);

                    lstEstadoCivil.Add(estadoCivil);

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
            return lstEstadoCivil;
        }

        public EstadoCivilVO Consultar(EstadoCivilVO objVO)
        {
            try
            {
                List<EstadoCivilVO> lstEstadoCivil = Selecionar(objVO);
                return lstEstadoCivil.Count > 0 ? (EstadoCivilVO)lstEstadoCivil.ToArray().GetValue(0) : null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        private List<EstadoCivilVO> GetLista()
        {
            EstadoCivilVO estadoCivilVO = null;
            List<EstadoCivilVO> lstEstadoCivilVO = null;
            try
            {
                lstEstadoCivilVO = new List<EstadoCivilVO>();
                while (GetSqlDataReader().Read())
                {
                    estadoCivilVO = new EstadoCivilVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdEstadoCivil"))))
                        estadoCivilVO.Id = Convert.ToInt32(GetSqlDataReader()["IdEstadoCivil"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Descricao"))))
                        estadoCivilVO.Descricao = Convert.ToString(GetSqlDataReader()["Descricao"]);

                    lstEstadoCivilVO.Add(estadoCivilVO);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return lstEstadoCivilVO;
        }

        public List<EstadoCivilVO> Selecionar(string sql)
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


        public Dictionary<int, List<EstadoCivilVO>> Paginar(string structs, int inicio, int fim)
        {
            Dictionary<int, List<EstadoCivilVO>> dictionany = null;
            try
            {
                List<EstadoCivilVO> lstEstadoCivil;
                dictionany = new Dictionary<int, List<EstadoCivilVO>>();
                var sbPaginar = new StringBuilder();
                int total = GetTotalResgistro(structs);
                sbPaginar.AppendLine(structs);
                sbPaginar.AppendLine("ORDER BY DBAthon.dbo.EstadoCivil.Descricao ");
                sbPaginar.Append("OFFSET ").Append(inicio).AppendLine(" ROWS");
                sbPaginar.Append("FETCH NEXT ").Append(fim).AppendLine(" ROWS ONLY");
                lstEstadoCivil = Selecionar(sbPaginar.ToString());
                dictionany.Add(total, lstEstadoCivil);
                return dictionany;

            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public List<EstadoCivilVO> Listar(EstadoCivilVO objVO)
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