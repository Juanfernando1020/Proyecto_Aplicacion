using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Pages.Main.Profile.Models
{
    public class ProfileInformation
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Location { get; set; }
        public string Admin { get; set; }
        public bool IsAdmin { get; set; }

        public ProfileInformation(string name, string phone, string location, bool isAdmin, string admin = null)
        {
            Name = name;
            Phone = phone;
            Location = location;
            Admin = admin;
            IsAdmin = isAdmin;
        }
    }
}
