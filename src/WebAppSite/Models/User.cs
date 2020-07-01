using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppSite.Models
{
    public class User
    {
        public int Id { get; set; }
        public int RpId { get; set; }
        public RP RP { get; set; }
        [Required]
        [StringLength(75, MinimumLength = 6, ErrorMessage = "Длина строки должна быть минимум 6 букв")]
        public string Fio { get; set; }
        [Required]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный почтовый адрес")]
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
