namespace Coffee.Administer.Domain
{
    using System;

    public class UserInfoShort
    {
        public string Login { get; set; }

        public string FIO { get; set; }

        public Guid UserId { get; set; }

        public DateTime RegistrationDate { get; set; }

        public int CounOfCardAccounts { get; set; }

    }
}