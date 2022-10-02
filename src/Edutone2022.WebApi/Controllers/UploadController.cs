using Edutone2022.Common;
using Edutone2022.Common.Interfaces;
using Edutone2022.Common.Models;
using Edutone2022.Common.Models.File;
using Edutone2022.Storage.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.IO;
using static Edutone2022.Common.Global;

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
        public async Task<IActionResult> UploadAvatar(UploadFileRequest request )
        {
            var uploadPath = Path.Combine("upload", "articles");
            var dirPath = Path.Combine(hostEnvironment.ContentRootPath, uploadPath);
            Directory.CreateDirectory(dirPath);
            var fullPath = Path.Combine(dirPath, request.FileName);

            using (var stream = new FileStream(fullPath, FileMode.Create)) 
            { 
                stream.Write(request.FileContent, 0, request.FileContent.Length);
            }

            var fileModel = await repository.SaveFile(request.FileName, uploadPath);

            return Ok(fileModel);
        }
    }
}
