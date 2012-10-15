using System;
using System.Collections.Generic;
using System.Web.Http;
using Coffee.CardAccount.Domain;
using Coffee.CardAccount.Service;
using Coffee.Shared;

namespace Coffee.Controllers
{
    public class CardAccountController : ApiController, ICardAccountService
    {
        private readonly ICardAccountService _cardAccountService;

        public CardAccountController(ICardAccountService cardAccountService)
        {
            _cardAccountService = cardAccountService;
        }

        public ResponseResult<IEnumerable<PaymentSystem>> EnumeratePaymentSystem()
        {
            return _cardAccountService.EnumeratePaymentSystem();
        }

        public ResponseResult<Guid> CreatePaymentSystem(string name, string logo, decimal commission, int binCode)
        {
            return _cardAccountService.CreatePaymentSystem(name, logo, commission, binCode);
        }

        public ResponseResult UpdatePaymentSystem(Guid paymentSystemId, string name, string logo, decimal commission)
        {
            return _cardAccountService.UpdatePaymentSystem(paymentSystemId, name, logo, commission);
        }

        public ResponseResult DeletePaymentSystem(Guid paymentSystemId)
        {
            return _cardAccountService.DeletePaymentSystem(paymentSystemId);
        }

        public ResponseResult<long> CreateCardAccount(Guid userId, Guid paymentSystemId, string currencyCode)
        {
            return _cardAccountService.CreateCardAccount(userId, paymentSystemId, currencyCode);
        }

        public ResponseResult BlockCardAccount(long cardAccountId)
        {
            return _cardAccountService.BlockCardAccount(cardAccountId);
        }

        public ResponseResult UnBlockCardAccount(long cardAccountId)
        {
            return _cardAccountService.UnBlockCardAccount(cardAccountId);
        }

        public ResponseResult<IEnumerable<FullDescriptionCardAccount>> EnumerateCardAccount()
        {
            return _cardAccountService.EnumerateCardAccount();
        }

        public ResponseResult<IEnumerable<DescriptionCardAccount>> EnumerateClientCardAccount(Guid userId)
        {
            return _cardAccountService.EnumerateClientCardAccount(userId);
        }
    }
}