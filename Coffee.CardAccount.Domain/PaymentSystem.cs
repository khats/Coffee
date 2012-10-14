using System;

namespace Coffee.CardAccount.Domain
{
    public class PaymentSystem
    {
        public Guid PaymentId { get; set; }

        public string Name { get; set; }

        public string Logo { get; set; }

        public decimal Commission { get; set; }

        public int BinCode { get; set; }
    }
}
