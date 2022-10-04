using Edutone2022.Common.Interfaces;
using Edutone2022.Common.Models;
using Edutone2022.Common.Models.File;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Edutone2022.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class UploadController : ControllerBase
    {
        private readonly IWebHostEnvironment hostEnvironment;
        private readonly IRepository repository;

        public UploadController(IWebHostEnvironment hostEnvironment, IRepository repository)
        {
            this.hostEnvironment = hostEnvironment;
            this.repository = repository;
        }

        [Authorize]
        [HttpPost("article")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(FileModel))]
        public async Task<IActionResult> UploadArticleImage(UploadFileRequest request )
        {
            var uploadPath = Path.Combine("upload", "articles");
            var dirPath = Path.Combine(hostEnvironment.ContentRootPath, uploadPath);
            Directory.CreateDirectory(dirPath);
            var fullPath = Path.Combine(dirPath, request.FileName);

            using (var fileStream = System.IO.File.Create(fullPath))
            {
                await fileStream.WriteAsync(request.FileContent);
            }

            var fileModel = await repository.SaveFile(request.FileName, uploadPath);

            return Ok(fileModel);
        }

        [Authorize]
        [HttpPost("contact")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(FileModel))]
        public async Task<IActionResult> UploadContactImage(UploadFileRequest request)
        {
            var uploadPath = Path.Combine("upload", "contacts");
            var dirPath = Path.Combine(hostEnvironment.ContentRootPath, uploadPath);
            Directory.CreateDirectory(dirPath);
            var fullPath = Path.Combine(dirPath, request.FileName);

            using (var fileStream = System.IO.File.Create(fullPath))
            {
                await fileStream.WriteAsync(request.FileContent);
            }

            var fileModel = await repository.SaveFile(request.FileName, uploadPath);

            return Ok(fileModel);
        }        
    }
}
