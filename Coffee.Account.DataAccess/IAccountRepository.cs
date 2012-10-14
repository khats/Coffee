namespace Coffee.Account.DataAccess
{
    using Coffee.Account.Domain;

    public interface IAccountRepository
    {
        UserAccountIdentifyInfoShort CheckUserLoginAndPassword(UserAccountIdentifyInfo identifyInfo);

        bool Authorize(UserAccountIdentifyInfoFull identifyInfoFull);
    }
}