using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopContactApp.Classes
{
    public class Contact
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }

        public override string ToString()
        {
            return $"{name} - {email} - {phoneNumber}";
        }
    }
}
