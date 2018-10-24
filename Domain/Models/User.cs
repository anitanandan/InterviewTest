using System.ComponentModel.DataAnnotations;

namespace Interview.Domain.Models
{
    public class User
    {
        public long Id { get; set; }
        public string Username { get; set; }
        //[Required(ErrorMessage = "Employee {0} is required")]
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
}
