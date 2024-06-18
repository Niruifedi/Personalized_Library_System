using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Personalized_Library_System.Models;

[Table("User")]
public class User
{
    public static int _lastId = 0;
    public int Id { get; private set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }

    public virtual ICollection<User_Catalogue> Catalogues { get; set; } = new List<User_Catalogue>();
    
    public User() {}

    public int Item()
    {
        Id = ++_lastId;
        return Id;
    }
}
