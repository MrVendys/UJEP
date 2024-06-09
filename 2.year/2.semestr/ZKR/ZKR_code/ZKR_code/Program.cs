using System;
using System.Runtime.CompilerServices;

namespace ZKR_code // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Code c = new Code();
            //c.Do();
            DoChat();
        }
        public static void DoChat()
        {
            chat_code ch = new chat_code();
            ch.Blowfish("mysecrett");

            string plaintext = "hiworlddddddddddddddd";
            System.Diagnostics.Debug.WriteLine("Plaintext: " + plaintext);

            byte[] encrypted = ch.Encrypt(plaintext);
            System.Diagnostics.Debug.WriteLine("Encrypted: " + BitConverter.ToString(encrypted).Replace("-", " "));

            string decrypted = ch.Decrypt(encrypted);
            System.Diagnostics.Debug.WriteLine("Decrypted: " + decrypted);
        }
    }
}