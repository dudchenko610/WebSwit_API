using System;
using System.Text;

namespace WebSwIT.BusinessLogicLayer.Helpers
{
    public class GeneratePasswordHelper
    {
        public string GeneratePassword()
        {
            string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            var result = new StringBuilder();
            var random = new Random();

            for (int i = default; i < 10; i++)
            {
                char value = valid[random.Next(valid.Length)];
                result.Append(value);
            }

            string newPassword = result.ToString();

            return newPassword;
        }
    }
}
