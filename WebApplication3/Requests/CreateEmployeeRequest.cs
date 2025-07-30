using System.Text.Json.Serialization;
using WebApplication3.Data;

namespace WebApplication3
{
    public class CreateEmployeeRequest
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        [JsonRequired]
        public int CompanyId { get; set; }
        public Passport Passport { get; set; }
        public Department Department { get; set; }
    }
}
