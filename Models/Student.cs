using System.ComponentModel.DataAnnotations;

namespace StudentLibrary.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Mobile { get; set; }

        public string Email { get; set; }
    }
}
