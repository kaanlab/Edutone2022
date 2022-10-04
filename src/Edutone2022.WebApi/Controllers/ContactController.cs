using Edutone2022.Common.Interfaces;
using Edutone2022.Common.Models;
using Edutone2022.Common.Models.Contact;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Edutone2022.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class ContactController : ControllerBase
    {
        private readonly IRepository repository;

        public ContactController(IRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<EmployeeContactModel>))]
        public async Task<IActionResult> All() => Ok(await repository.GetContacts());

        [Authorize]
        [HttpPost("add")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(EmployeeContactModel))]
        public async Task<IActionResult> Add(ContactAddRequest request) => Ok(await repository.CreateContact(request));        

        [Authorize]
        [HttpPut("update/{contactId:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EmployeeContactModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(Guid contactId, EmployeeContactModel contact)
        {
            var updatedContact = await repository.GetContactById(contactId);
            if (updatedContact is null)
            {
                return NotFound();
            }

            if (updatedContact.Id != contact.Id)
            {
                return BadRequest();
            }

            return Ok(await repository.UpdateContact(contact));
        }

        [Authorize]
        [HttpDelete("delete/{contactId:Guid}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(Guid contactId)
        {
            var deletedContact = await repository.GetContactById(contactId);
            if (deletedContact is null)
            {
                return NotFound();
            }

            await repository.DeleteContact(contactId);

            return NoContent();
        }
    }
}
