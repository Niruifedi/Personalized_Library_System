using Personalized_Library_System.Models;
using Personalized_Library_System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Personalized_Library_System.Controllers;

[ApiController]
[Route("api/[controller]/")]
public class BookController : ControllerBase
{
    
    private readonly AppDbContext _context;

    public BookController(AppDbContext context)
    {
        this._context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Books>>> Get() => 
    await _context.Books.ToListAsync();

    [HttpPost]
    public async Task<IActionResult> Upload(BooksVm books)
    {

        if (books.FileUpload != null)
        {
            var filePath = Path.GetFileName(books.FileUpload.FileName);
            string ext = Path.GetExtension(books.FileUpload.FileName);
            if (ext.ToLower() != ".pdf")
            {
                return BadRequest();
            }

            using (var stream = System.IO.File.Create(filePath))
            {
                await books.FileUpload.CopyToAsync(stream);
            }

            Books books1 = new Books();
            books1.Title = books.Name;
            books1.FilePath = filePath;

            _context.Books.Add(books1);
            await _context.SaveChangesAsync();

            // User_Catalogue useerCatalogue = new User_Catalogue{
            //     Books = new List<Books> { books1 }
            // };
            // var newBook = new List<Books> {books1};
            // if (newBook != null)
            //     _context.Catalogues.AddAsync(newBook);
        }
        else
        {
            return BadRequest();
        }    
        return Ok();
    }
}