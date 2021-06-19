using System;
using System.Collections.Generic;
using System.Text;

namespace CarsAppAPI.Models
{

    public class RegisterModel
    { 
        public User user { get; set; }
        public Errors errors { get; set; }
    }

    public class User
    {
        public string name { get; set; }
        public string email { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime created_at { get; set; }
        public int id { get; set; }
    }

    public class Errors
    {
        public string[] email { get; set; }
        public string[] password { get; set; }
        public string[] name { get; set; }
    }

}
