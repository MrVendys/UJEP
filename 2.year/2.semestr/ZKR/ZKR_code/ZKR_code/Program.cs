using System;
using System.Runtime.CompilerServices;
using System.Text;

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
            var encryptedText = BitConverter.ToString(encrypted).Replace("-","");
            System.Diagnostics.Debug.WriteLine("Encrypted: " + encryptedText);




            byte[] bytes = new byte[encryptedText.Length / 2];
            for (int i = 0; i < encryptedText.Length; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(encryptedText.Substring(i, 2), 16);
            }



            string decrypted = ch.Decrypt(encrypted);
            System.Diagnostics.Debug.WriteLine("Decrypted: " + decrypted);
        }
    }
}