using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContactsApi.Data;
using ContactsApi.Models;

namespace ContactsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly ContactsApiDbContext _context;

        public ContactsController(ContactsApiDbContext context)
        {
            _context = context;
        }

        // GET: api/Contacts
        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            var contacts = await _context.Contacts.ToListAsync();
            return Ok(contacts);
        }

        

        // POST: api/Contacts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostContact(AddContactRequest addContactRequest)
        {


            var contact = new Contact() 
          {
                Id = Guid.NewGuid(),
              Email = addContactRequest.Email,
              Phone = addContactRequest.Phone,
              FullName = addContactRequest.FullName,
          
          };
          await  _context.Contacts.AddAsync(contact);
            await _context.SaveChangesAsync();

            return Ok(contact);
        }

        [HttpPut]
        [Route("{id:guid}")]

        public async Task <ActionResult> PutContact([FromRoute] Guid id, UpdateContactRequest updateContactRequest)

        {
            var contact= await _context.Contacts.FindAsync(id);

            if (contact != null)
            {
                contact.Email = updateContactRequest.Email;
                contact.Phone = updateContactRequest.Phone;
                contact.FullName = updateContactRequest.FullName;

                await _context.SaveChangesAsync();

                return Ok(contact);
            
            }

            return NotFound();

        }


    }
}
