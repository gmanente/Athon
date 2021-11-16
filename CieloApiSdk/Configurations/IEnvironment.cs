using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cielo
{
    /// <summary>
    /// Descrição: Interface da configuração dos Ambientes da Api da Cielo
    /// Autor: Evander Costa
    /// Data: 07/03/2017
    /// Alteração: 09/03/2017
    /// </summary>
    public interface IEnvironment
    {
        string TransactionUrl { get; }
        string QueryUrl { get; }
    }
}
