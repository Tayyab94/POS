using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Shop.Models.AuthModel
{
    public class AuthUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        //private string _userRole;

        //public string UserRole
        //{
        //    get { return _userRole; }
        //    set
        //    {
        //        _userRole = value;
        //        Role= Enum.TryParse<AuthUserRole>(value, out var role)?role : AuthUserRole.Cashier;
        //    }
        //}

        //[NotMapped]
        //public AuthUserRole Role { get; set; }


        [Required]
        public AuthUserRole Role { get; set; }
        public DateTime CreatedAt { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime? LastLogin { get; set; }

        public AuthUser()
        {
            CreatedAt = DateTime.Now;
        }
    }

    // Models/AuthUserRole.cs
    public enum AuthUserRole
    {
        SuperAdmin,
        Admin,
        Cashier
    }
}
