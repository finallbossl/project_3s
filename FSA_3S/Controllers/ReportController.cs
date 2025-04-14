﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using FSA_3S.Data;
using FSA_3S.Enum;
using FSA_3S.Models;

namespace FSA_3S.Controllers
{
    [Route("api/report")]
    [ApiController]
    public class ReportController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;

        [HttpGet("realestate/count/available")]
        public async Task<IActionResult> GetAvailableCount()
        {
            var count = await _context.RealEstates.CountAsync(r => r.RealEstateStatus == RealEstateStatusEnum.Available);
            return Ok(new { status = "Available", count });
        }

        [HttpGet("realestate/count/sold")]
        public async Task<IActionResult> GetSoldCount()
        {
            var count = await _context.RealEstates.CountAsync(r => r.RealEstateStatus == RealEstateStatusEnum.Sold);
            return Ok(new { status = "Sold", count });
        }

        [HttpGet("realestate/count/for-rent")]
        public async Task<IActionResult> GetForRentCount()
        {
            var count = await _context.RealEstates.CountAsync(r => r.RealEstateStatus == RealEstateStatusEnum.ForRent);
            return Ok(new { status = "For Rent", count });
        }

        [HttpGet("realestate/count/rented")]
        public async Task<IActionResult> GetRentedCount()
        {
            var count = await _context.RealEstates.CountAsync(r => r.RealEstateStatus == RealEstateStatusEnum.Rented);
            return Ok(new { status = "Rented", count });
        }

        [HttpGet("contract/count/active")]
        public async Task<IActionResult> GetActiveContractsCount()
        {
            var count = await _context.Contracts.CountAsync(c => c.ContractStatus == ContractStatusEnum.Active);
            return Ok(new { status = "Active", count });
        }

        [HttpGet("contract/count/completed")]
        public async Task<IActionResult> GetCompletedContractsCount()
        {
            var count = await _context.Contracts.CountAsync(c => c.ContractStatus == ContractStatusEnum.Completed);
            return Ok(new { status = "Completed", count });
        }

        [HttpGet("contract/count/canceled")]
        public async Task<IActionResult> GetCanceledContractsCount()
        {
            var count = await _context.Contracts.CountAsync(c => c.ContractStatus == ContractStatusEnum.Canceled);
            return Ok(new { status = "Canceled", count });
        }

        [HttpGet("contract/count/expired")]
        public async Task<IActionResult> GetExpiredContractsCount()
        {
            var count = await _context.Contracts.CountAsync(c => c.ContractStatus == ContractStatusEnum.Expired);
            return Ok(new { status = "Expired", count });
        }

        [HttpGet("appointment/count/finished")]
        public async Task<IActionResult> GetFinishedAppointmentsCount()
        {
            var count = await _context.Appointments.CountAsync(a => a.Status == "finish");
            return Ok(new { status = "Finish", count });
        }

        [HttpGet("appointment/count/not-finish")]
        public async Task<IActionResult> GetNotFinishedAppointmentsCount()
        {
            var count = await _context.Appointments.CountAsync(a => a.Status == "not finish");
            return Ok(new { status = "Not Finish", count });
        }
    }
}