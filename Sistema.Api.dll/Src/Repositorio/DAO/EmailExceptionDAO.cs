using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Sistema.Api.dll.Src.Repositorio.DAO
{
    public class EmailExceptionDAO : AbstractDAO
    {

        public EmailExceptionDAO(SqlCommand sqlComm)
            : base(sqlComm)
        {

        }

        public void GravarErroEmail(string emailRemetente, string emailDestino, string messageException)
        {
            try
            {

                objSbInsert = new StringBuilder();
                long idEamailException = GetCodigoSequece("DBAthon.dbo.SeqEmailException");

                objSbInsert.AppendLine(@"INSERT INTO DBAthon.dbo.EmailException ");
                objSbInsert.AppendLine(@"(                                      ");
                objSbInsert.AppendLine(@"           IdEmailException            ");
                objSbInsert.AppendLine(@"          ,EmailRemetente              ");
                objSbInsert.AppendLine(@"          ,EmailDestino                ");
                objSbInsert.AppendLine(@"          ,MensagemErro                ");
                objSbInsert.AppendLine(@")                                      ");
                objSbInsert.AppendLine(@"     VALUES                            ");
                objSbInsert.AppendLine(@"(                                      ");
                objSbInsert.AppendLine(@"           @IdEmailException           ");
                objSbInsert.AppendLine(@"          ,@EmailRemetente              ");
                objSbInsert.AppendLine(@"          ,@EmailDestino               ");
                objSbInsert.AppendLine(@"          ,@MensagemErro               ");
                objSbInsert.AppendLine(@")                                      ");


                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbInsert.ToString();
                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdEmailException", SqlDbType.Int).Value = idEamailException;
                GetSqlCommand().Parameters.Add("EmailDestino", SqlDbType.VarChar).Value = emailDestino;
                GetSqlCommand().Parameters.Add("EmailRemetente", SqlDbType.VarChar).Value = emailRemetente;
                GetSqlCommand().Parameters.Add("MensagemErro", SqlDbType.VarChar).Value = messageException;
                GetSqlCommand().ExecuteNonQuery();

            }
            catch (Exception)
            {
                //throw e;
            }
            finally
            {
                if (objSbInsert != null)
                {
                    objSbInsert = null;
                }
            }
        }
    }
}