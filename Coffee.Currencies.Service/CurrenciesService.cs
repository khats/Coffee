using System;
using System.Collections.Generic;
using Coffee.Currencies.DataAccess;
using Coffee.Shared;
using Coffee.Shared.Logging;

namespace Coffee.Currencies.Service
{
    public class CurrenciesService : ICurrenciesService 
    {
        private readonly ILoggingService _loggingService;

        private readonly ICurrencyRepository _currencyRepository;

        public CurrenciesService(ILoggingService loggingService, ICurrencyRepository currencyRepository)
        {
            _loggingService = loggingService;
            _currencyRepository = currencyRepository;
        }

        public ResponseResult<IEnumerable<string>> EnumerateDepartments()
        {
            try
            {
                return new ResponseResult<IEnumerable<string>>(_currencyRepository.EnumerateCurrency());
            }
            catch (Exception e)
            {
                const string errorMessage = "Ошибка при получении списка валют";
                _loggingService.Log(this, errorMessage, LogType.Error, e);
                return new ResponseResult<IEnumerable<string>>(errorMessage);
            }
        }

        public ResponseResult CreateCurrency(string currencyCode)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(currencyCode))
                {
                    return new ResponseResult("Название валюты не задано");
                }

                if (currencyCode.Length != 3)
                {
                    return new ResponseResult("Валюта задана не в международном формате");
                }

                if (_currencyRepository.CreateCurrency(currencyCode))
                {
                    return new ResponseResult
                               {
                                   IsSuccess = true
                               };
                }
                
                return new ResponseResult("Данная валюта уже существует");
            }
            catch (Exception e)
            {
                const string errorMessage = "Ошибка при создании валюты";
                _loggingService.Log(this, errorMessage, LogType.Error, e);
                return new ResponseResult<IEnumerable<string>>(errorMessage);
            }
        }

        public ResponseResult DeleteCurrency(string currencyCode)
        {
            try
            {
                var result = _currencyRepository.DeleteCurrency(currencyCode);
                if (result == null)
                {
                    return new ResponseResult
                               {
                                   IsSuccess = true
                               };
                }

                return new ResponseResult(result);
            }
            catch (Exception e)
            {
                const string errorMessage = "Ошибка при удалении валюты";
                _loggingService.Log(this, errorMessage, LogType.Error, e);
                return new ResponseResult<IEnumerable<string>>(errorMessage);
            }
        }
    }
}
