using System;

namespace KooliProjekt.Application.DTOs
{
    public class WorkLogDto
    {
        public int Id { get; set; }
        public int? AssignmentId { get; set; }
        public string AssignmentName { get; set; }
        public DateTime Date { get; set; }
        public decimal TimeSpent { get; set; }
        public int? ImplementerId { get; set; }
        public string ImplementerUserName { get; set; }
        public string Description { get; set; }
    }
}
