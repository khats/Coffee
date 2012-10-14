using System;
using System.Collections.Generic;
using Coffee.CardAccount.DataAccess;
using Coffee.CardAccount.Domain;
using Coffee.Shared;

namespace Coffee.CardAccount.Service
{
    public interface ICardAccountService
    {
        ResponseResult<IEnumerable<PaymentSystem>> EnumeratePaymentSystem();

        ResponseResult<Guid> CreatePaymentSystem(string name, string logo, decimal commission, int binCode);

        ResponseResult UpdatePaymentSystem(Guid paymentSystemId, string name, string logo, decimal commission);

        ResponseResult DeletePaymentSystem(Guid paymentSystemId);

        ResponseResult<long> CreateCardAccount(Guid userId, Guid paymentSystemId, string currencyCode);

        ResponseResult BlockCardAccount(long cardAccountId);

        ResponseResult UnBlockCardAccount(long cardAccountId);  
            
        ResponseResult<IEnumerable<FullDescriptionCardAccount>> EnumerateCardAccount();

        ResponseResult<IEnumerable<DescriptionCardAccount>> EnumerateClientCardAccount(Guid userId);
    }
}