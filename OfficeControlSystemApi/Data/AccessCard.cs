using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OfficeControlSystemApi.Data
{
    public class AccessCard
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTimeOffset IssuedDate { get; set; }
        public DateTimeOffset? ExpiryDate { get; set; }
    }
}
