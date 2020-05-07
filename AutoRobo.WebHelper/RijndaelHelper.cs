using System;
using System.Collections.Generic;

using System.Text;

namespace Util
{
    public class RijndaelHelper
    {
        static string passPhrase = "coobots@gmail.com2012";        // can be any string
        static string saltValue = "szsong100";        // can be any string
        static string hashAlgorithm = "SHA1";             // can be "MD5"
        static int passwordIterations = 2;                  // can be any number
        static string initVector = "@1a2cG3D4eUF6g7B$"; // must be 16 bytes
        static int keySize = 256;                // can be 192 or 128

        static public string Encrypt(string plainText) {
            string cipherText = Rijndael.Encrypt(plainText,
                                                           passPhrase,
                                                           saltValue,
                                                           hashAlgorithm,
                                                           passwordIterations,
                                                           initVector,
                                                           keySize);
            return cipherText;
        }
        static public string Decrypt(string cipherText) {
            string plainText = Rijndael.Decrypt(cipherText,
                                                         passPhrase,
                                                         saltValue,
                                                         hashAlgorithm,
                                                         passwordIterations,
                                                         initVector,
                                                         keySize);
            return plainText;
        }
      
    }
}
