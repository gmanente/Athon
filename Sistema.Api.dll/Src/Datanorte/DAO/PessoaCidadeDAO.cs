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
    public class PessoaCidadeDAO : AbstractDAO, IDAO<PessoaCidadeVO>
    {
        public PessoaCidadeDAO(SqlCommand sqlComm)
            : base(sqlComm)
        {

        }

        public long Alterar(PessoaCidadeVO objVO, string where = null)
        {
            throw new NotImplementedException();
        }

        public PessoaCidadeVO Consultar(PessoaCidadeVO objVO)
        {
            try
            {
                List<PessoaCidadeVO> lst = Selecionar(objVO);

                return lst.Count > 0 ? (PessoaCidadeVO)lst.ToArray().GetValue(0) : null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Deletar(PessoaCidadeVO objVO)
        {
            throw new NotImplementedException();
        }

        public long Inserir(PessoaCidadeVO objVO)
        {
            throw new NotImplementedException();
        }

        public List<PessoaCidadeVO> Listar(PessoaCidadeVO objVO)
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

        public List<PessoaCidadeVO> Selecionar(PessoaCidadeVO objVO, int top = 0)
        {
            try
            {
                objSbSelect = new StringBuilder();

                objSbSelect.AppendLine(@"
                        SELECT DBAthon.dbo.PessoaCidade.IdPessoaCidade 
                              ,DBAthon.dbo.PessoaCidade.Nome
                              ,DBAthon.dbo.PessoaCidade.UF
	                      FROM DBAthon.dbo.PessoaCidade
                         WHERE 1 = 1 ");

                if (objVO != null)
                {
                    GetSqlCommand().Parameters.Clear();

                    if (objVO.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.PessoaCidade.IdPessoaCidade = @IdPessoaCidade");
                        GetSqlCommand().Parameters.Add("IdPessoaCidade", SqlDbType.Int).Value = objVO.Id;
                    }
                                        
                    if (!string.IsNullOrEmpty(objVO.UF))
                    {
                        objSbSelect.AppendLine(@" AND DBAthon.dbo.PessoaCidade.UF = @UF");
                        GetSqlCommand().Parameters.Add("UF", SqlDbType.Int).Value = objVO.UF;
                    }                   
                }

                objSbSelect.AppendLine(@" ORDER BY DBAthon.dbo.PessoaCidade.UF ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();


                List<PessoaCidadeVO> lst = new List<PessoaCidadeVO>();

                while (GetSqlDataReader().Read())
                {
                    PessoaCidadeVO pessoaCidadeVO = new PessoaCidadeVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdPessoaCidade"))))
                        pessoaCidadeVO.Id = Convert.ToInt64(GetSqlDataReader()["IdPessoaCidade"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Nome"))))
                        pessoaCidadeVO.Nome = Convert.ToString(GetSqlDataReader()["Nome"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("UF"))))
                        pessoaCidadeVO.UF  = Convert.ToString(GetSqlDataReader()["UF"]);


                    lst.Add(pessoaCidadeVO);
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