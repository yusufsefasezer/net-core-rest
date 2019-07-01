using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using net_core_rest.Data;
using net_core_rest.Models;

namespace net_core_rest.Controllers
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly ContactDbContext _context;

        public ContactsController(ContactDbContext context)
        {
            _context = context;
        }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        // GET: api/Contacts

        /// <summary>
        /// Retrieves contact list
        /// </summary>
        /// <returns>A response with contact list</returns>
        /// <response code="200">Returns the contact list</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contact>>> GetContacts()
        {
            return await _context.Contacts.ToListAsync();
        }

        // GET: api/Contacts/5

        /// <summary>
        /// Retrieves a contact by id
        /// </summary>
        /// <param name="id">contact id</param>
        /// <returns>A response with contact</returns>
        /// <response code="200">Returns the contact</response>
        /// <response code="404">If contact is not exists</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<Contact>> GetContact(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);

            if (contact == null)
            {
                return NotFound();
            }

            return contact;
        }

        // PUT: api/Contacts/5

        /// <summary>
        /// Updates an existing contact
        /// </summary>
        /// <param name="id">contact id</param>
        /// <param name="contact">Contact model</param>
        /// <returns>A response as update contact result</returns>
        /// <response code="200">If contact was updated successfully</response>
        /// <response code="400">For bad request</response>
        /// <response code="404">If contact is not exists</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContact(int id, Contact contact)
        {
            if (id != contact.Id)
            {
                return BadRequest();
            }

            _context.Entry(contact).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }

            return Ok();
        }

        // POST: api/Contacts

        /// <summary>
        /// Creates a new contact
        /// </summary>
        /// <param name="contact">Contact model</param>
        /// <returns>A response with new contact</returns>
        /// <response code="201">A response as creation of contact</response>
        [HttpPost]
        public async Task<ActionResult<Contact>> PostContact(Contact contact)
        {
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContact", new { id = contact.Id }, contact);
        }

        // DELETE: api/Contacts/5

        /// <summary>
        /// Deletes an existing contact
        /// </summary>
        /// <param name="id">Contact id</param>
        /// <returns>A response as delete contact result</returns>
        /// <response code="200">If contact was deleted successfully</response>
        /// <response code="404">If contact is not exists</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Contact>> DeleteContact(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();

            return contact;
        }

        private bool ContactExists(int id)
        {
            return _context.Contacts.Any(e => e.Id == id);
        }
    }
}
