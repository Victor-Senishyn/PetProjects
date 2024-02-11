namespace OfficeControlSystemApi.Models.DTOs
{
    public class VisitHistoryDto
    {
        public long Id { get; set; }
        public long AccessCardId { get; set; }
        public DateTimeOffset VisitDateTime { get; set; }
        public DateTimeOffset ExitDateTime { get; set; }
    }
}
