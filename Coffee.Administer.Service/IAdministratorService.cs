namespace Coffee.Administer.Service
{
    using System;
    using System.Collections.Generic;

    using Coffee.Administer.Domain;
    using Coffee.Shared;

    public interface IAdministratorService
    {
        ResponseResult CreateUser(UserInfo userInfo);

        ResponseResult<IEnumerable<UserInfoShort>> EnumerateClients(
            string fio, string login, int pageNumber, int countPerPage);

        ResponseResult<UserInfo> GetUser(Guid userId);

        ResponseResult UpdateUserInfo(UserInfo userInfo);
    }
}