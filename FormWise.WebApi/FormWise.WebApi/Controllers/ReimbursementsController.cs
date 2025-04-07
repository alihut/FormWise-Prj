using FormWise.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using FormWise.WebApi.Models;
using FormWise.WebApi.Common;

namespace FormWise.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReimbursementsController : ControllerBase
    {
        private readonly IReimbursementService _reimbursementService;

        public ReimbursementsController(IReimbursementService reimbursementService)
        {
            _reimbursementService = reimbursementService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ReimbursementRequest model)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
            {
                return Unauthorized(Result<string>.Fail("Invalid or missing user identity.", 401));
            }

            var result = await _reimbursementService.CreateReimbursementAsync(userId, model);
            return StatusCode(result.StatusCode, result);
        }
    }
}
