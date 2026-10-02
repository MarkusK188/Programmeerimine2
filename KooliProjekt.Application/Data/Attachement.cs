using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Data
{
    public class Attachement
    {
        public int Id { get; set; }
        [Required]
        public Assignment Assignment { get; set; }
        [Required]
        public string FileName { get; set; }
        [Required]
        public string FilePath { get; set; }
        public DateTime UploadTime { get; set; }
        [Required]
        public User UploadedBy { get; set; }
    }
}
