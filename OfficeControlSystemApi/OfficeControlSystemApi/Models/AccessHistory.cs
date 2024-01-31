using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OfficeControlSystemApi.Models
{
    public class AccessHistory
    {
        public Guid AccessHistoryId { get; set; }
        public Guid AccessCardId { get; set; }
        public AccessCard AccessCard { get; set; }
        public DateTime EntryTime { get; set; }
        public DateTime? ExitTime { get; set; }
    }
}
