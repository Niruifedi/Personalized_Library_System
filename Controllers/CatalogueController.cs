using Personalized_Library_System.Models;
using Personalized_Library_System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Personalized_Library_System.Controllers;

[ApiController]
[Route("api/[controller]/")]
public class CatalogueController: ControllerBase
{
    private readonly  AppDbContext _context;

    public CatalogueController(AppDbContext context)
    {
        this._context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User_Catalogue>>> Get() =>
    // This Returns the list of all catalogues in the database
    await _context.Catalogues.ToListAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult> getId(int id)
    {
        // This Returns a Catalogue with the specified ID {id}
        var catalogue = await _context.Catalogues.FindAsync(id);
        if (catalogue is null)
            return NotFound("Catalogue Not Found");

        return Ok(catalogue);
    }

    [HttpPost]
    public async Task<IActionResult> create([FromForm]User_Catalogue catalogue)
    {
        // This creates a new Catalogue in the database
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        _context.Add(catalogue);
        await _context.SaveChangesAsync();
        if (catalogue.UserId != 0)
        {
            // var userId = catalogue.UserId;
            var user = await _context.User.FindAsync(catalogue.UserId);
            if (user != null)
            {
                user.Catalogues.Add(catalogue);
                // _context.Entry(user.Catalogues).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            } 
            else
            {
                return NotFound($"No User Found with id ");
            }

        }

        return Created("Catalogue Created Successfully", catalogue);    

    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromForm] int id, [FromForm] User_Catalogue catalogue)
    {
        // This Updates a specific Catalogue object using the {id}
        if (id != catalogue.Id)
            return BadRequest("Id Mismatch");

        var catalogue1 = await _context.Catalogues.FindAsync(id);
        if (catalogue1 is null)
            return NotFound("Catalogue Not Found");

        catalogue1.Name = catalogue.Name;
        catalogue1.Description = catalogue.Description;
        catalogue1.UserId = catalogue.UserId;

        await _context.SaveChangesAsync();
        return Ok("Catalogue Updated Successfully");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        // This Deletes a specific Catalogue object using the {id}
        var catalogue = await _context.Catalogues.FindAsync(id);
        if (catalogue is null)
            return NotFound("Catalogue Not Found");

        _context.Catalogues.Remove(catalogue);
        await _context.SaveChangesAsync();
        return Ok("Catalogue Deleted Successfully");
    }

}