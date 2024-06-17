using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;


namespace Personalized_Library_System.Models;

[Table("Catalogue")]
public class User_Catalogue
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }
    public string? Description { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }

    public ICollection<Books> Books { get; set; }
}