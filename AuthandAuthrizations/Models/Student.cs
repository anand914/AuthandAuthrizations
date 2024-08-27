using System.ComponentModel.DataAnnotations;

namespace AuthandAuthrizations.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public DateTime Dob { get; set; }
    }
}
