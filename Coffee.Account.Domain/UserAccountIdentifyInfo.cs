namespace Coffee.Account.Domain
{
    using System.Runtime.Serialization;

    [DataContract]
    public class UserAccountIdentifyInfo
    {
        [DataMember]
        public string Login { get; set; }

        [DataMember]
        public string Password { get; set; }
    }
}