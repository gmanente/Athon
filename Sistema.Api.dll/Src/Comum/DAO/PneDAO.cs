using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Comum.VO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Sistema.Api.dll.Src.Comum.DAO
{
    public class PneDAO : AbstractDAO, IDAO<PneVO>
    {
        public PneDAO(SqlCommand sqlComm)
            : base(sqlComm)
        {

        }

        public long Inserir(PneVO objVO)
        {
            throw new NotImplementedException();
        }

        public long Alterar(PneVO objVO, string where = null)
        {
            throw new NotImplementedException();
        }

        public void Deletar(PneVO objVO)
        {
            throw new NotImplementedException();
        }


        public List<PneVO> Selecionar(PneVO objVO, int top = 0)
        {
            PneVO pne = null;
            List<PneVO> lstPne = null;

            try
            {
                objSbSelect = new StringBuilder();
                lstPne = new List<PneVO>();

                string varTop = "";
                if (top > 0)
                {
                    varTop = top.ToString();
                }

                objSbSelect.AppendLine(@"SELECT                                     ").Append(varTop);
                objSbSelect.AppendLine(@"        IdPne                              ");
                objSbSelect.AppendLine(@"      , Descricao                          ");
                objSbSelect.AppendLine(@"  FROM DBAthon.dbo.Pne                     ");
                objSbSelect.AppendLine(@"WHERE 1 = 1                                ");

                if (objVO != null)
                {
                    GetSqlCommand().Parameters.Clear();
                    if (objVO.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND IdPne = @IdPne");
                        GetSqlCommand().Parameters.Add("IdPne", SqlDbType.Int).Value = objVO.Id;
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
                    pne = new PneVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdPne"))))
                        pne.Id = Convert.ToInt32(GetSqlDataReader()["IdPne"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Descricao"))))
                        pne.Descricao = Convert.ToString(GetSqlDataReader()["Descricao"]);

                    lstPne.Add(pne);

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
            return lstPne;
        }


        public List<PneVO> SelecionarPne(PneVO objVO, int top = 0)
        {
            PneVO pne = null;
            List<PneVO> lstPne = null;

            try
            {
                objSbSelect = new StringBuilder();
                lstPne = new List<PneVO>();

                string varTop = "";
                if (top > 0)
                {
                    varTop = top.ToString();
                }

                objSbSelect.AppendLine(@"SELECT                                     ").Append(varTop);
                objSbSelect.AppendLine(@"        IdPne                              ");
                objSbSelect.AppendLine(@"      , Descricao                          ");
                objSbSelect.AppendLine(@"  FROM DBAthon.dbo.Pne                     ");
                objSbSelect.AppendLine(@"WHERE IdPne <> 1                           ");

                if (objVO != null)
                {
                    GetSqlCommand().Parameters.Clear();
                    if (objVO.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND IdPne = @IdPne");
                        GetSqlCommand().Parameters.Add("IdPne", SqlDbType.Int).Value = objVO.Id;
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
                    pne = new PneVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdPne"))))
                        pne.Id = Convert.ToInt32(GetSqlDataReader()["IdPne"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Descricao"))))
                        pne.Descricao = Convert.ToString(GetSqlDataReader()["Descricao"]);

                    lstPne.Add(pne);

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
            return lstPne;
        }

        public PneVO Consultar(PneVO objVO)
        {
            try
            {
                return (PneVO)Selecionar(objVO).ToArray().GetValue(0);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<PneVO> Listar(PneVO objVO)
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

        public List<PneVO> ListarPne(PneVO objVO)
        {
            try
            {
                return SelecionarPne(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}