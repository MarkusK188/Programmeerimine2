using System;

namespace KooliProjekt.Application.DTOs
{
    public class AssignmentDto
    {
        public int Id { get; set; }
        public string AssignmentName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public decimal EstimatedHours { get; set; }
        public bool FixedPrice { get; set; }
        public int? TeamLeadId { get; set; }
        public string TeamLeadUserName { get; set; }
        public string AssignmentDesc { get; set; }
    }
}
