using System.Collections.Generic;

namespace Coffee.Currencies.DataAccess
{
    public interface ICurrencyRepository
    {
        IEnumerable<string> EnumerateCurrency();

        bool CreateCurrency(string currencyCode);

        string DeleteCurrency(string currencyCode);
    }
}