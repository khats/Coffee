namespace Coffee.Controllers
{
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

        public ResponseResult<bool> CreateUser(UserInfoCreation userInfo)
        {
            return this._administratorService.CreateUser(userInfo);
        }

        #endregion
    }
}