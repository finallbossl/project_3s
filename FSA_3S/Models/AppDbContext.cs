using FSA_3S.Enum;
using FSA_3S.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;

namespace FSA_3S.Models
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = _configuration.GetConnectionString("Db");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserEntity>()
                .Property(u => u.Status)
                .HasConversion(new EnumToStringConverter<UserStatusEnum>());

            modelBuilder.Entity<ContractEntity>()
                .Property(u => u.ContractStatus)
                .HasConversion(new EnumToStringConverter<ContractStatusEnum>());

            modelBuilder.Entity<ContractEntity>()
    .Property(u => u.ContractType)
    .HasConversion(new EnumToStringConverter<ContractTypeEnum>());

            modelBuilder.Entity<RealEstateEntity>()
    .Property(u => u.RealEstateStatus)
    .HasConversion(new EnumToStringConverter<RealEstateStatusEnum>());

            modelBuilder.Entity<RealEstateEntity>()
    .Property(u => u.RealEstateType)
    .HasConversion(new EnumToStringConverter<RealEstateTypeEnum>());

            // Configure AppointmentEntity
        modelBuilder.Entity<AppointmentEntity>()
            .HasMany(a => a.MappingUserAppointments)
            .WithOne(m => m.Appointment)
            .HasForeignKey(m => m.AppointmentId);
           
         modelBuilder.Entity<AppointmentEntity>()
        .HasOne(a => a.Customer) // Đảm bảo AppointmentEntity có một Customer
        .WithMany(c => c.Appointments) // Customer có nhiều Appointment
        .HasForeignKey(a => a.CustomerId);

            // Configure UserEntity
            modelBuilder.Entity<UserEntity>()
            .HasMany(u => u.MappingUserAppointments)
            .WithOne(m => m.User)
            .HasForeignKey(m => m.UserId);

        // Configure MappingUserAppointmentEntity
        modelBuilder.Entity<MappingUserAppointmentEntity>()
            .HasKey(m => m.MappingUserAppointmentId);
        }

    

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<ReportEntity> Reports { get; set; }
        public DbSet<ContractEntity> Contracts { get; set; }
        public DbSet<CustomerEntity> Customers { get; set; }
        public DbSet<RealEstateEntity> RealEstates { get; set; }
        public DbSet<AppointmentEntity> Appointments { get; set; }
        public DbSet<MappingUserAppointmentEntity> MappingUserAppointments { get; set; }
        public DbSet<MappingContractCustomerEntity> MappingContractCustomers { get; set; }
        public DbSet<MappingContractClauseEntity> MappingContractClauseEntities { get; set; }

    }
}
