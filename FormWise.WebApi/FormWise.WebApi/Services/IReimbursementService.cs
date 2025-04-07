using FormWise.WebApi.Common;
using FormWise.WebApi.Models;

namespace FormWise.WebApi.Services
{
    public interface IReimbursementService
    {
        Task<Result<Guid>> CreateReimbursementAsync(Guid userId, ReimbursementRequest model);
    }
}
