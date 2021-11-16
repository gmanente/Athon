using Sistema.Api.dll.Interface;
using Sistema.Api.dll.Src.CarteirinhaAluno.VO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Sistema.Api.dll.Src.CarteirinhaAluno.DAO
{
     public class FuncionarioFotoLogDAO : AbstractDAO, IDAO<FuncionarioFotoLogVO>
      {
         public FuncionarioFotoLogDAO(SqlCommand sqlComm)
            : base(sqlComm)
        {
        }




         public long Alterar(FuncionarioFotoLogVO objVO, string where = null)
         {
             throw new NotImplementedException();
         }

         public void Deletar(FuncionarioFotoLogVO objVO)
         {
             throw new NotImplementedException();
         }

         public long Inserir(FuncionarioFotoLogVO objVO)
         {
             try
             {
                 objSbInsert = new StringBuilder();
                 long IdFuncionarioFotoLog = GetCodigoSequece("DBAlunoFoto.dbo.SeqFuncionarioFotoLog");
                 objSbInsert.AppendLine(@"INSERT INTO  DBAlunoFoto.dbo.FuncionarioFotoLog      ");
                 objSbInsert.AppendLine(@"(                                              ");
                 objSbInsert.AppendLine(@"             IdFuncionarioFotoLog                    ");
                 objSbInsert.AppendLine(@"          ,  IdFuncionarioFoto                       ");
                 objSbInsert.AppendLine(@"          ,  IdUsuario                         ");
                 objSbInsert.AppendLine(@"          ,  DataImpressao                     ");
                 objSbInsert.AppendLine(@")                                              ");
                 objSbInsert.AppendLine(@"VALUES                                         ");
                 objSbInsert.AppendLine(@"(                                              ");
                 objSbInsert.AppendLine(@"             @IdFuncionarioFotoLog                   ");
                 objSbInsert.AppendLine(@"          ,  @IdFuncionarioFoto                      ");
                 objSbInsert.AppendLine(@"          ,  @IdUsuario                        ");
                 objSbInsert.AppendLine(@"          ,  @DataImpressao                    ");
                 objSbInsert.AppendLine(@")                                              ");

                 GetSqlCommand().CommandText = "";
                 GetSqlCommand().CommandText = objSbInsert.ToString();
                 GetSqlCommand().Parameters.Clear();
                 GetSqlCommand().Parameters.Add("IdFuncionarioFotoLog", SqlDbType.Int).Value = IdFuncionarioFotoLog;
                 GetSqlCommand().Parameters.Add("IdFuncionarioFoto", SqlDbType.Int).Value = objVO.FuncionarioFoto.Id;
                 GetSqlCommand().Parameters.Add("IdUsuario", SqlDbType.Int).Value = objVO.Usuario.Id;
                 GetSqlCommand().Parameters.Add("DataImpressao", SqlDbType.DateTime).Value = objVO.DataImpressao;
                 GetSqlCommand().ExecuteNonQuery();

                 return IdFuncionarioFotoLog;

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

         public List<FuncionarioFotoLogVO> Selecionar(FuncionarioFotoLogVO objVO, int top = 0)
         {
             throw new NotImplementedException();
         }

         public FuncionarioFotoLogVO Consultar(FuncionarioFotoLogVO objVO)
         {
             throw new NotImplementedException();
         }

         public List<FuncionarioFotoLogVO> Listar(FuncionarioFotoLogVO objVO)
         {
             throw new NotImplementedException();
         }
    }
}
