namespace Coffee.Account.DataAccess
{
    using System;

    using Coffee.Account.Domain;
    using Coffee.Administer.Domain;

    public interface IAccountRepository
    {
        UserAccountIdentifyInfoShort CheckUserLoginAndPassword(UserAccountIdentifyInfo identifyInfo);

        bool Authorize(UserAccountIdentifyInfoFull identifyInfoFull);

        UserInfo GetUser(Guid userId);

        bool UpdateUserPassword(Guid userId, string newPassword, string oldPassword);
    }
}