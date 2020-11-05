using System;
using System.ComponentModel.DataAnnotations;

namespace KrutQuest.Service.Transport
{
    public class LoginRequest
    {        
        public string Login { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
