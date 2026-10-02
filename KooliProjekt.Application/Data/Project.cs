using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Data
{
    public class Project
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string ProjectName { get; set; }
        
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        
        [Range(typeof(Decimal), "0.01", "9999999999999999E+28")]
        public Decimal Budget { get; set; }

        [Range(typeof(Decimal), "0.01", "9999999999999999E+28")]
        public Decimal HourlyRate { get; set; }
        public Team Team { get; set; }
    }
}
