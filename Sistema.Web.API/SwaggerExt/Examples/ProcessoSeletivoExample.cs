using System;
using Sistema.Web.API.SwaggerExt.Models;
using Swashbuckle.Examples;

namespace Sistema.Web.API.SwaggerExt.Examples
{
    internal class ProcessoSeletivoExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new ProcessoSeletivo
            {
                IdCampus = 1,
                IdProcessoSeletivo = 2,
                ProcessoSeletivoDescricao = "40° Processo Seletivo Vagas Remanescentes 2020/2 On-line",
                DataProva = Convert.ToDateTime("04/10/2020 08:00:00")
            };
        }
    }
}