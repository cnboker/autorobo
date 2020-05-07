using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AutoRobo.WebHelper
{
    public static class ShortUrl
    {
        /// <summary>
        /// Converts a Base-10 Integer to Base-64.  This will effectively shorten the number of characters used to represent a number > 10.  
        /// </summary>
        public static string Shrink(int Number)
        {
            return ConvertDecToBase(Number, 10);
        }
 
        /// <summary>
        /// Converts a Base-64 Number (represented as a string) into a Base-10 Integer.  This will effectively expand the numbers of characters needed to represent the number.
        /// </summary>
        /// <param name="NumberString"></param>
        /// <returns></returns>
        public static int Expand(string NumberString)
        {
            return ConvertBaseToDec(NumberString, 10);
        }
 
        private static string DefaultChars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        private static string Base64Chars =  "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-_";
 
        private static int ConvertBaseToDec(string NumberString, int Base)
        {
            string Chars;
            if (Base == 64)
            {
                NumberString = Regex.Replace(NumberString, "^A+", "");
                Chars = Base64Chars;
            }
            else
            {
                NumberString = Regex.Replace(NumberString, "^0+", "");
                Chars = DefaultChars;
            }
 
            int Dec = 0;
            for (int i = 0; i <= NumberString.Length - 1; i++)
            {
                int CharNum = Chars.IndexOf(NumberString[i]);
                int Conversion = CharNum * IntPow(Base, (NumberString.Length - (i + 1)));
                Dec = Dec + Conversion;
            }
 
            return Dec;
        }
 
        private static string ConvertDecToBase(int OriginalNumber, int Base)
        {
            char[] Chars;
            if (Base == 64)
            {
                Chars = Base64Chars.ToCharArray();
            }
            else
            {
                Chars = DefaultChars.ToCharArray();
            }
 
            string encoded = "";
            encoded = ConvertDecToBase(OriginalNumber, Base, encoded, Chars);
            return encoded;
        }
 
        private static string ConvertDecToBase(int Number, int Base, string EncodedString, char[] Chars)
        {
            if (Number < Base)
            {
                EncodedString = EncodedString + Chars[Number];
            }
            else
            {
                int NewNumber = Number / Base;
                EncodedString = ConvertDecToBase(NewNumber, Base, EncodedString, Chars);
 
                Number = Number - (NewNumber * Base);
                if (Number < Base)
                {
                    EncodedString = EncodedString + Chars[Number];
                }
            }
            return EncodedString;
        }
 
        private static int IntPow(int x, int pow)
        {
            int ret = 1;
            while (pow != 0)
            {
                if ((pow & 1) == 1)
                    ret *= x;
                x *= x;
                pow >>= 1;
            }
            return ret;
        }
    }
}

