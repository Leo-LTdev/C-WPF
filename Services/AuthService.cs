using System;
using System.Text;

namespace WpfApp1.Services
{
    public class AuthService
    {
        public string HashEnBase(string password)
        {
            if (string.IsNullOrEmpty(password)) return string.Empty;
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(bytes);
        }
    }
}