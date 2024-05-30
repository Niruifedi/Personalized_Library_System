using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Personalized_Library_System.Models;

[Table("User")]
public class User
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
    [Required]
    public string? Email { get; set; }
    [Required]
    public string? Username { get; set; }
    [Required]
    public string? Password { get; set; }

    // public ICollection<User_Catalogue> Catalogues { get; } = new List<User_Catalogue>();
    
    public User() {}
}
