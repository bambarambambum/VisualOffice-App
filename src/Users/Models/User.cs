using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Users.API.Models
{
    public class User
    {
        public int Id { get; set; }
        public int RpId { get; set; }
        public RP RP { get; set; }
        public string Fio { get; set; }
        public string Email { get; set; }
    }
    public class RP
    {
        public int Id { get; set; }
        public string Fio { get; set; }
        public string Email { get; set; }
        public List<User> User { get; set; }
        public RP()
        {
            User = new List<User>();
        }
    }
}
