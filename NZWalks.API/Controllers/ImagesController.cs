using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        [HttpPost("/Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDTO imageDetails)
        {
            ValidateFileUpload(imageDetails);
            if(ModelState.IsValid)
            {
                var imageDomainModel = new Image
                {
                    File = imageDetails.File,
                    FileExtension = Path.GetExtension(imageDetails.File.FileName),
                    FileSizeInBytes = imageDetails.File.Length,
                    FileName = imageDetails.FileName,
                    FileDescription = imageDetails.FileDescription,
                };

                await imageRepository.Upload(imageDomainModel);
                return Ok(imageDomainModel);
            }
            return BadRequest(ModelState);
        }

        private void ValidateFileUpload(ImageUploadRequestDTO imageDetails)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };
            if (!allowedExtensions.Contains(Path.GetExtension(imageDetails.File.FileName)))
            {
                ModelState.AddModelError("File", "Unsupported file extension");
            }
            if(imageDetails.File.Length > 10485760)
            {
                ModelState.AddModelError("File", "File size greater than 10MB. Upload a smaller file");
            }
        }
    }
}
