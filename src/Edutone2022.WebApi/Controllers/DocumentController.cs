using Azure.Core;
using Edutone2022.Common.Interfaces;
using Edutone2022.Common.Models;
using Edutone2022.Common.Models.Document;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace Edutone2022.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class DocumentController : ControllerBase
    {
        private readonly string _uploadPath = Path.Combine("upload", "docs");
        private readonly IWebHostEnvironment hostEnvironment;
        private readonly IRepository repository;

        public DocumentController(IRepository repository, IWebHostEnvironment hostEnvironment)
        {
            this.repository = repository;
            this.hostEnvironment = hostEnvironment;
        }

        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<DocumentModel>))]
        public async Task<IActionResult> All() => Ok(await repository.GetDocuments());

        [Authorize]
        [HttpPost("add")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(DocumentModel))]
        public async Task<IActionResult> Add(DocumentAddRequest request)
        {
            await SaveOnDisk(request.FileName, request.FileContent);
            return Ok(await repository.CreateDocument(_uploadPath, request));
        }

        [Authorize]
        [HttpPut("update/{documentId:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DocumentModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(Guid documentId, DocumentUpdateRequest document)
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

            await SaveOnDisk(document.FileName, document.FileContent);
            return Ok(await repository.UpdateDocument(_uploadPath, document));
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

        private async Task SaveOnDisk(string fileName, byte[] fileContent)
        {
            var dirPath = Path.Combine(hostEnvironment.ContentRootPath, _uploadPath);
            Directory.CreateDirectory(dirPath);
            var fullPath = Path.Combine(dirPath, fileName);

            using (var fileStream = System.IO.File.Create(fullPath))
            {
                await fileStream.WriteAsync(fileContent);
            }
        }
    }
}
