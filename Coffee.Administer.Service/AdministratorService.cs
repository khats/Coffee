namespace Coffee.Administer.Service
{
    using System;

    using Coffee.Administer.DataAccess;
    using Coffee.Administer.Domain;
    using Coffee.Shared;
    using Coffee.Shared.Helper;
    using Coffee.Shared.Logging;

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

        public ResponseResult<bool> CreateUser(UserInfoCreation userInfo)
        {
            try
            {
                if (userInfo == null || string.IsNullOrWhiteSpace(userInfo.Address) ||
                    string.IsNullOrWhiteSpace(userInfo.Country) || !_helper.ValidatePassword(userInfo.Password) ||
                    string.IsNullOrWhiteSpace(userInfo.Fio) || !_helper.ValidateEmail(userInfo.Email) ||
                    string.IsNullOrWhiteSpace(userInfo.Login) || !_helper.ValidatePhone(userInfo.Mobile) ||
                    !_helper.ValidatePhone(userInfo.Phone) || string.IsNullOrWhiteSpace(userInfo.Zip))
                {
                    return new ResponseResult<bool>("Неверные входные данные");
                }

                return new ResponseResult<bool>(_administratorRepository.CreateUser(userInfo));
            }
            catch (Exception exception)
            {
                _loggingService.Log(this, CreateUserException, LogType.Error, exception);
                return new ResponseResult<bool>(CreateUserException);
            }
        }

        #endregion
    }
}