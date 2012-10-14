using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coffee.CardAccount.DataAccess;
using Coffee.CardAccount.Domain;
using Coffee.Shared;
using Coffee.Shared.Logging;

namespace Coffee.CardAccount.Service
{
    public class CardAccountService : ICardAccountService 
    {
        private readonly ILoggingService _loggingService;
        private readonly ICardAccountRepository _cardAccountRepository;

        public CardAccountService(ILoggingService loggingService, ICardAccountRepository cardAccountRepository)
        {
            _loggingService = loggingService;
            _cardAccountRepository = cardAccountRepository;
        }

        public ResponseResult<IEnumerable<PaymentSystem>> EnumeratePaymentSystem()
        {
            throw new NotImplementedException();
        }

        public ResponseResult<Guid> CreatePaymentSystem(string name, string logo, decimal commission, int binCode)
        {
            throw new NotImplementedException();
        }

        public ResponseResult UpdatePaymentSystem(Guid paymentSystemId, string name, string logo, decimal commission)
        {
            throw new NotImplementedException();
        }

        public ResponseResult DeletePaymentSystem(Guid paymentSystemId)
        {
            throw new NotImplementedException();
        }

        public ResponseResult<long> CreateCardAccount(Guid userId, Guid paymentSystemId, string currencyCode)
        {
            throw new NotImplementedException();
        }

        public ResponseResult BlockCardAccount(long cardAccountId)
        {
            throw new NotImplementedException();
        }

        public ResponseResult UnBlockCardAccount(long cardAccountId)
        {
            throw new NotImplementedException();
        }

        public ResponseResult<IEnumerable<FullDescriptionCardAccount>> EnumerateCardAccount()
        {
            throw new NotImplementedException();
        }

        public ResponseResult<IEnumerable<DescriptionCardAccount>> EnumerateClientCardAccount(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
