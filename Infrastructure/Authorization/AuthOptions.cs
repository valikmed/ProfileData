using System;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Infrastructure.Authorization
{
    public class AuthOptions
    {
        public const string ISSUER = "MyAuthServer";                                        // Автор токену (Творець)
        public const string AUDIENCE = "MyAuthClient";                                      // Назва того, хто ним користується
        private const string KEY = "ThisIsASecureKeyThatIsAtLeast128BitsLong!";             // Секретний ключ.
        public const int LIFETIME = 60;                                                     // Термін придатності (в хвилинах) з моменту створення

        // Функція, яка повертає симетричний ключ на основі секретного ключа `KEY`
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}

