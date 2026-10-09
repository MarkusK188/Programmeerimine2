using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Data
{
    public class Team
    {
        public int Id { get; set; }

        [ForeignKey("TeamLeadId")]
        [Required]
        public User TeamLead { get; set; }
        
        [ForeignKey("UserId")]
        public User User { get; set; }
        
    }
}
