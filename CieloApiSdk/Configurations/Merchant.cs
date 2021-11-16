using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cielo
{
    /// <summary>
    /// Descrição: Classe de configuração do Merchant
    /// Autor: Evander Costa
    /// Data: 07/03/2017
    /// Alteração: 09/03/2017
    /// </summary>
    public class Merchant : IMerchant
    {
        public static readonly Merchant Production = new Merchant(Guid.Parse("afe44aaf-969b-4a74-a2cb-81cee0d179f3"), "rIGa7HhabPdeUIUV6YF5zp2WdfdTmsQs28papNvh");

        // MerchandId e MerchantKey
        public static readonly Merchant Sandbox = new Merchant(Guid.Parse("ffed4f23-ca09-4b67-a6b4-9f89cff9b455"), "TZFJWUXXRXCIRFMUSCMEPXQWZPIEIHYQZOBRIDTE");


        public Merchant(Guid id, string key)
        {
            this.Id = id;
            this.Key = key;
        }


        public Guid Id { get; }

        public string Key { get; }
    }
}
