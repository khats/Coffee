using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Coffee.Currencies.Service;
using Coffee.Shared;

namespace Coffee.Controllers
{
    public class CurrenciesController : ApiController, ICurrenciesService
    {
        private readonly ICurrenciesService _currenciesService;

        public CurrenciesController(ICurrenciesService currenciesService)
        {
            _currenciesService = currenciesService;
        }

        [HttpGet]
        public ResponseResult<IEnumerable<string>> EnumerateDepartments()
        {
            return _currenciesService.EnumerateDepartments();
        }

        [HttpPost]
        public ResponseResult CreateCurrency(string currencyCode)
        {
            return _currenciesService.CreateCurrency(currencyCode);
        }

        [HttpPost]
        public ResponseResult DeleteCurrency(string currencyCode)
        {
            return _currenciesService.DeleteCurrency(currencyCode);
        }
    }
}
