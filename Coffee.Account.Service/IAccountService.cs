namespace Coffee.Account.Service
{
    using Coffee.Account.Domain;
    using Coffee.Shared;

    public interface IAccountService
    {
        ResponseResult<UserAccountIdentifyInfoShort> CheckUserLoginAndPassword(UserAccountIdentifyInfo identifyInfo);

        ResponseResult<bool> Authorize(UserAccountIdentifyInfoFull identifyInfoFull);
    }
}