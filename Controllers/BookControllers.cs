using Personalized_Library_System.Models;
using Personalized_Library_System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace Personalized_Library_System.Controllers;

[ApiController]
[Route("api/[controller]/")]
public class BookController : ControllerBase
{
    // controller for the book model
    
    private readonly AppDbContext _context;

    public BookController(AppDbContext context)
    {
        // constructor for the book controller
        this._context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Books>>> Get() =>
    // This Returns the list of all books in the database
    await _context.Books.ToListAsync();
    
    [HttpGet("{id}")]
    public async Task<ActionResult> getId(int id)
    {
        // This Returns a Book with the specified ID {id}
        var book = await _context.Books.FindAsync(id);
        if (book is null)
        {
            return NotFound();
        }
        return Ok(book);
    }

    [HttpPost]
    public async Task<IActionResult> Upload([FromForm]int userId, BooksVm books, [FromForm]string genre)
    {
        // This creates a new book in the database
        if (books.FileUpload != null)
        {
            var filePath = Path.GetFileName(books.FileUpload.FileName);
            string ext = Path.GetExtension(books.FileUpload.FileName);
            if (ext.ToLower() != ".pdf")
            {
                return BadRequest(ModelState);
            }

            using (var stream = System.IO.File.Create(filePath))
            {
                await books.FileUpload.CopyToAsync(stream);
            }
            var user = await _context.User.FindAsync(userId);
            if (user is null)
            {
                return NotFound();
            }
            Books books1 = new Books{
                Title = books.Name,
                FilePath = filePath,
                CatalogueId = user.Id,
                Genre = genre,
                Catalogue = user.Catalogues.FirstOrDefault(userId => userId.UserId == user.Id)
            };

            _context.Books.Add(books1);
            await _context.SaveChangesAsync();
        }
        else
        {
            return BadRequest(ModelState);
        }    
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> update(int id)
    {
        // This Updates a specific Book object using the {id}
        if (id == 0)
        {
            return BadRequest();
        }

        var book = await _context.Books.FindAsync(id);
        if (book?.FilePath == null)
        {
           return NotFound();
        }
        _context.Entry(book).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (book is null)
            {
                return NotFound();
            }
            else
                throw;
        }

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        // This Deletes a specific Book object using the {id}            
        var book = await _context.Books.FindAsync(id);

        if (book == null)
            return NotFound();

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();

        return Ok();
    }

}