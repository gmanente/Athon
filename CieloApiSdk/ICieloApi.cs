﻿using Cielo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cielo
{
    public interface ICieloApi
    {
        /// <summary>
        /// Descrição: Interface da Api da Cielo
        /// Autor: Evander Costa
        /// Data: 07/03/2017
        /// Alteração: 09/03/2017
        /// </summary>
        Transaction CreateTransaction(Guid requestId, Transaction transaction);

        Transaction GetTransaction(Guid requestId, Guid paymentId);

        ReturnStatus CancellationTransaction(Guid requestId, Guid paymentId, decimal? amount = null);

        ReturnStatus CaptureTransaction(Guid requestId, Guid paymentId, decimal? amount = null, decimal? serviceTaxAmount = null);
    }
}
