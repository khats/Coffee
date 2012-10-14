namespace Coffee.Account.Service
{
    using System;

    using Coffee.Account.DataAccess;
    using Coffee.Account.Domain;
    using Coffee.Shared;
    using Coffee.Shared.Logging;

    public class AccountService : IAccountService
    {
        private readonly ILoggingService _loggingService;

        private readonly IAccountRepository _accountRepository;

        private const string CheckUserLoginAndPasswordExceptionMessagge =
            "Ошибка при попытке проверить логин-пароль пользователя.";

        private const string AuthorizeErrorMessage = "Ошибка при попытке авторизации пользователя";

        private const string WronginputDataErrorMessage = "Неверные входные данные.";

        public AccountService(ILoggingService loggingService, IAccountRepository accountRepository)
        {
            _loggingService = loggingService;
            _accountRepository = accountRepository;
        }

        #region Implementation of IAccountService

        public ResponseResult<UserAccountIdentifyInfoShort> CheckUserLoginAndPassword(UserAccountIdentifyInfo identifyInfo)
        {
            try
            {
                if (identifyInfo == null || string.IsNullOrEmpty(identifyInfo.Login) || 
                    string.IsNullOrEmpty(identifyInfo.Password))
                {
                    return new ResponseResult<UserAccountIdentifyInfoShort>(WronginputDataErrorMessage);
                }

                var userInfo = _accountRepository.CheckUserLoginAndPassword(identifyInfo);
                return userInfo == null
                           ? new ResponseResult<UserAccountIdentifyInfoShort>("Неверная пара логин-пароль")
                           : new ResponseResult<UserAccountIdentifyInfoShort>(userInfo);
            }
            catch (Exception exception)
            {
                _loggingService.Log(this, CheckUserLoginAndPasswordExceptionMessagge, LogType.Error, exception);
                return new ResponseResult<UserAccountIdentifyInfoShort>(CheckUserLoginAndPasswordExceptionMessagge);
            }
        }

        public ResponseResult<bool> Authorize(UserAccountIdentifyInfoFull identifyInfoFull)
        {
            try
            {
                if (identifyInfoFull == null || string.IsNullOrEmpty(identifyInfoFull.Login) || 
                    string.IsNullOrEmpty(identifyInfoFull.Password) || 
                    string.IsNullOrEmpty(identifyInfoFull.Value))
                {
                    return new ResponseResult<bool>(WronginputDataErrorMessage);
                }

                return new ResponseResult<bool>(_accountRepository.Authorize(identifyInfoFull));
            }
            catch (Exception exception)
            {
                _loggingService.Log(this, AuthorizeErrorMessage, LogType.Error, exception);
                return new ResponseResult<bool>(AuthorizeErrorMessage);
            }
        }

        #endregion
    }
}