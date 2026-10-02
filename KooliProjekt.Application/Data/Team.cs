using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Data
{
    public class Team
    {
        public int Id { get; set; }

        [Required]
        public User TeamLead { get; set; }
        
        public User User { get; set; }
        public int UserId { get; set; }
    }
}
