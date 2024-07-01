using System.ComponentModel.DataAnnotations.Schema;

namespace Personalized_Library_System.Models;

[Table("login")]

public class LoginModel
{
    public string? Username { get; set; }
    public string? Password { get; set; }
}