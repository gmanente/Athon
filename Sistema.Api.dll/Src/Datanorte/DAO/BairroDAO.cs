using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.Datanorte.VO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Sistema.Api.dll.Src.Datanorte.DAO
{
    class BairroDAO : AbstractDAO, IDAO<BairroVO>
    {
        public BairroDAO(SqlCommand sqlComm)
            : base(sqlComm)
        {

        }

        public long Alterar(BairroVO objVO, string where = null)
        {
            throw new NotImplementedException();
        }

        public BairroVO Consultar(BairroVO objVO)
        {
            try
            {
                List<BairroVO> lst = Selecionar(objVO);

                return lst.Count > 0 ? (BairroVO)lst.ToArray().GetValue(0) : null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Deletar(BairroVO objVO)
        {
            throw new NotImplementedException();
        }

        public long Inserir(BairroVO objVO)
        {
            throw new NotImplementedException();
        }

        public List<BairroVO> Listar(BairroVO objVO)
        {
            try
            {
                return Selecionar(objVO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BairroVO> Selecionar(BairroVO objVO, int top = 0)
        {
            {
                try
                {
                    objSbSelect = new StringBuilder();

                    objSbSelect.AppendLine(@"
                        SELECT DISTINCT 
                               UF
                              ,Cidade
                              ,Bairro
	                      FROM DBAthon.dbo.UvwBairro
                         WHERE 1 = 1 ");

                    if (objVO != null)
                    {
                        GetSqlCommand().Parameters.Clear();

                        if (!string.IsNullOrEmpty(objVO.UF))
                        {
                            objSbSelect.AppendLine(@" AND DBAthon.dbo.UvwBairro.UF = @UF");
                            GetSqlCommand().Parameters.Add("UF", SqlDbType.VarChar).Value = objVO.UF;
                        }

                        if (!string.IsNullOrEmpty(objVO.Cidade))
                        {
                            objSbSelect.AppendLine(@" AND DBAthon.dbo.UvwBairro.Cidade = @Cidade");
                            GetSqlCommand().Parameters.Add("Cidade", SqlDbType.VarChar).Value = objVO.Cidade;
                        }
                    }

                    objSbSelect.AppendLine(@" ORDER BY DBAthon.dbo.UvwBairro.Cidade, DBAthon.dbo.UvwBairro.Bairro ");

                    GetSqlCommand().CommandText = "";
                    GetSqlCommand().CommandText = objSbSelect.ToString();


                    List<BairroVO> lst = new List<BairroVO>();

                    while (GetSqlDataReader().Read())
                    {
                        BairroVO bairroVO = new BairroVO();

                        if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Bairro"))))
                            bairroVO.Bairro = Convert.ToString(GetSqlDataReader()["Bairro"]);

                        if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Cidade"))))
                            bairroVO.Cidade = Convert.ToString(GetSqlDataReader()["Cidade"]);

                        if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("UF"))))
                            bairroVO.UF = Convert.ToString(GetSqlDataReader()["UF"]);


                        lst.Add(bairroVO);
                    }

                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (objSbSelect != null)
                        objSbSelect = null;

                    Close();
                }
            }
        }
    }
}
