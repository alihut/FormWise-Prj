using FormWise.WebApi.Common;
using FormWise.WebApi.Models;

namespace FormWise.WebApi.Services
{
    public interface IAuthService
    {
        Task<Result<LoginResponse>> LoginAsync(string email, string password);
    }
}
