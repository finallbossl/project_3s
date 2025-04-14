using FSA_3S.DTOs;
using FSA_3S.Models.Entities;
using FSA_3S.Services.Interface;
using FSA_3S.Services.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics.Contracts;

namespace FSA_3S.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditController(IAuditService auditService, IContractService contractService) : ControllerBase
    {
        private readonly IAuditService _auditService = auditService;
        private readonly IContractService _contractService = contractService;
        [HttpGet]
        public async Task<IActionResult> GetAuditInfo()
        {
            var auditInfo = await _auditService.GetAuditInfoAsync();
            return Ok(auditInfo);
        }

        [HttpGet("deleted-contracts")]
        public async Task<IActionResult> GetDeletedContracts()
        {
            var deletedContracts = await _contractService.GetDeletedContractsAsync();
            return Ok(deletedContracts);
        }

    }
}