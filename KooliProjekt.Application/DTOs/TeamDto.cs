namespace KooliProjekt.Application.DTOs
{
    public class TeamDto
    {
        public int Id { get; set; }
        public int? TeamLeadId { get; set; }
        public string TeamLeadUserName { get; set; }
        public int? UserId { get; set; }
        public string UserName { get; set; }
    }
}
