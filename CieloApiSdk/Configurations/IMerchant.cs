using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cielo
{
    /// <summary>
    /// Descrição: Interface da configuração do Merchant
    /// Autor: Evander Costa
    /// Data: 07/03/2017
    /// Alteração: 09/03/2017
    /// </summary>
    public interface IMerchant
    {
        Guid Id { get; }
        string Key { get; }
    }
}
