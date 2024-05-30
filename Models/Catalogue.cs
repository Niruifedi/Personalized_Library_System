using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Personalized_Library_System.Models;

public class User_Catalogue
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;
}