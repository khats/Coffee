namespace Coffee.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Web.Http;

    using Coffee.Administer.Domain;
    using Coffee.Administer.Service;
    using Coffee.Shared;

    public class AdministratorController : ApiController, IAdministratorService
    {
        private readonly IAdministratorService _administratorService;

        public AdministratorController(IAdministratorService administratorService)
        {
            _administratorService = administratorService;
        }

        #region Implementation of IAdministratorService

        public ResponseResult CreateUser(UserInfo userInfo)
        {
            return this._administratorService.CreateUser(userInfo);
        }

        public ResponseResult<IEnumerable<UserInfoShort>> EnumerateClients(string fio, string login, int pageNumber, int countPerPage)
        {
            return _administratorService.EnumerateClients(fio, login, pageNumber, countPerPage);
        }

        public ResponseResult<UserInfo> GetUser(Guid userId)
        {
            return _administratorService.GetUser(userId);
        }

        public ResponseResult UpdateUserInfo(UserInfo userInfo)
        {
            return _administratorService.UpdateUserInfo(userInfo);
        }

        #endregion
    }
}