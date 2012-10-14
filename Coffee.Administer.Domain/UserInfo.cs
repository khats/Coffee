namespace Coffee.Administer.Domain
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;

    [DataContract]
    public class UserInfo
    {
        public Guid UserId { get; set; }

        [DataMember]
        [Required]
        public string Login { get; set; }

        [DataMember]
        [Required]
        public string Password { get; set; }

        [DataMember]
        [Required]
        public string Email { get; set; }

        [DataMember]
        [Required]
        public string Fio { get; set; }

        [DataMember]
        [Required]
        public string Phone { get; set; }

        [DataMember]
        [Required]
        public string Address { get; set; }

        [DataMember]
        [Required]
        public string Mobile { get; set; }

        [DataMember]
        [Required]
        public string Country { get; set; }

        [DataMember]
        [Required]
        public string Zip { get; set; }
    }
}