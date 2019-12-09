namespace MagicShortener.Common.Security
{
    public interface IPasswordHasher
    {
        (string hash, string password) Hash(string password);

        bool Check(byte[] hash, byte[] salt, string password);
    }
}
