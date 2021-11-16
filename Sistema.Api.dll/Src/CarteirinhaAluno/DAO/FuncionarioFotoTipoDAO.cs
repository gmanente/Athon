using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.CarteirinhaAluno.VO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Sistema.Api.dll.Src.CarteirinhaAluno.DAO
{
    public class FuncionarioFotoTipoDAO: AbstractDAO, IDAO<FuncionarioFotoTipoVO>
    {
        public FuncionarioFotoTipoDAO(SqlCommand sqlComm)
            : base(sqlComm)
        {
        }

        public long Alterar(VO.FuncionarioFotoTipoVO objVO, string where = null)
        {
            throw new NotImplementedException();
        }

        public void Deletar(VO.FuncionarioFotoTipoVO objVO)
        {
            throw new NotImplementedException();
        }

        public long Inserir(VO.FuncionarioFotoTipoVO objVO)
        {
            try
            {
                objSbInsert = new StringBuilder();
                long IdAlunoFotoTipo = GetCodigoSequece("DBAlunoFotodbo.SeqFuncionarioFotoTipo    ");
                objSbInsert.AppendLine(@"INSERT INTO  DBAlunoFotodbo.FuncionarioFotoTipo          ");
                objSbInsert.AppendLine(@"(                                                  ");
                objSbInsert.AppendLine(@"             IdFuncionarioFotoTipo                       ");
                objSbInsert.AppendLine(@"          ,  Descricao                             ");
                objSbInsert.AppendLine(@")                                                  ");
                objSbInsert.AppendLine(@"VALUES                                             ");
                objSbInsert.AppendLine(@"(                                                  ");
                objSbInsert.AppendLine(@"          ,  @IdFuncionarioFotoTipo                      ");
                objSbInsert.AppendLine(@"          ,  @Descricao                            ");
                objSbInsert.AppendLine(@")                                                  ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbInsert.ToString();
                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdFuncionarioFotoTipo", SqlDbType.Int).Value = objVO.Id;
                GetSqlCommand().Parameters.Add("Descricao", SqlDbType.VarChar).Value = objVO.Descricao;
                GetSqlCommand().ExecuteNonQuery();
                return IdAlunoFotoTipo;

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

        public List<VO.FuncionarioFotoTipoVO> Selecionar(VO.FuncionarioFotoTipoVO objVO, int top = 0)
        {
            throw new NotImplementedException();
        }

        public VO.FuncionarioFotoTipoVO Consultar(VO.FuncionarioFotoTipoVO objVO)
        {
            throw new NotImplementedException();
        }

        public List<VO.FuncionarioFotoTipoVO> Listar(VO.FuncionarioFotoTipoVO objVO)
        {
            throw new NotImplementedException();
        }
    }
}
