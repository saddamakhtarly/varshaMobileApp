using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Models
{
    public class LoginResponse : Response
    {

    }

    public class Response
    {
        public bool IsValid { get; set; }
        public string Message { get; set; }
    }
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
