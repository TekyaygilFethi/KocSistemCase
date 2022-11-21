using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace KUSYS.Helper.WebHelpers
{
    public static class CryptographyHelper
    {
        static SHA1 sha = new SHA1CryptoServiceProvider();
        static readonly IConfiguration _configuration;

        static CryptographyHelper()
        {
            _configuration = new ConfigurationBuilder().
                SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(@"appsettings.json", false, false)
                .AddEnvironmentVariables()
                .Build();
        }

        public static string Encode(string str)
        {
            var salt = _configuration.GetSection("Salt").Value;

            return Convert.ToBase64String(sha.ComputeHash(Encoding.UTF8.GetBytes(str + salt)));
        }
    }
}
