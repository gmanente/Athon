using System;

namespace Sistema.Api.dll.Repositorio.Exceptions
{
    public class ExceptionEmail : AbstractException
    {
        public ExceptionEmail(string mensagem)
            : base(mensagem)
        {
        }

        public ExceptionEmail(string mensagem, Exception exception, int idLotacao)
            : base(mensagem, exception, idLotacao)
        {
        }

    }
}