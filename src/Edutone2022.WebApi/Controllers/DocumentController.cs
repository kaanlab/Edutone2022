using Edutone2022.Common.Interfaces;
using Edutone2022.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Edutone2022.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class DocumentController : ControllerBase
    {
        private readonly IRepository repository;

        public DocumentController(IRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<DocumentModel>))]
        public async Task<IActionResult> All() => Ok(await repository.GetDocuments());

        [Authorize]
        [HttpPost("add")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(DocumentModel))]
        public async Task<IActionResult> Add(DocumentModel document) => Ok(await repository.CreateDocument(document));

        [Authorize]
        [HttpPut("update/{documentId:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DocumentModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(Guid documentId, DocumentModel document)
        {
            var updatedDocument = await repository.GetDocumentById(documentId);
            if (updatedDocument is null)
            {
                return NotFound();
            }

            if (updatedDocument.Id != document.Id)
            {
                return BadRequest();
            }

            return Ok(await repository.UpdateDocument(document));
        }

        [Authorize]
        [HttpDelete("delete/{documentId:Guid}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(Guid documentId)
        {
            var deletedDocument = await repository.GetDocumentById(documentId);
            if (deletedDocument is null)
            {
                return NotFound();
            }

            await repository.DeleteDocument(documentId);

            return NoContent();
        }
    }
}
