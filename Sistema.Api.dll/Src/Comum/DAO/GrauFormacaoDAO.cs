using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Comum.VO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Sistema.Api.dll.Src.Comum.DAO
{
    public class GrauFormacaoDAO : AbstractDAO, IDAO<GrauFormacaoVO>
    {
        public GrauFormacaoDAO(SqlCommand sqlComm)
            : base(sqlComm)
        {

        }

        public long Inserir(GrauFormacaoVO objVO)
        {
            throw new NotImplementedException();
        }

        public long Alterar(GrauFormacaoVO objVO, string where = null)
        {
            throw new NotImplementedException();
        }

        public void Deletar(GrauFormacaoVO objVO)
        {
            throw new NotImplementedException();
        }


        public List<GrauFormacaoVO> Selecionar(GrauFormacaoVO objVO, int top = 0)
        {
            GrauFormacaoVO grauFormacao = null;
            List<GrauFormacaoVO> lstGrauFormacao = null;

            try
            {
                objSbSelect = new StringBuilder();
                lstGrauFormacao = new List<GrauFormacaoVO>();

                string varTop = "";
                if (top > 0)
                {
                    varTop = top.ToString();
                }

                objSbSelect.AppendLine(@"SELECT          ").Append(varTop);
                objSbSelect.AppendLine(@"        IdGrauFormacao           ");
                objSbSelect.AppendLine(@"      , Descricao              ");
                objSbSelect.AppendLine(@"  FROM DBAthon.dbo.GrauFormacao  ");
                objSbSelect.AppendLine(@"WHERE 1 = 1                    ");

                if (objVO != null)
                {
                    GetSqlCommand().Parameters.Clear();
                    if (objVO.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND IdGrauFormacao = @IdGrauFormacao");
                        GetSqlCommand().Parameters.Add("IdHabilitacao", SqlDbType.Int).Value = objVO.Id;
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
                    grauFormacao = new GrauFormacaoVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdGrauFormacao"))))
                        grauFormacao.Id = Convert.ToInt32(GetSqlDataReader()["IdGrauFormacao"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Descricao"))))
                        grauFormacao.Descricao = Convert.ToString(GetSqlDataReader()["Descricao"]);

                    lstGrauFormacao.Add(grauFormacao);

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
            return lstGrauFormacao;
        }

        public GrauFormacaoVO Consultar(GrauFormacaoVO objVO)
        {
            try
            {
                return (GrauFormacaoVO)Selecionar(objVO).ToArray().GetValue(0);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<GrauFormacaoVO> Listar(GrauFormacaoVO objVO)
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