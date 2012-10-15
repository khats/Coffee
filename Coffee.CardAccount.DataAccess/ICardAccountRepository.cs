using System;
using System.Collections.Generic;
using Coffee.CardAccount.Domain;

namespace Coffee.CardAccount.DataAccess
{
    public interface ICardAccountRepository
    {
        IEnumerable<PaymentSystem> EnumeratePaymentSystem();

        Guid CreatePaymentSystem(string name, string logo, decimal commission, int binCode);
    }
}