namespace Coffee.Controllers
{
    using System;
    using System.Web.Http;
    using System.Web.Security;

    using Coffee.Account.Domain;
    using Coffee.Account.Service;
    using Coffee.Administer.Domain;
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
            FormsAuthentication.SetAuthCookie(identifyInfoFull.Login, false);
            return new ResponseResult<bool>(true);
            var bl = _accountService.Authorize(identifyInfoFull);
            if (bl.Data)
            {
                FormsAuthentication.SetAuthCookie(identifyInfoFull.Login, false);
            }

            return bl;
        }

        public ResponseResult<UserInfo> GetUser(Guid? userId)
        {
            return _accountService.GetUser((Guid)(Membership.GetUser(User.Identity.Name)).ProviderUserKey);
        }

        public ResponseResult UpdateUserPassword(Guid userId, string newPassword, string newPasswordConfirm, string oldPassword)
        {
            return _accountService.UpdateUserPassword(userId, newPassword, newPasswordConfirm, oldPassword);
        }

        #endregion
    }
}