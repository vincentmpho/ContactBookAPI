using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContactBookAPI.Data;
using ContactBookAPI.Models;
using ContactBookAPI.Models.DTOs;

namespace ContactBookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly ContactDbContext _context;

        public ContactsController(ContactDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllContacts()
        {
            try
            {
                var contacts = await _context.Contacts.ToListAsync();
               
                return StatusCode(StatusCodes.Status200OK, contacts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddContact(AddContactRequestDTO request)
        {
            try
            {
                // Map the request into a domain model of contact
                var domainModelContact = new Contact
                {
                    Id = Guid.NewGuid(),
                    Name = request.Name,
                    Email = request.Email,
                    Phone = request.Phone,
                    Favorite = request.Favorite,
                };

                _context.Contacts.Add(domainModelContact);
                await _context.SaveChangesAsync();

                return StatusCode(StatusCodes.Status200OK, domainModelContact);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteContact(Guid id)
        {
           var contact = await _context.Contacts.FindAsync(id);

            if ( contact is not null)
            {
                _context.Contacts.Remove(contact);
                await _context.SaveChangesAsync();
            }

            return StatusCode(StatusCodes.Status200OK, new { message = $"Contact with ID {id} deleted successfully." });

        }
    }
}
