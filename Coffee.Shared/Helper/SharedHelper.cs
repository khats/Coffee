namespace Coffee.Shared.Helper
{
    using System;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.RegularExpressions;

    public class SharedHelper : ISharedHelper
    {
        #region Implementation of ISharedHelper

        public bool CompareStringWithHash(string data, string hashedString)
        {
            using (var sha1 = new SHA1CryptoServiceProvider())
            {
                var hash = Convert.ToBase64String(sha1.ComputeHash(Encoding.Unicode.GetBytes(data)));
                return hash == hashedString;
            }
        }

        public string ComputeHashFromString(string data)
        {
            using (var sha1 = new SHA1CryptoServiceProvider())
            {
                return Convert.ToBase64String(sha1.ComputeHash(Encoding.Unicode.GetBytes(data)));
            }
        }

        public string GenerateSalt()
        {
            var buf = new byte[16];
            using (var prov = new RNGCryptoServiceProvider())
            {
                prov.GetBytes(buf);
                return Convert.ToBase64String(buf);
            }
        }

        public string EncodePassword(string pass, string salt)
        {
            var bytes = Encoding.Unicode.GetBytes(pass);
            var src = Convert.FromBase64String(salt);
            var dst = new byte[src.Length + bytes.Length];
            Buffer.BlockCopy(src, 0, dst, 0, src.Length);
            Buffer.BlockCopy(bytes, 0, dst, src.Length, bytes.Length);
            using (var algorithm = HashAlgorithm.Create("SHA1"))
            {
                var inArray = algorithm.ComputeHash(dst);
                return Convert.ToBase64String(inArray);
            }
        }

        public bool ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            var regex = new Regex(
                @"^(([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+([;.](([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+)*$");

            return regex.IsMatch(email);

        }

        public bool ValidatePhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
            {
                return false;
            }

            var regex = new Regex(@"^(\+|[0-9]|\(|\)|\s|\-)*$");
            var count = phone.Where(char.IsDigit).Count();
            return regex.IsMatch(phone) && count >= 5;
        }


        public bool ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
            {
                return false;
            }

            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789".ToCharArray();
            var numbers = "0123456789".ToCharArray();
            return password.Any(numbers.Contains) && !password.Any(l => !chars.Contains(l));
        }


        #endregion
    }
}