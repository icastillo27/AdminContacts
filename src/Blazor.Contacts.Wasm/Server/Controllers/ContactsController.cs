using Blazor.Contacts.Wasm.Repositories;
using Blazor.Contacts.Wasm.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blazor.Contacts.Wasm.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]    
    public class ContactsController : ControllerBase
    {
        private readonly IContactRepository _contactRepository;

        public ContactsController(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Contact contact)
        {

            if (contact == null)
                return BadRequest();

            if (string.IsNullOrEmpty(contact.FirstName))
            {
                ModelState.AddModelError("FirstName", "First Name can´t be empty");
            }
            if (string.IsNullOrEmpty(contact.LastName))
            {
                ModelState.AddModelError("LastName", "Last Name can´t be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _contactRepository.InsertConctact(contact);

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Contact contact)
        {

            if (contact == null)
                return BadRequest();

            if (id == 0)
            {
                ModelState.AddModelError("Id", "Id can´t be empty");
            }

            if (string.IsNullOrEmpty(contact.FirstName))
            {
                ModelState.AddModelError("FirstName", "First Name can´t be empty");
            }
            if (string.IsNullOrEmpty(contact.LastName))
            {
                ModelState.AddModelError("LastName", "Last Name can´t be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _contactRepository.UpdateContact(contact);

            return NoContent();
        }

        [HttpGet]
        public async Task<IEnumerable<Contact>> Get()
        {
            return await _contactRepository.GetAll();
        }


        [HttpGet("{id}")]
        public async Task<Contact> Get(int id)
        {
            return await _contactRepository.GetDetail(id);
        }


        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _contactRepository.DeleteContact(id);
        }
    }
}
