using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BLL;

namespace WebApiProject.Test
{
    [TestClass]
    public class BLLTest
    {
        [TestMethod]
        public void EncriptionWorks()
        {
            var plainText = "M1C0ntr4sen4";
            var encription = new BLL.Encription();
            var encryptedText = encription.Encrypt(plainText);
            var decryptedText = encription.Decrypt(encryptedText);

            Assert.AreEqual(plainText, decryptedText);
        }
    }
}
