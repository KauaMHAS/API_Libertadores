using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ApiLibertadoresHAS.Utils
{
    public class Criptografia
    {
        public static void CriarPasswordHah(string password, out byte[] hash, out byte[] salt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                salt = hmac.Key;
                hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        // Trocamos o 'hase' por 'HashCode' aqui no parâmetro
        public static bool VerificarPasswordHash(string password, byte[] HashCode, byte[] salt)
        { 
            using (var hmac = new System.Security.Cryptography.HMACSHA512(salt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    // Agora o seu HashCode vai funcionar perfeitamente aqui!
                    if (computedHash[i] != HashCode[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}