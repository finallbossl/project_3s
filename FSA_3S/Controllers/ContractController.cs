using FSA_3S.Models.Requests;
using FSA_3S.Models.Respone;
using FSA_3S.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FSA_3S.Controllers
{
    [Route("api/contracts")]
    [ApiController]
    public class ContractController(IContractService contractService) : ControllerBase
    {
        private readonly IContractService _contractService = contractService;
        /// <summary>
        /// API Post for Contract
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateContract([FromBody] ContractRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _contractService.CreateContractAsync(request);
            if (result == null)
                return BadRequest("Failed to create contract.");

            return CreatedAtAction(nameof(CreateContract), new { id = result.ContractId }, result);
        }
        /// <summary>
        /// API Get All Contract
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-all-contracts")]
        public async Task<IActionResult> GetAllContracts()
        {
            var contracts = await _contractService.GetAllContractsAsync();
            return Ok(contracts);
        }
        /// <summary>
        /// API Put for Contract
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContract(int id, [FromBody] ContractRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _contractService.UpdateContractAsync(id, request);
            if (result == null)
                return NotFound("Contract not found or update failed.");

            return Ok(result);
        }
        /// <summary>
        /// API Delete Contract
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns></returns>
        [HttpDelete("{contractId}")]
        public async Task<IActionResult> DeleteContract(int contractId)
        {
            var isDeleted = await _contractService.DeleteContractAsync(contractId);
            if (!isDeleted)
                return NotFound(new { Message = "Contract not found." });

            return Ok(new { Message = "Contract deleted successfully." });
        }
    }
}