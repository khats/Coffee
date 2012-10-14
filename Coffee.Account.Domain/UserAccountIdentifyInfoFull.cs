namespace Coffee.Account.Domain
{
    using System.Runtime.Serialization;

    [DataContract]
    public class UserAccountIdentifyInfoFull : UserAccountIdentifyInfoShort
    {
        [DataMember]
        public string Value { get; set; }
    }
}