using Microsoft.EntityFrameworkCore;
using backend_learning.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace backend_learning.Entities.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            var utcConverter = new ValueConverter<DateTime, DateTime>(
                toDb => toDb,
                fromDb => DateTime.SpecifyKind(fromDb, DateTimeKind.Utc)
            );

            builder.Property(p => p.Email)
                .IsRequired();
                
            builder.Property(p => p.TimeOfCreation)
                .HasConversion(utcConverter);
        }
    }
}