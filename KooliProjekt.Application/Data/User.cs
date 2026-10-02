using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Data
{
    [Index(nameof(UserName), IsUnique = true)]
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(25)]
        public string UserName { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Email { get; set; }
        
        public string Role { get; set; }
        
        [Required]
        [StringLength(100)]
        public string PasswordHash { get; set; }
        
    }
}
