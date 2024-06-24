using Cw10.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cw10.Configurations;

public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        
            builder.ToTable("Doctor");

            builder.HasKey(e => e.IdDoctor);
            builder.Property(e => e.FirstName).HasMaxLength(100);
            builder.Property(e => e.LastName).HasMaxLength(100);
            builder.Property(e => e.Email).HasMaxLength(100);

            builder.HasData(new List<Doctor>()
            {
                new() { IdDoctor = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@gmail.com" },
                new() { IdDoctor = 2, FirstName = "Ann", LastName = "Smith", Email = "ann-smith@mail" },
                new() { IdDoctor = 3, FirstName = "Jack", LastName = "Taylor", Email = "jack.t@gmail.com" }
            });
    }
}