using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using OfficeControlSystemApi.Models.Interface;

namespace OfficeControlSystemApi.Models
{
    public class AccessCard : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [ForeignKey("EmployeeId")]
        public long EmployeeId { get; set; }
        public AccessLevel AccessLevel { get; set; }
        public Employee Employee { get; set; }
        public ICollection<VisitHistory> VisitHistories { get; set; }
    }
}
