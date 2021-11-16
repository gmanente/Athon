using System;
using System.Diagnostics;

namespace Sistema.Api.dll.Repositorio.Exceptions
{
    public class ModelException
    {
        public int IdArquivo { get; set; }
        public string Arquivo { get; set; }
        public string Metodo { get; set; }
        public string Linha { get; set; }
        public string Mensagem { get; set; }
        public string Caminho { get; set; }


        public ModelException(Exception error)
        {
            var trace = new StackTrace(error, true);

            this.IdArquivo = trace.GetFrame(trace.FrameCount - 1).GetFileName().LastIndexOf('\\') + 1;
            this.Arquivo = trace.GetFrame(trace.FrameCount - 1).GetFileName().Substring(this.IdArquivo).ToString();
            this.Metodo = trace.GetFrame(trace.FrameCount - 1).GetMethod().Name;
            this.Linha = trace.GetFrame(trace.FrameCount - 1).GetFileLineNumber().ToString();
            this.Caminho = trace.GetFrame(trace.FrameCount - 1).GetFileName();
            this.Mensagem = error.Message;

        }
    }
}