using System;
using System.IO;
using API.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly BlobStorage blob = new BlobStorage();

        [HttpPost]
        public async Task<string> UploadAsync()
        {
            var form = this.Request.Form;
            var stream = form.Files[0].OpenReadStream();            
            var extension = Path.GetExtension(form.Files[0].FileName);
            var fileName = $"{Guid.NewGuid()}{extension}";
            await this.blob.UploadFile(stream, fileName);
            return $"https://localhost:44337/{fileName}";
        }

        [HttpGet]
        [Route("/{id}")]
        public async Task<Stream> DownloadFile([FromRoute] string id) => await this.blob.GetFile(id);
    }
}
