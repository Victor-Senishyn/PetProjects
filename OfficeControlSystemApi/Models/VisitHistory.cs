using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using OfficeControlSystemApi.Models.Interface;

namespace OfficeControlSystemApi.Models
{
    [Table("visit_history")]
    public class VisitHistory : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [ForeignKey(nameof(AccessCardId))]
        public long AccessCardId { get; set; }
        public DateTimeOffset VisitDateTime { get; set; }
        public DateTimeOffset ExitDateTime { get; set; }
    }
}
