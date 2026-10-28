using System;

namespace KooliProjekt.Application.DTOs
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public decimal Budget { get; set; }
        public decimal HourlyRate { get; set; }
        public int? TeamId { get; set; }
    }
}
