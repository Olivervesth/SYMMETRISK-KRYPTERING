using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Symmetriskcrypt
{
    class Controller
    {
        public byte[] GenerateRandom(int length)//generates random byte array
        {
            using (var randomNumb = new RNGCryptoServiceProvider())
            {
                var random = new byte[length];
                randomNumb.GetBytes(random);
                return random;
            }
        }
    }
}
