﻿using Cielo.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Cielo
{
    /// <summary>
    /// Descrição: Classe VO Erro
    /// Autor: Evander Costa
    /// Data: 07/03/2017
    /// Alteração: 09/03/2017
    /// </summary>
    public class Error
    {
        public int Code { get; set; }

        public string Message { get; set; }
    }
}
