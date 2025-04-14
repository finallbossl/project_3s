using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FSA_3S.Models;

namespace FSA_3S.Data.Configurations
{
    public class NotificationConfiguration : IEntityTypeConfiguration<NotificationEntity>
    {
        public void Configure(EntityTypeBuilder<NotificationEntity> builder)
        {
            builder.ToTable("Notifications");

            builder.HasKey(n => n.NotificationId);

            builder.Property(n => n.Title)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(n => n.Message)
                .IsRequired();

            builder.Property(n => n.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(n => n.IsRead)
                .HasDefaultValue(false);

            // Thiết lập quan hệ với bảng UserEntity
            builder.HasOne(n => n.Sender)
                .WithMany()  // Người gửi có thể có nhiều thông báo
                .HasForeignKey(n => n.SenderId)
                .OnDelete(DeleteBehavior.Restrict);  // Không xóa người dùng khi có thông báo liên quan

            builder.HasOne(n => n.Receiver)
                .WithMany()  // Người nhận có thể có nhiều thông báo
                .HasForeignKey(n => n.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);  // Không xóa người dùng khi có thông báo liên quan
        }
    }
}