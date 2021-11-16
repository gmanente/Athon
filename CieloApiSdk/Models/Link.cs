using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cielo
{
    /// <summary>
    /// Descrição: Classe VO Link
    /// Autor: Evander Costa
    /// Data: 07/03/2017
    /// Alteração: 09/03/2017
    /// </summary>
    public class Link
    {
        public string Method { get; set; }
        public string Rel { get; set; }
        public string Href { get; set; }
    }
}
