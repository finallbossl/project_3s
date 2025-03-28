
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using FSA_3S.Models.Requests;
using FSA_3S.Services.Interface;
using FSA_3S.Services.Service;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FSA_3S.Controllers
{
    [Route("api/Realestate")]
    [ApiController]
    public class RealEstateController(IRealEstateService realestateservice, Cloudinary cloudinary) : ControllerBase
    {
        private readonly IRealEstateService _realestateservice = realestateservice;
        private readonly Cloudinary _cloudinary = cloudinary;

        /// <summary>
        /// API Post for RealEstate (with Image Upload to Cloudinary)
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] RealEstateRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            string imageUrl = null;
            if (request.ImagePath != null)
            {
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(request.ImagePath.FileName, request.ImagePath.OpenReadStream()),
                    Folder = "real_estate_images"
                };

                var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                imageUrl = uploadResult.SecureUrl.AbsoluteUri.ToString();
            }

            var result = await _realestateservice.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = result.RealEstateId }, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _realestateservice.GetByIdAsync(id);
            if (result == null) return NotFound();

            return Ok(result);
        }
        /// <summary>
        /// API Get All RealEstate
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _realestateservice.GetAllRealEstateAsync();
            return Ok(result);
        }
        /// <summary>
        /// API Put RealEstate
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRealEstate(int id, [FromForm] RealEstateRequest request)
        {
            var result = await _realestateservice.UpdateRealEstateAsync(id, request);
            if (result == null)
            {
                return NotFound("Không tìm thấy bất động sản cần cập nhật.");
            }
            return Ok(result);
        }
        /// <summary>
        /// API Delete RealEstate
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRealEstate(int id)
        {
            var result = await _realestateservice.DeleteRealEstateAsync(id);
            if (!result)
            {
                return NotFound("Không tìm thấy bất động sản cần xóa.");
            }
            return Ok("Xóa thành công.");
        }
    }
}
