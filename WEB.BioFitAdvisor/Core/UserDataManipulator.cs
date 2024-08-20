using API.BioFitAdvisor.Domain.DTO;
using WEB.BioFitAdvisor.Models;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace WEB.BioFitAdvisor.Core
{
    public class UserDataManipulator
    {
        private static readonly byte[] Key = Encoding.UTF8.GetBytes("3A7F9E5D2B1C8F4E6A0D4B9C8E2F6A3D");
        private static readonly byte[] IV = Encoding.UTF8.GetBytes("4B7A3D1E9F5C2E8F");

        private IHttpContextAccessor _HttpContextAccessor;

        public UserDataManipulator(IHttpContextAccessor httpContextAccessor) 
		{
            _HttpContextAccessor = httpContextAccessor;
        }

        public void SetUserData(ResponseToken token, bool KeepSession = false, int Company_ID = 0)
        {
			try
			{
                UserData user = new UserData();

                user.Token = token.token;
                user.ExpieredDate = token.ExpirationDate;
                user.KeepSession = KeepSession;
                user.Company_ID = Company_ID;
                user.Name = token.UserName;
                user.Email = token.Email;
                user.UserId = token.UserId;

                string json = JsonConvert.SerializeObject(user);

                _HttpContextAccessor.HttpContext.Session.SetString("UserData", Encrypt(json));
            }
			catch (Exception)
			{
				throw;
			}
        }

        public void SetUserData(UserData UserData)
        {
            try
            {
                string json = JsonConvert.SerializeObject(UserData);

                _HttpContextAccessor.HttpContext.Session.SetString("UserData", Encrypt(json));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public UserData GetUserData()
        {
            try
            {
                string UserData = _HttpContextAccessor.HttpContext.Session.GetString("UserData")??"";

                if (string.IsNullOrEmpty(UserData))
                {
                    return null;
                }

                UserData user = JsonConvert.DeserializeObject<UserData>(Decrypt(UserData));

                if (user != null && user.ExpieredDate < DateTime.Now  )
                {
                    _HttpContextAccessor.HttpContext.Session.Clear();
                    return null;
                }

                return user;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static string Encrypt(string plainText)
        {
            using (var aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    using (var swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

        public static string Decrypt(string cipherText)
        {
            var cipherBytes = Convert.FromBase64String(cipherText);

            using (var aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (var msDecrypt = new MemoryStream(cipherBytes))
                using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                using (var srDecrypt = new StreamReader(csDecrypt))
                {
                    return srDecrypt.ReadToEnd();
                }
            }
        }
    }
}
