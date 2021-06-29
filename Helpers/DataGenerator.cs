using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confused.Helpers
{
    public class DataGenerator
    {
        public static string GenerateRandomString(int length)
        {
            char[] word = new char[length];
            char[] chars = "qwertyuiopasdfghjklzxcvbnm".ToCharArray();
            var random = new Random();

            for (int i = 0; i < length; i++)
            {
                word[i] = chars[random.Next(chars.Length)];
            }

            return new string(word);
        }
    }
}
