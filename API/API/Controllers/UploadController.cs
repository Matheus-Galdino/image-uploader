using System;
using System.IO;
using API.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Azure.Storage.Blobs.Models;
using System.Collections.Generic;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private BlobStorage blob = new BlobStorage();

        [HttpPost]
        public async Task<string> UploadAsync()
        {
            var form = Request.Form;
            var stream = form.Files[0].OpenReadStream();
            var extension = Path.GetExtension(form.Files[0].FileName);
            string fileName = $"{Guid.NewGuid()}{extension}";
            await blob.UploadFile(stream, fileName);

            return "I hope i've had success";
        }

        [HttpGet]
        public async Task<List<BlobItem>> GetAllFiles() => await blob.GetAllFiles();

        [HttpGet("{id}")]
        public string DownloadFile(string name)
        {
            return "Hello Friend";
        }
    }
}
