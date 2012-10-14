namespace Coffee.Account.Domain
{
    using System.Runtime.Serialization;

    [DataContract]
    public class UserAccountIdentifyInfoShort : UserAccountIdentifyInfo
    {
        [DataMember]
        public int Key { get; set; }
    }
}