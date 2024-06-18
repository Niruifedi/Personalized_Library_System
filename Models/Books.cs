using System.ComponentModel.DataAnnotations.Schema;

namespace Personalized_Library_System.Models;

[Table("Books")]
public class Books
{
    public static int _lastId = 0;
    public int Id { get; private set; }

    public string? Title { get; set; }
    public string? FilePath { get; set; }
    public string? Genre { get; set; }
    public int CatalogueId { get; set; }
    public User_Catalogue? Catalogue { get; set; }

    public Books() {}

    public int Item()
    {
        Id = ++_lastId;
        return Id;
    }

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