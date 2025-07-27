using System.Security.Cryptography;
using Shared.Domain.Services.Hash;

namespace Shared.Infrastructure.Services.Hash;

public class WordHasherService: IWordHasherService
{
    private const int SaltSize = 16;
    
    private const int HashSize = 32;
    private const int HashIterations = 100000;
    
    private readonly HashAlgorithmName _algorithm= HashAlgorithmName.SHA512;
    public string Hash(string text)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
        byte[] hash=Rfc2898DeriveBytes.Pbkdf2(text, salt, HashIterations,_algorithm,HashSize);
        return $"{Convert.ToHexString(hash)}-{Convert.ToHexString(salt)}";
    }

    
    
    public bool IsValid(string text, string hashText)
    {
        string[] parts = hashText.Split('-');
        byte[] hash = Convert.FromHexString(parts[0]);
        byte[] salt = Convert.FromHexString(parts[1]);
        byte[] inputHash=Rfc2898DeriveBytes.Pbkdf2(text, salt, HashIterations,_algorithm,HashSize);
        return CryptographicOperations.FixedTimeEquals(hash, inputHash);
    }
}