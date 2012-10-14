using System;
using System.Collections.Generic;
using Coffee.Shared;

namespace Coffee.Currencies.Service
{
    public interface ICurrenciesService
    {
        ResponseResult<IEnumerable<string>> EnumerateDepartments();

        ResponseResult CreateCurrency(string currencyCode);
        
        ResponseResult DeleteCurrency(string currencyCode);
    }
}