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
            try
            {
                return new ResponseResult<IEnumerable<PaymentSystem>>(_cardAccountRepository.EnumeratePaymentSystem());
            }
            catch (Exception e)
            {
                const string errorMessage = "Ошибка при получении списка платежных систем";
                _loggingService.Log(this, errorMessage, LogType.Error, e);
                return new ResponseResult<IEnumerable<PaymentSystem>>(errorMessage);
            }
        }

        public ResponseResult<Guid> CreatePaymentSystem(string name, string logo, decimal commission, int binCode)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    return new ResponseResult<Guid>("Название системы не задано");
                }

                if (name.Length > 128)
                {
                    return new ResponseResult<Guid>("Длина названия больше 128 символов");
                }

                if (string.IsNullOrWhiteSpace(logo))
                {
                    return new ResponseResult<Guid>("Логотим платежной системы не задан");
                }

                if (logo.Length > 128)
                {
                    return new ResponseResult<Guid>("Длина имени файла логотипа 128 символов");
                }

                if (commission <= 0)
                {
                    return new ResponseResult<Guid>("Коммиссия не может быть отрицательной или равной ноль");
                }

                if (binCode < 0)
                {
                    return new ResponseResult<Guid>("BinCode меньше 0");
                }

                return new ResponseResult<Guid>(_cardAccountRepository.CreatePaymentSystem(name, logo, commission, binCode));
            }
            catch (Exception e)
            {
                const string errorMessage = "Ошибка при создании платежной системы";
                _loggingService.Log(this, errorMessage, LogType.Error, e);
                return new ResponseResult<Guid>(errorMessage);
            }
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
