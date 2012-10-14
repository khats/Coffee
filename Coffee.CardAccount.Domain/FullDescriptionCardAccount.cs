using System;

namespace Coffee.CardAccount.Domain
{
    public class FullDescriptionCardAccount : DescriptionCardAccount
    {
        public Guid UserId { get; set; }

        public string FIO { get; set; }
    }
}