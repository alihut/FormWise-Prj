using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FormWise.WebApi.Common;
using FormWise.WebApi.Configuration;
using FormWise.WebApi.Domain;
using FormWise.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace FormWise.WebApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly MainDbContext _db;
        private readonly JwtSettings _jwtSettings;

        public AuthService(MainDbContext db, IOptions<JwtSettings> jwtOptions)
        {
            _db = db;
            _jwtSettings = jwtOptions.Value;
        }
        public async Task<Result<LoginResponse>> LoginAsync(string email, string password)
        {
            var user = await _db.Users
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
                return Result<LoginResponse>.Fail("User not found", 404);


            if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                return Result<LoginResponse>.Fail("Invalid credentials", 401);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email)
                }),
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiresInMinutes),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Result<LoginResponse>.Success(new LoginResponse { token = tokenString });
        }
    }
}
