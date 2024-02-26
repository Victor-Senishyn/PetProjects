using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OfficeControlSystemApi.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OfficeControlSystemApi.Models.Identity
{
    [Table("user")]
    public class User : IdentityUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id {  get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public Permission Permission { get; set; }
        public Employee Employee { get; set; }
        public AccessCard AccessCard { get; set; }
    }
}
