using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Comum.VO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Sistema.Api.dll.Src.Comum.DAO
{
    public class BlocoDAO : AbstractDAO, IDAO<BlocoVO>
    {
        public BlocoDAO(SqlCommand sqlComm)
            : base(sqlComm)
        {
        }

        public long Inserir(BlocoVO objVO)
        {
            try
            {
                objSbInsert = new StringBuilder();
                long IdBloco = GetCodigoSequece("DBAthon.dbo.SeqBloco         ");
                objSbInsert.AppendLine(@"INSERT INTO  DBAthon.dbo.Bloco       ");
                objSbInsert.AppendLine(@"(                                    ");
                objSbInsert.AppendLine(@"             IdBloco                 ");
                objSbInsert.AppendLine(@"          ,  IdCampus                ");
                objSbInsert.AppendLine(@"          ,  Descricao               ");
                objSbInsert.AppendLine(@"          ,  Sigla                   ");
                objSbInsert.AppendLine(@"          ,  SiglaGeral              ");
                objSbInsert.AppendLine(@")                                    ");
                objSbInsert.AppendLine(@"VALUES                               ");
                objSbInsert.AppendLine(@"(                                    ");
                objSbInsert.AppendLine(@"             @IdBloco                ");
                objSbInsert.AppendLine(@"          ,  @IdCampus               ");
                objSbInsert.AppendLine(@"          ,  @Descricao              ");
                objSbInsert.AppendLine(@"          ,  @Sigla                  ");
                objSbInsert.AppendLine(@"          ,  @SiglaGeral             ");
                objSbInsert.AppendLine(@")");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbInsert.ToString();
                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdBloco", SqlDbType.Int).Value = IdBloco;
                GetSqlCommand().Parameters.Add("IdCampus", SqlDbType.Int).Value = objVO.Campus.Id;
                GetSqlCommand().Parameters.Add("Descricao", SqlDbType.VarChar).Value = objVO.Descricao;
                GetSqlCommand().Parameters.Add("Sigla", SqlDbType.VarChar).Value = objVO.Sigla;
                GetSqlCommand().Parameters.Add("SiglaGeral", SqlDbType.VarChar).Value = objVO.SiglaGeral;
                GetSqlCommand().ExecuteNonQuery();
                return IdBloco;

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

        public long Alterar(BlocoVO objVO, string where = null)
        {
            try
            {
                objSbUpdate = new StringBuilder();
                objSbUpdate.AppendLine("UPDATE DBAthon.dbo.Bloco     ");
                objSbUpdate.AppendLine("   SET IdCampus = @IdCampus  ");
                objSbUpdate.AppendLine("     , Descricao = @Descricao");
                objSbUpdate.AppendLine("     , Sigla = @Sigla        ");
                objSbUpdate.AppendLine("     , SiglaGeral = @SiglaGeral        ");

                if (where == null)
                {
                    objSbUpdate.AppendLine(" WHERE IdBloco = @IdBloco");
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
                    GetSqlCommand().Parameters.Add("IdBloco", SqlDbType.Int).Value = objVO.Id;
                    GetSqlCommand().Parameters.Add("IdCampus", SqlDbType.Int).Value = objVO.Campus.Id;
                    GetSqlCommand().Parameters.Add("Descricao", SqlDbType.VarChar).Value = objVO.Descricao;
                    GetSqlCommand().Parameters.Add("Sigla", SqlDbType.Char).Value = objVO.Sigla;
                    GetSqlCommand().Parameters.Add("SiglaGeral", SqlDbType.Char).Value = objVO.SiglaGeral;
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

        public void Deletar(BlocoVO objVO)
        {
            try
            {
                CheckDelete("DBAthon.dbo.Bloco", "IdBloco", objVO.Id);

                objSbDelete = new StringBuilder();
                objSbDelete.AppendLine("DELETE FROM DBAthon.dbo.Bloco ");
                objSbDelete.AppendLine(" WHERE IdBloco = @IdBloco     ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbDelete.ToString();
                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdBloco", SqlDbType.Int).Value = objVO.Id;

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

        public List<BlocoVO> Listar(BlocoVO objVO)
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

        public List<BlocoVO> Selecionar(string sql)
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

        public BlocoVO Consultar(BlocoVO objVO)
        {
            try
            {
                List<BlocoVO> lstBlocoVO = Selecionar(objVO);
                return lstBlocoVO.Count > 0 ? (BlocoVO)lstBlocoVO.ToArray().GetValue(0) : null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<BlocoVO> GetLista()
        {
            BlocoVO bloco = null;
            List<BlocoVO> lstBlocoVO = null;
            try
            {
                lstBlocoVO = new List<BlocoVO>();
                while (GetSqlDataReader().Read())
                {
                    bloco = new BlocoVO();
                    bloco.Campus = new CampusVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdBloco"))))
                        bloco.Id = Convert.ToInt64(GetSqlDataReader()["IdBloco"]);
                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Descricao"))))
                        bloco.Descricao = Convert.ToString(GetSqlDataReader()["Descricao"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Sigla"))))
                        bloco.Sigla = Convert.ToString(GetSqlDataReader()["Sigla"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("SiglaGeral"))))
                        bloco.SiglaGeral = Convert.ToString(GetSqlDataReader()["SiglaGeral"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdCampus"))))
                        bloco.Campus.Id = Convert.ToInt64(GetSqlDataReader()["IdCampus"]);

                    lstBlocoVO.Add(bloco);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return lstBlocoVO;
        }

        public List<BlocoVO> Selecionar(BlocoVO objVO, int top = 0)
        {
            List<BlocoVO> lstBloco = null;

            try
            {
                objSbSelect = new StringBuilder();
                lstBloco = new List<BlocoVO>();

                string varTop = "";
                if (top > 0)
                {
                    varTop = top.ToString();
                }

                objSbSelect.AppendLine(@"SELECT                             ");
                objSbSelect.AppendLine(@"      DBAthon.dbo.Bloco.IdBloco    ");
                objSbSelect.AppendLine(@"    , DBAthon.dbo.Bloco.IdCampus   ");
                objSbSelect.AppendLine(@"    , DBAthon.dbo.Bloco.Descricao  ");
                objSbSelect.AppendLine(@"    , DBAthon.dbo.Bloco.Sigla      ");
                objSbSelect.AppendLine(@"    , DBAthon.dbo.Bloco.SiglaGeral ");
                objSbSelect.AppendLine(@" FROM DBAthon.dbo.Bloco            ");
                objSbSelect.AppendLine(@" WHERE 1 = 1                       ");

                if (objVO != null)
                {
                    GetSqlCommand().Parameters.Clear();
                    if (objVO.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.Bloco.IdBloco = @IdBloco");
                        GetSqlCommand().Parameters.Add("IdBloco", SqlDbType.Int).Value = objVO.Id;
                    }
                    if (objVO.Campus.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.Bloco.IdCampus = @IdCampus");
                        GetSqlCommand().Parameters.Add("IdCampus", SqlDbType.Int).Value = objVO.Campus.Id;
                    }
                }

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
    }
}