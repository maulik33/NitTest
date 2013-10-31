using System;
using System.Text;
using BlowfishNET;

namespace NursingLibrary.Utilities
{
    /// <summary>
    /// Encrypts and Decrypts using the Blowfish algorithm
    /// </summary>
    public class BlowFishCipherHelper
    {
        private const string DefaultKey = "87325400F186E0823740689AFEE01A9C";

        private static readonly byte[] HttpHashKey = HexStringToByteArray(DefaultKey);
        private static readonly BlowfishCFB RtmpHashGen = new BlowfishCFB(HttpHashKey, 0, HttpHashKey.Length);
    
        /// <summary>
        /// Encrypts the string passed
        /// </summary>
        /// <param name="param">String to ne encrypted</param>
        /// <returns>Hex string of the encrypted string</returns>
        public static string Encrypt(string param)
        {
            //Set the Key
            RtmpHashGen.SetIV(new byte[16], 0);
            
            var dataArr = Encoding.UTF8.GetBytes(param);
            var outputArr = new byte[dataArr.Length];

            //Encrypt the data array
            RtmpHashGen.Encrypt(dataArr, 0, outputArr, 0, dataArr.Length);

            //Converts the decrypted data to string
            return Convert.ToBase64String(outputArr);
        }

        /// <summary>
        /// Decrypt the Hex string
        /// </summary>
        /// <param name="encryptedData">Hex sring</param>
        /// <returns>String</returns>
        public static string Decrypt(string encryptedData)
        {
            RtmpHashGen.Initialize(HttpHashKey, 0, HttpHashKey.Length);
            RtmpHashGen.SetIV(new byte[16], 0);

            var inputArr = Convert.FromBase64String(encryptedData);
            var outputArr = new byte[inputArr.Length];

            //Decrypt
            RtmpHashGen.Decrypt(inputArr, 0, outputArr, 0, inputArr.Length);

            //Converts the decrypted data to sring
            return Encoding.UTF8.GetString(outputArr);
        }

        /// <summary>
        /// Converts byte array data to Hex string
        /// </summary>
        /// <param name="ba">byte array data to convert</param>
        /// <returns></returns>
        public static string ByteArrayToHexString(byte[] ba)
        {
            var hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        /// <summary>
        /// Converts Hex string to Byte array
        /// </summary>
        /// <param name="hex">hex string value</param>
        /// <returns></returns>
        public static byte[] HexStringToByteArray(String hex)
        {
            int numberChars = hex.Length;
            var bytes = new byte[numberChars / 2];
            for (int i = 0; i < numberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }
    }
}
