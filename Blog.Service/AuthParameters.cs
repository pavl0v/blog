using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Blog.Service
{
    public class AuthParameters
    {
        private const string _key = "qwerty1234567890!@#$%^&*()";

        public string Audience { get; }
        public string Issuer { get; }
        public int Lifetime { get; }

        public AuthParameters()
        {
            Audience = "http://localhost/";
            Issuer = "Blog";
            Lifetime = 1;
        }

        public SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_key));
        }
    }
}
