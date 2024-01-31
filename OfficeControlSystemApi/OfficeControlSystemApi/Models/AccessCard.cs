namespace OfficeControlSystemApi.Models
{
    public class AccessCard
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public int AccessLevel { get; set; }
        public Employee Employee { get; set; }
        public List<AccessHistory> AccessHistory { get; set; } = new List<AccessHistory>();
    }
}
