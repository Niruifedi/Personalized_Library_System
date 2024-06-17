using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Personalized_Library_System.Models;

[Table("User")]
public class User
{
    [Required]
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }

    public virtual ICollection<User_Catalogue> Catalogues { get; set; } = new List<User_Catalogue>();
    
    public User() {}
}
