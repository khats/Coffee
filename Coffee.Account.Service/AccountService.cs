namespace Coffee.Account.Service
{
    using System;

    using Coffee.Account.DataAccess;
    using Coffee.Account.Domain;
    using Coffee.Administer.Domain;
    using Coffee.Shared;
    using Coffee.Shared.Helper;
    using Coffee.Shared.Logging;

    [LoggingId("AccountService")]
    public class AccountService : IAccountService
    {
        private readonly ILoggingService _loggingService;

        private readonly IAccountRepository _accountRepository;

        private readonly ISharedHelper _helper;

        private const string CheckUserLoginAndPasswordExceptionMessagge =
            "Ошибка при попытке проверить логин-пароль пользователя.";

        private const string AuthorizeErrorMessage = "Ошибка при попытке авторизации пользователя";

        private const string WronginputDataErrorMessage = "Неверные входные данные.";

        public AccountService(ILoggingService loggingService, IAccountRepository accountRepository,
            ISharedHelper helper)
        {
            _loggingService = loggingService;
            _accountRepository = accountRepository;
            _helper = helper;
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

        public ResponseResult<UserInfo> GetUser(Guid? userId)
        {
            try
            {
                return userId == null
                           ? new ResponseResult<UserInfo>("Неверные входные данные.")
                           : new ResponseResult<UserInfo>(this._accountRepository.GetUser((Guid)userId));
            }
            catch (Exception e)
            {
                const string ErrorMessage = "Не удалось получить информацию о пользователе.";
                _loggingService.Log(this, ErrorMessage, LogType.Error, e);
                return new ResponseResult<UserInfo>(ErrorMessage);
            }
        }

        public ResponseResult UpdateUserPassword(Guid userId, string newPassword, string newPasswordConfirm, string oldPassword)
        {
            try
            {
                if (newPassword != newPasswordConfirm || !_helper.ValidatePassword(newPassword))
                {
                    return new ResponseResult("Неверные входные данные.");
                }

                return new ResponseResult
                    { IsSuccess = _accountRepository.UpdateUserPassword(userId, newPassword, oldPassword) };
            }
            catch (Exception e)
            {
                const string ErrorMessage = "Не удалось обновить пароль пользователя.";
                _loggingService.Log(this, ErrorMessage, LogType.Error, e);
                return new ResponseResult(ErrorMessage);
            }
        }

        #endregion
    }
}