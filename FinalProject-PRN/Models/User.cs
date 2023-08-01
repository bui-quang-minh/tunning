using System;
using System.Collections.Generic;

namespace FinalProject_PRN.Models
{
    public partial class User
    {
        public User()
        {
            Orders = new HashSet<Order>();
        }

        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public bool IsAdmin { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ImageRef { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
