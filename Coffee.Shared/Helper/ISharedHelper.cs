namespace Coffee.Shared.Helper
{
    public interface ISharedHelper
    {
        bool CompareStringWithHash(string data, string hashedString);

        string ComputeHashFromString(string data);

        string GenerateSalt();

        string EncodePassword(string pass, string salt);

        bool ValidateEmail(string email);

        bool ValidatePassword(string password);

        bool ValidatePhone(string phone);
    }
}