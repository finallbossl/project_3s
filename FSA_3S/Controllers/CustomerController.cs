using FSA_3S.Models.Requests;
using FSA_3S.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FSA_3S.Controllers
{
    [ApiController]
    [Route("api/Customer")]
    public class CustomerController(ICustomerService customerService) : ControllerBase
    {
        private readonly ICustomerService _customerService = customerService;

        [HttpPost("Create")]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .SelectMany(x => x.Value.Errors)
                    .Select(x => x.ErrorMessage)
                    .ToList();

                return BadRequest(new
                {
                    Message = "Dữ liệu yêu cầu không hợp lệ",
                    Errors = errors
                });
            }

            try
            {
                var result = await _customerService.CreateCustomerAsync(request);
                return Ok(new
                {
                    Message = "Khách hàng đã được tạo thành công",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Message = "Không thể tạo khách hàng",
                    Error = ex.Message
                });
            }
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllCustomers()
        {
            var result = await _customerService.GetAllCustomersAsync();
            return Ok(result);
        }

        [HttpGet("GetByType/{customerType}")]
        public async Task<IActionResult> GetCustomersByType(string customerType)
        {
            var result = await _customerService.GetCustomersByTypeAsync(customerType);
            return Ok(result);
        }
    }
}
