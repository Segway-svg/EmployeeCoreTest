using System.Text.Json.Serialization;
using WebApplication3.Data;

namespace WebApplication3
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; } 
        public string Phone { get; set; }
        public int CompanyId { get; set; }
        public Passport Passport { get; set; }
        public Department Department { get; set; }
    }
}
