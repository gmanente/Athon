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
    public class CidadeDAO : AbstractDAO, IDAO<CidadeVO>
    {
        public CidadeDAO(SqlCommand sqlComm)
            : base(sqlComm)
        {

        }

        public long Alterar(CidadeVO objVO, string where = null)
        {
            throw new NotImplementedException();
        }

        public CidadeVO Consultar(CidadeVO objVO)
        {
            try
            {
                List<CidadeVO> lst = Selecionar(objVO);

                return lst.Count > 0 ? (CidadeVO)lst.ToArray().GetValue(0) : null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Deletar(CidadeVO objVO)
        {
            throw new NotImplementedException();
        }

        public long Inserir(CidadeVO objVO)
        {
            throw new NotImplementedException();
        }

        public List<CidadeVO> Listar(CidadeVO objVO)
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

        public List<CidadeVO> Selecionar(CidadeVO objVO, int top = 0)
        {
            try
            {
                objSbSelect = new StringBuilder();

                objSbSelect.AppendLine(@"
                        SELECT DISTINCT 
                               UF
                              ,Cidade
	                      FROM DBAthon.dbo.UvwCidade
                         WHERE 1 = 1 ");

                if (objVO != null)
                {
                    GetSqlCommand().Parameters.Clear();

                    if (!string.IsNullOrEmpty(objVO.UF))
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.UvwCidade.UF = @UF");
                        GetSqlCommand().Parameters.Add("UF", SqlDbType.VarChar).Value = objVO.UF;
                    }
                }

                objSbSelect.AppendLine(@" ORDER BY DBAthon.dbo.UvwCidade.Cidade ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();


                List<CidadeVO> lst = new List<CidadeVO>();

                while (GetSqlDataReader().Read())
                {
                    CidadeVO cidadeVO = new CidadeVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Cidade"))))
                        cidadeVO.Nome = Convert.ToString(GetSqlDataReader()["Cidade"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("UF"))))
                        cidadeVO.UF = Convert.ToString(GetSqlDataReader()["UF"]);


                    lst.Add(cidadeVO);
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
