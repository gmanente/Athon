using System;

namespace Sistema.Api.dll.Repositorio.Exceptions
{
    public class SysException : AbstractException
    {
        public SysException()
            : base()
        {
        }

        public SysException(string mensagem)
            : base(mensagem)
        {
        }

        public SysException(string mensagem, Exception exception, int idLotacao)
            : base(mensagem, exception, idLotacao)
        {
        }
    }
}