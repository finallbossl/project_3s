using FSA_3S.Models.Entities;
using FSA_3S.Models.Requests;
using FSA_3S.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FSA_3S.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkController(IWorkService workService) : ControllerBase
    {
        private readonly IWorkService _workService = workService;

        [HttpPost]
        public async Task<IActionResult> CreateWork([FromBody] WorkRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var work = await _workService.CreateWorkAsync(request);
                return CreatedAtAction(nameof(CreateWork), new { id = work!.WorkId }, work);
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAllWorksAsync()
        {
            var works = await _workService.GetAllWorksAsync();
            return Ok(works);
        }

        [HttpPut("{workId}")]
        public async Task<IActionResult> UpdateWorkAsync(int workId, [FromBody] WorkRequest request)
        {
            var updatedWork = await _workService.UpdateWorkAsync(workId, request);
            if (updatedWork == null)
            {
                return NotFound("Work không tồn tại.");
            }
            return Ok(updatedWork);
        }

        [HttpDelete("{workId}")]
        public async Task<IActionResult> DeleteWorkAsync(int workId)
        {
            var result = await _workService.DeleteWorkAsync(workId);
            if (!result)
            {
                return NotFound(new { message = "Works is not found." });
            }

            return Ok(new { message = "Delete Successfully." });
        }
    }
}