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
    /// Descrição: Classe VO Cliente
    /// Autor: Evander Costa
    /// Data: 07/03/2017
    /// Alteração: 09/03/2017
    /// </summary>
    public class Customer
    {
        public Customer()
        {
        }


        public Customer(string name)
        {
            this.Name = name;
        }


        public string Name { get; set; }
        public string Identity { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public IdentityType? IdentityType { get; set; }

        public string Email { get; set; }
        public DateTime? Birthdate { get; set; }
        public Address Address { get; set; }
        public Address DeliveryAddress { get; set; }
    }
}
