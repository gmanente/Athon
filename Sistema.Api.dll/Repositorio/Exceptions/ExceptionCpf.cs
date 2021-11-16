using System;

namespace Sistema.Api.dll.Repositorio.Exceptions
{
    public class ExceptionCpf : AbstractException
    {
        public ExceptionCpf(String mensagem)
            : base(mensagem)
        {
        }
    }
}