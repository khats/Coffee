namespace Coffee.Administer.DataAccess
{
    using Coffee.Administer.Domain;

    public interface IAdministratorRepository
    {
        bool CreateUser(UserInfoCreation userInfo);
    }
}