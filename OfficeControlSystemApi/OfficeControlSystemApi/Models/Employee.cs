using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OfficeControlSystemApi.Models
{
    public class Employee
    {
        public Guid EmployeeId { get; set; }
        public Guid AccessCardId { get; set; }
        public string Name { get; set; }
        public AccessCard AccessCard { get; set; }
    }
}
