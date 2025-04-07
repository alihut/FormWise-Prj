using FormWise.WebApi.Configuration;
using FormWise.WebApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FormWise.WebApi.Domain
{
    public class MainDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public DbSet<Reimbursement> Reimbursements => Set<Reimbursement>();

        public DbSet<User> Users => Set<User>();

        public MainDbContext(DbContextOptions<MainDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var defaultUser = _configuration.GetSection(nameof(DefaultUser)).Get<DefaultUser>();

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = defaultUser.Id, // use known ID for testing
                Email = defaultUser.Email,
                PasswordHash = defaultUser.PasswordHash,// hashed "admin123"
                CreatedDate = DateTime.UtcNow
            });

            modelBuilder.Entity<Reimbursement>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Date).IsRequired();
                entity.Property(e => e.Amount).IsRequired();
                entity.Property(e => e.Description).IsRequired();
                entity.Property(e => e.ReceiptFilePath).IsRequired();
            });
        }

        public override int SaveChanges()
        {
            SetAuditFields();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            SetAuditFields();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
        {
            SetAuditFields();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void SetAuditFields()
        {
            var entries = ChangeTracker
                .Entries<BaseEntity>()
                .Where(e => e.State == EntityState.Added);

            foreach (var entry in entries)
            {
                if (entry.Entity.Id == Guid.Empty)
                {
                    entry.Entity.Id = Guid.NewGuid();
                }

                entry.Entity.CreatedDate = DateTime.UtcNow;
            }
        }
    }
}
