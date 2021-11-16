using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cielo
{
    /// <summary>
    /// Descrição: Classe VO Transação
    /// Autor: Evander Costa
    /// Data: 07/03/2017
    /// Alteração: 09/03/2017
    /// </summary>
    public class Transaction
    {
        public Transaction()
        {
        }


        public Transaction(string merchantOrderId, Customer customer, Payment payment)
        {
            this.MerchantOrderId = merchantOrderId;
            this.Customer = customer;
            this.Payment = payment;
        }


        public string MerchantOrderId { get; set; }
        public Customer Customer { get; set; }
        public Payment Payment { get; set; }
    }
}
