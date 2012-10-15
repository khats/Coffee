namespace Coffee.Administer.DataAccess
{
    using System;
    using System.Collections.Generic;

    using Coffee.Administer.Domain;

    public interface IAdministratorRepository
    {
        bool CreateUser(UserInfo userInfo);

        IEnumerable<UserInfoShort> EnumerateClients(string fio, string login, int pageNumber, int countPerPage);

        UserInfo GetUser(Guid userId);

        bool UpdateUserInfo(UserInfo userInfo);
    }
}