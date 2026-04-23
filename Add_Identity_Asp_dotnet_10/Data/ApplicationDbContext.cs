using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using <ProjectName>.Entities;

namespace <ProjectName>.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {

        private readonly IConfiguration _configuration;

        public ApplicationDbContext(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public DbSet<AppUser> AppUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var majorVersion = int.Parse(_configuration["Database:Major_Version"] ?? throw new ArgumentNullException("value cannot be is null"));
                var minorVersion = int.Parse(_configuration["Database:Minor_Version"] ?? throw new ArgumentNullException("value cannot be is null"));
                var patchVersion = int.Parse(_configuration["Database:Patch_Version"] ?? throw new ArgumentNullException("value cannot be is null"));

                optionsBuilder.UseMySql(_configuration.GetConnectionString("ApplicationDbContext"), new MySqlServerVersion(new Version(majorVersion, minorVersion, patchVersion)));
            }
        }
    }
}