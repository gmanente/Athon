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
    public class Contato_LancamentoDAO : AbstractDAO, IDAO<Contato_LancamentoVO>
    {
        public Contato_LancamentoDAO(SqlCommand sqlComm) : base(sqlComm)
        {
        }

        public long Alterar(Contato_LancamentoVO objVO, string where = null)
        {
            try
            {
                objSbUpdate = new StringBuilder();

                objSbUpdate.AppendLine(" UPDATE DBAthon.dbo.Contato_Lancamento                                        ");
                objSbUpdate.AppendLine("    SET DBAthon.dbo.Contato_Lancamento.DataOperacao = @DataOperacao     ");
                objSbUpdate.AppendLine("       ,DBAthon.dbo.Contato_Lancamento.Observacao   = @Observacao     ");

                if (where == null)
                {
                    objSbUpdate.AppendLine(@"WHERE DBAthon.dbo.Contato_Lancamento.Contato_Lancamento_Id = @Contato_Lancamento_Id");
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
                    GetSqlCommand().Parameters.Add("Contato_Lancamento_Id", SqlDbType.Int).Value = objVO.Contato_Lancamento_Id;
                    GetSqlCommand().Parameters.Add("DataOperacao", SqlDbType.Int).Value = objVO.DataOperacao;
                    GetSqlCommand().Parameters.Add("Observacao", SqlDbType.VarChar).Value = objVO.Observacao;

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

        public Contato_LancamentoVO Consultar(Contato_LancamentoVO objVO)
        {
            throw new NotImplementedException();
        }

        public void Deletar(Contato_LancamentoVO objVO)
        {
            try
            {
                objSbDelete = new StringBuilder();

                CheckDelete("DBAthon.dbo.Contato_Lancamento", "Contato_Lancamento_Id", objVO.Id);

                objSbDelete.AppendLine(@"DELETE FROM DBAthon.dbo.Contato_Lancamento WHERE Contato_Lancamento_Id = @Contato_Lancamento_Id ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbDelete.ToString();

                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("Contato_Lancamento_Id", SqlDbType.Int).Value = objVO.Id;

                GetSqlCommand().ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                throw e;
            }
            finally
            {
                if (objSbDelete != null)
                    objSbDelete = null;
            }
        }

        public long Inserir(Contato_LancamentoVO objVO)
        {
            try
            {
                objSbInsert = new StringBuilder();

                long Contato_Lancamento_Id = GetCodigoSequece("DBAthon.dbo.SeqContato_Lancamento ");

                objSbInsert.AppendLine(@"INSERT INTO DBAthon.dbo.Contato_Lancamento   ");
                objSbInsert.AppendLine(@"(                                            ");
                objSbInsert.AppendLine(@"       Contato_Lancamento_Id                 ");
                objSbInsert.AppendLine(@"      ,Contato_Id                            ");
                objSbInsert.AppendLine(@"      ,DataOperacao                          ");
                objSbInsert.AppendLine(@"      ,IdUsuario                             ");
                objSbInsert.AppendLine(@"      ,Observacao                            ");
                objSbInsert.AppendLine(@")                                            ");
                objSbInsert.AppendLine(@"     VALUES                                  ");
                objSbInsert.AppendLine(@"(                                            ");
                objSbInsert.AppendLine(@"       @Contato_Lancamento_Id                ");
                objSbInsert.AppendLine(@"      ,@Contato_Id                           ");
                objSbInsert.AppendLine(@"      ,GETDATE()                             ");
                objSbInsert.AppendLine(@"      ,@IdUsuario                            ");
                objSbInsert.AppendLine(@"      ,@Observacao                           ");
                objSbInsert.AppendLine(@")                                            ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbInsert.ToString();

                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("Contato_Lancamento_Id", SqlDbType.Int).Value = Contato_Lancamento_Id;
                GetSqlCommand().Parameters.Add("Contato_Id", SqlDbType.Int).Value = objVO.Contato_Id;
                GetSqlCommand().Parameters.Add("IdUsuario", SqlDbType.Int).Value = objVO.Usuario.Id;
                GetSqlCommand().Parameters.Add("Observacao", SqlDbType.VarChar).Value = objVO.Observacao;

                GetSqlCommand().ExecuteNonQuery();

                return Contato_Lancamento_Id;
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

        public List<Contato_LancamentoVO> Listar(Contato_LancamentoVO objVO)
        {
            throw new NotImplementedException();
        }

        public List<Contato_LancamentoVO> Selecionar(Contato_LancamentoVO objVO, int top = 0)
        {
            throw new NotImplementedException();
        }
    }
   
}
