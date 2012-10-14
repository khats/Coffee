namespace Coffee.Administer.Service
{
    using Coffee.Administer.Domain;
    using Coffee.Shared;

    public interface IAdministratorService
    {
        ResponseResult<bool> CreateUser(UserInfoCreation userInfo);
    }
}