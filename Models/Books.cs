using System.ComponentModel.DataAnnotations.Schema;

namespace Personalized_Library_System.Models;

[Table("Books")]
public class Books
{
    public int Id { get; set; }

    public string? Title { get; set; }
    public string? FilePath { get; set; }

}

public class BooksVm
{
    public string? Name { get; set; }
    public IFormFile? FileUpload { get; set; }
    // public IEnumerable<Books>? Books { get; internal set; }
}

// public class FileUpload
// {
//     public IFormFile? FormFile { get; set; }
// }