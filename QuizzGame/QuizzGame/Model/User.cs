using System;
using System.Collections.Generic;
using System.Text;

namespace QuizzGame.Model
{
    public class User
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
