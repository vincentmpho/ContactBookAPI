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
         
        public async Task <IActionResult> GetAllContacts()
        {
           var contacts = await _context.Contacts.ToListAsync();

            return Ok(contacts);
        }

        [HttpPost]

        public async Task <IActionResult> AddContact(AddContactRequestDTO request)
        {
            //Map the request into a domain model of contact
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
            return Ok(domainModelContact);

        }
    }
}
