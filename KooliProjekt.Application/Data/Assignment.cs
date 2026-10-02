using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Data
{
    public class Assignment
    {
        public int Id { get; set; }
        public string AssignmentName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }

        [Range(typeof(Decimal), "0.01", "9999999999999999E+28")]
        public Decimal EstimatedHours { get; set; }
        [Required]
        public Boolean FixedPrice { get; set; }
        [Required]
        public User TeamLead { get; set; }
        [Required]
        [StringLength(1000)]
        public string AssignmentDesc { get; set; }
    }
}
