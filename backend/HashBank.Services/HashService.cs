using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace HashBank.Services
{
    public interface IHashService
    {
        string HashPassword(string password);
    }
    public class HashService : IHashService
    {
        public string HashPassword(string password)
        {
            byte[] saltBytes = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }

            byte[] hashed = KeyDerivation.Pbkdf2(
                password: password,
                salt: saltBytes,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 32);
            return Convert.ToBase64String(hashed);
        }
    }
}
