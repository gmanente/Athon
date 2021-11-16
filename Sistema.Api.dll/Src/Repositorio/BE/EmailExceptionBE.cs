using Sistema.Api.dll.Src.Repositorio.DAO;
using System;

namespace Sistema.Api.dll.Src.Repositorio.BE
{
    public class EmailExceptionBE : AbstractBE
    {
        public void GravarErroEmail(string emailRemetente, string emailDestino, string messageException)
        {
            EmailExceptionDAO emailExceptionDao = null;
            try
            {
                emailExceptionDao = new EmailExceptionDAO(GetSqlCommand());
                emailExceptionDao.GravarErroEmail(emailRemetente, emailDestino, messageException);

            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}