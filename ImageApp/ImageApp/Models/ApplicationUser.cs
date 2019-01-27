using Microsoft.AspNetCore.Identity;
using System;

namespace ImageApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime DateCreated { get; set; }
    }
}
