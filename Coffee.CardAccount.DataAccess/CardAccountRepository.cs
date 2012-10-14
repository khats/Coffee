namespace Coffee.CardAccount.DataAccess
{
    using System;
    using System.Collections.Generic;

    using Coffee.CardAccount.Domain;
    using Coffee.Shared.Configuration;

    public class CardAccountRepository : ICardAccountRepository
    {
        private readonly IConfigurationService _configurationService;

        public CardAccountRepository(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
        }

        #region Implementation of ICardAccountRepository

        public IEnumerable<PaymentSystem> EnumeratePaymentSystem()
        {
            throw new NotImplementedException();
        }

        public Guid CreatePaymentSystem(string name, string logo, decimal commission, int binCode)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}