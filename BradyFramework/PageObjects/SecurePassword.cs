using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace BradyFramework.PageObjects
{
    public static class SecurePassword
    {
        public static class Global
        {
            public const String strPermutation = "Qazwsx@123";
            public const Int32 bytePerm1 = 0x19;
            public const Int32 bytePerm2 = 0x59;
            public const Int32 bytePerm3 = 0x17;
            public const Int32 bytePerm4 = 0x41;
        }

        public static string Encrypt(string data)
        {
            return Convert.ToBase64String(Encrpyt(Encoding.UTF8.GetBytes(data)));
        }

        public static string Decrypt(string data)
        {
            return Encoding.UTF8.GetString(Decrypt(Convert.FromBase64String(data)));
        }

        public static byte[] Encrpyt(byte[] strData)
        {
            PasswordDeriveBytes passBytes = new PasswordDeriveBytes(Global.strPermutation,
                                                                    new byte[]
                                                                    {
                                                                        Global.bytePerm1,
                                                                        Global.bytePerm2,
                                                                        Global.bytePerm3,
                                                                        Global.bytePerm4
                                                                    });
            MemoryStream memoryStream= new MemoryStream();
#pragma warning disable SYSLIB0021 // Type or member is obsolete
            Aes aes = new AesManaged();
#pragma warning restore SYSLIB0021 // Type or member is obsolete
            aes.Key = passBytes.GetBytes(aes.KeySize / 8);
            aes.IV = passBytes.GetBytes(aes.BlockSize / 8);

            CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(),
                                                         CryptoStreamMode.Write);
            cryptoStream.Write(strData, 0, strData.Length);
            cryptoStream.Close();
            return memoryStream.ToArray();
        }

        public static byte[] Decrypt(byte[] strData) 
        {
            PasswordDeriveBytes passBytes = new PasswordDeriveBytes(Global.strPermutation,
                                                                    new byte[]
                                                                    {
                                                                        Global.bytePerm1,
                                                                        Global.bytePerm2,
                                                                        Global.bytePerm3,
                                                                        Global.bytePerm4
                                                                    });
            MemoryStream memoryStream = new MemoryStream();
#pragma warning disable SYSLIB0021 // Type or member is obsolete
            Aes aes = new AesManaged();
#pragma warning restore SYSLIB0021 // Type or member is obsolete
            aes.Key = passBytes.GetBytes(aes.KeySize / 8);
            aes.IV = passBytes.GetBytes(aes.BlockSize / 8);

            CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(),
                                                         CryptoStreamMode.Write);
            cryptoStream.Write(strData, 0, strData.Length);
            cryptoStream.Close();
            return memoryStream.ToArray();
        }
    }
}
