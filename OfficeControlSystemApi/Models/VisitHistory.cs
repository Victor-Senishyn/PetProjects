using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using OfficeControlSystemApi.Models.Interface;

namespace OfficeControlSystemApi.Models
{
    public class VisitHistory : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public long AccessCardId { get; set; }
        public DateTimeOffset VisitDateTime { get; set; }
        public DateTimeOffset ExitDateTime { get; set; }
        [ForeignKey(nameof(AccessCardId))]
        public AccessCard AccessCard { get; set; }
    }
}
