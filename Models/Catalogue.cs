using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;


namespace Personalized_Library_System.Models;

[Table("Catalogue")]
public class User_Catalogue
{
    public static int _lastId = 0;
    public int Id { get; private set; }

    [Required]
    public string? Name { get; set; }
    public string? Description { get; set; }
    // public DateTime PublishedOn { get; set; }
    public int UserId { get; set; }

    // public virtual User? User { get; }

    public virtual ICollection<Books> Books { get; set; } = new List<Books>();

    public User_Catalogue() {}

    public int Item()
    {
        Id = ++_lastId;
        return Id;
    }
}