namespace Coffee.Controllers
{
    using System.Web.Http;

    using Coffee.Account.Domain;
    using Coffee.Account.Service;
    using Coffee.Shared;

    public class AccountController : ApiController, IAccountService
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        #region Implementation of IAccountService

        public ResponseResult<UserAccountIdentifyInfoShort> CheckUserLoginAndPassword(UserAccountIdentifyInfo identifyInfo)
        {
            return _accountService.CheckUserLoginAndPassword(identifyInfo);
        }

        #endregion

        #region Implementation of IAccountService

        public ResponseResult<bool> Authorize(UserAccountIdentifyInfoFull identifyInfoFull)
        {
            return _accountService.Authorize(identifyInfoFull);
        }

        #endregion
    }
}