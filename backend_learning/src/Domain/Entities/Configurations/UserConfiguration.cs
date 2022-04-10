using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace backend_learning.Domain.Entities.Configurations
{
    // The configuration of an Entity is usually done in the DbContext's "OnModelCreating" method, but this is an extra class, created
    // to not put too much code in the previously mentioned method.
    // This class is a configuration class for an entity, it configures the Table in a custom way which
    // cant be done by the "by convention" or "Data annotation" approach by using the "FluentAPI"
    public class UserConfiguration : IEntityTypeConfiguration<User>  // This interface defines the class as a configuration, it is
                                                                     // defining the "Configure" method
    {
        // In this method, every customisation of the table is done by using the "FluentAPI"
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // This is a value converter, it defines an operation which should be done upon insertion and extraction of the
            // specified time to/from the database
            var utcConverter = new ValueConverter<DateTime, DateTime>(    // The input is "DateTime" and the output is "DateTime" as well
                toDb => toDb,                                             // Change nothing when writing it TO the DB
                fromDb => DateTime.SpecifyKind(fromDb, DateTimeKind.Utc)  // Specify that the DateTime is in the UTC format when reading it FROM the DB
            );

            // Defines a relationship (This is usually done by convention, FLUENT API is just used in non conventional cases)
            builder.HasMany(p => p.Jobs)
                .WithMany(p => p.User);


            // This is a FluentAPI operation on the property "Email" which makes it requiered (foreign key not nullable)
            builder.Property(p => p.Email)
                .IsRequired();
            
            // This is a FluentAPI operation on the property "TimeOfCreation" which applies the above specified value converter on the property
            builder.Property(p => p.TimeOfCreation)
                .HasConversion(utcConverter);
        }
    }
}