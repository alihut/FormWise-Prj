using FormWise.WebApi.Common;
using FormWise.WebApi.Domain;
using FormWise.WebApi.Domain.Entities;
using System.Linq;
using FormWise.WebApi.Models;

namespace FormWise.WebApi.Services
{
    public class ReimbursementService : IReimbursementService
    {
        private readonly MainDbContext _db;
        private readonly IFileStorageService _fileStorageService;

        public ReimbursementService(MainDbContext db, IFileStorageService fileStorageService)
        {
            _db = db;
            _fileStorageService = fileStorageService;
        }
        public async Task<Result<Guid>> CreateReimbursementAsync(Guid userId, ReimbursementRequest model)
        {
            try
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".pdf" };
                var extension = Path.GetExtension(model.Receipt.FileName).ToLowerInvariant();
                if (!allowedExtensions.Contains(extension))
                    return Result<Guid>.Fail("Only PDF, JPG, or PNG files are allowed.");

                if(model.Amount <= 0)
                    return Result<Guid>.Fail("Amount should be greater than 0");


                // Save file to disk
                var filePath = await _fileStorageService.SaveFileAsync(model.Receipt, "receipts");

                var entity = new Reimbursement
                {
                    UserId = userId,
                    Date = model.Date,
                    Amount = model.Amount,
                    Description = model.Description,
                    ReceiptFilePath = filePath
                };

                _db.Reimbursements.Add(entity);
                await _db.SaveChangesAsync();

                return Result<Guid>.Success(entity.Id, "Reimbursement submitted");
            }
            catch (Exception ex)
            {
                return Result<Guid>.Fail("An error occurred while creating reimbursement.", 500, new List<string> { ex.Message });
            }
        }
    }
}
