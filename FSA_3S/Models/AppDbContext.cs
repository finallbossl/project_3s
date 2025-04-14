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

            modelBuilder.Entity<AppointmentEntity>()
                .HasMany(a => a.MappingUserAppointments)
                .WithOne(m => m.Appointment)
                .HasForeignKey(m => m.AppointmentId)
                .OnDelete(DeleteBehavior.Cascade); 

            modelBuilder.Entity<AppointmentEntity>()
                .HasOne(a => a.Customer)
                .WithMany(c => c.Appointments)
                .HasForeignKey(a => a.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserEntity>()
                .HasMany(u => u.MappingUserAppointments)
                .WithOne(m => m.User)
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<MappingUserAppointmentEntity>()
                .HasKey(m => m.MappingUserAppointmentId);

            modelBuilder.Entity<MappingUserAppointmentEntity>()
                .HasOne(m => m.User)
                .WithMany()
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<MappingUserAppointmentEntity>()
                .HasOne(m => m.Creator)
                .WithMany()
                .HasForeignKey(m => m.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<MappingUserAppointmentEntity>()
                .HasOne(m => m.Updater)
                .WithMany()
                .HasForeignKey(m => m.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<NotificationEntity>()
                .ToTable("Notifications"); 

            modelBuilder.Entity<NotificationEntity>()
                .HasKey(n => n.NotificationId); 

            modelBuilder.Entity<NotificationEntity>()
                .Property(n => n.NotificationId)
                .HasColumnName("IdNotification"); 

            modelBuilder.Entity<NotificationEntity>()
                .Property(n => n.Title)
                .IsRequired() 
                .HasMaxLength(255); 

            modelBuilder.Entity<NotificationEntity>()
                .Property(n => n.Message)
                .IsRequired(); 

            modelBuilder.Entity<NotificationEntity>()
                .Property(n => n.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()"); 

            modelBuilder.Entity<NotificationEntity>()
                .Property(n => n.IsRead)
                .HasDefaultValue(false); 

            modelBuilder.Entity<MappingUserNotificationEntity>()
           .HasOne(m => m.Notification)  
            .WithMany()  
           .HasForeignKey(m => m.NotificationId)
           .OnDelete(DeleteBehavior.Cascade); 

            modelBuilder.Entity<MappingUserNotificationEntity>()
                .HasOne(m => m.User)  
                .WithMany()  
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }



        public DbSet<UserEntity> Users { get; set; }
        public DbSet<ReportEntity> Reports { get; set; }
        public DbSet<ContractEntity> Contracts { get; set; }
        public DbSet<ClauseEntity> Clauses { get; set; }
        public DbSet<CustomerEntity> Customers { get; set; }
        public DbSet<RealEstateEntity> RealEstates { get; set; }
        public DbSet<AppointmentEntity> Appointments { get; set; }
        public DbSet<MappingUserAppointmentEntity> MappingUserAppointments { get; set; }
        public DbSet<MappingContractCustomerEntity> MappingContractCustomers { get; set; }
        public DbSet<MappingContractClauseEntity> MappingContractClauseEntities { get; set; }
        public DbSet<AuditEntity> Audits { get; set; }
        public DbSet<WorkEntity> WorkEntity { get; set; }
        public DbSet<NotificationEntity> Notifications { get; set; }
        public DbSet<MappingUserNotificationEntity> MappingUserNotification { get; set; }
    }
}
