using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cielo
{
    /// <summary>
    /// Descrição: Classe VO Retorno da Situação
    /// Autor: Evander Costa
    /// Data: 07/03/2017
    /// Alteração: 09/03/2017
    /// </summary>
    public class ReturnStatus
    {
        public string ReturnCode { get; set; }
        public string ReturnMessage { get; set; }
        public string ReasonCode { get; set; }
        public string ReasonMessage { get; set; }
        public string ProviderReturnCode { get; set; }
        public string ProviderReturnMessage { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Status? Status { get; set; }

        public List<Link> Links { get; set; }
    }
}
