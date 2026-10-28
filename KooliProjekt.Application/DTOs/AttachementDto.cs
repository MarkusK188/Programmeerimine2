using System;

namespace KooliProjekt.Application.DTOs
{
    public class AttachementDto
    {
        public int Id { get; set; }
        public int? AssignmentId { get; set; }
        public string AssignmentName { get; set; }
        public string FileName { get; set; }
        public DateTime UploadTime { get; set; }
        public int? UploadedById { get; set; }
        public string UploadedByUserName { get; set; }

        // FilePath is intentionally omitted because it may reveal an internal server path.
    }
}
