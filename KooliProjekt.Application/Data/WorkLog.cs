using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Data
{
    public class WorkLog
    {
        public int Id { get; set; }
        [Required]
        public Assignment Assignment { get; set; }
        public DateTime Date { get; set; }
        public Decimal TimeSpent { get; set; }
        [Required]
        public User Implementer { get; set; }
        [Required]
        [StringLength(1000)]
        public string Description { get; set; }
    }
}
