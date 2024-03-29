﻿using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers {
    [Route("api/[controller]")]
    [ApiController]

    public class ImageUploadController : ControllerBase {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ImageUploadController(IWebHostEnvironment webHostEnvironment) {
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UploadedImage uploadedImage) {
            try {

                if (ModelState.IsValid == false) {
                    return BadRequest(ModelState);
                }
                if (uploadedImage.OldImagePath != string.Empty) {
                    if (uploadedImage.OldImagePath != "/assets//img/other/36421940-placeholder.jpg") {
                        string oldUploadedImageFileName = uploadedImage.OldImagePath.Split('/').Last();
                        System.IO.File.Delete($"{_webHostEnvironment.ContentRootPath}\\wwwroot\\uploads\\{oldUploadedImageFileName}");
                    }
                }

                string guid = Guid.NewGuid().ToString();
                string imageFileName = guid + uploadedImage.NewImageFileExtensions;

                string fullImageFileSystemPath = $"{_webHostEnvironment.ContentRootPath}\\wwwroot\\uploads\\{imageFileName}";
                FileStream fileStream = System.IO.File.Create(fullImageFileSystemPath);
                byte[] imageContentAsByteArray = Convert.FromBase64String(uploadedImage.NewImageBase64Content);
                await fileStream.WriteAsync(imageContentAsByteArray, 0, imageContentAsByteArray.Length);
                fileStream.Close();

                string relativeFilePathWithoutTrailingSlashes = $"uploads/{imageFileName}";
                return Created("Create", relativeFilePathWithoutTrailingSlashes);
            }
            catch (Exception e) {

                return StatusCode(500, $"Something went wrong on our side. Error message: {e.Message}");
            }
        }
    }
}
