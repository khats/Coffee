namespace Coffee.Account.Service
{
    using System;

    using Coffee.Account.Domain;
    using Coffee.Administer.Domain;
    using Coffee.Shared;

    public interface IAccountService
    {
        ResponseResult<UserAccountIdentifyInfoShort> CheckUserLoginAndPassword(UserAccountIdentifyInfo identifyInfo);

        ResponseResult<bool> Authorize(UserAccountIdentifyInfoFull identifyInfoFull);

        ResponseResult<UserInfo> GetUser(Guid? userId);

        ResponseResult UpdateUserPassword(
            Guid userId, string newPassword, string newPasswordConfirm, string oldPassword);
    }
}