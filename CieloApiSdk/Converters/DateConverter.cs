using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cielo.Converters
{
    /// <summary>
    /// Descrição: Classe de conversão de data para o formato ISO
    /// Autor: Evander Costa
    /// Data: 07/03/2017
    /// Alteração: 09/03/2017
    /// </summary>
    internal class DateConverter : IsoDateTimeConverter
    {
        public DateConverter()
        {
            this.DateTimeFormat = "yyyy-MM-dd";
        }
    }
}
