using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Cielo.Converters
{
    /// <summary>
    /// Descrição: Classe de conversão de data de expiração do cartão
    /// Autor: Evander Costa
    /// Data: 07/03/2017
    /// Alteração: 09/03/2017
    /// </summary>
    internal class CreditCardExpirationDateConverter : IsoDateTimeConverter
    {
        public CreditCardExpirationDateConverter()
        {
            base.DateTimeFormat = "MM/yyyy";
        }
    }
}
