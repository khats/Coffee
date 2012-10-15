namespace Coffee.Administer.Service
{
    using System;
    using System.Collections.Generic;

    using Coffee.Administer.DataAccess;
    using Coffee.Administer.Domain;
    using Coffee.Shared;
    using Coffee.Shared.Helper;
    using Coffee.Shared.Logging;

    [LoggingIdAttribute("AdministratorService")]
    public class AdministratorService : IAdministratorService
    {
        private readonly ILoggingService _loggingService;

        private readonly IAdministratorRepository _administratorRepository;

        private readonly ISharedHelper _helper;

        private const string CreateUserException = "Ошибка при создании пользователя.";

        public AdministratorService(ILoggingService loggingService, IAdministratorRepository administratorRepository,
            ISharedHelper helper)
        {
            _loggingService = loggingService;
            _administratorRepository = administratorRepository;
            _helper = helper;
        }

        #region Implementation of IAdministratorService

        public ResponseResult CreateUser(UserInfo userInfo)
        {
            try
            {
                if (userInfo == null || string.IsNullOrWhiteSpace(userInfo.Address) ||
                    string.IsNullOrWhiteSpace(userInfo.Country) || !_helper.ValidatePassword(userInfo.Password) ||
                    string.IsNullOrWhiteSpace(userInfo.Fio) || !_helper.ValidateEmail(userInfo.Email) ||
                    string.IsNullOrWhiteSpace(userInfo.Login) || !_helper.ValidatePhone(userInfo.Mobile) ||
                    !_helper.ValidatePhone(userInfo.Phone) || string.IsNullOrWhiteSpace(userInfo.Zip))
                {
                    return new ResponseResult("Неверные входные данные");
                }

                return new ResponseResult { IsSuccess = _administratorRepository.CreateUser(userInfo) };
            }
            catch (Exception exception)
            {
                _loggingService.Log(this, CreateUserException, LogType.Error, exception);
                return new ResponseResult<bool>(CreateUserException);
            }
        }

        public ResponseResult<IEnumerable<UserInfoShort>> EnumerateClients(
            string fio, string login, int pageNumber, int countPerPage)
        {

            try
            {
                return
                    new ResponseResult<IEnumerable<UserInfoShort>>(
                        _administratorRepository.EnumerateClients(fio, login, pageNumber, countPerPage));
            }
            catch (Exception e)
            {
                const string ErrorMessage = "Не удалось получить клиентов.";
                _loggingService.Log(this, ErrorMessage, LogType.Error, e);
                return new ResponseResult<IEnumerable<UserInfoShort>>(ErrorMessage);
            }
        }

        public ResponseResult<UserInfo> GetUser(Guid userId)
        {
            try
            {
                return new ResponseResult<UserInfo>(_administratorRepository.GetUser(userId));
            }
            catch (Exception e)
            {
                const string ErrorMessage = "Не удалось получить пользователя";
                _loggingService.Log(this, ErrorMessage, LogType.Error, e);
                return new ResponseResult<UserInfo>(ErrorMessage);
            }
        }

        public ResponseResult UpdateUserInfo(UserInfo userInfo)
        {
            try
            {
                if (userInfo == null || userInfo.UserId == Guid.Empty || string.IsNullOrWhiteSpace(userInfo.Address) ||
                    string.IsNullOrWhiteSpace(userInfo.Zip) || string.IsNullOrWhiteSpace(userInfo.Country) ||
                    string.IsNullOrWhiteSpace(userInfo.Fio) || string.IsNullOrWhiteSpace(userInfo.Login) ||
                    !_helper.ValidatePhone(userInfo.Mobile) || !_helper.ValidatePhone(userInfo.Phone) ||
                    !_helper.ValidateEmail(userInfo.Email))
                {
                    return new ResponseResult("Неверные входные данные.");
                }

                return new ResponseResult { IsSuccess = false };
            }
            catch (Exception e)
            {
                const string ErrorMessage = "Не удалось обновить информацию о пользователе.";
                _loggingService.Log(this, ErrorMessage, LogType.Error, e);
                return new ResponseResult(ErrorMessage);
            }
        }

        #endregion
    }
}