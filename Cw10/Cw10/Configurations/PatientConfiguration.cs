using Cw10.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cw10.Configurations;

public class PatientConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.ToTable("Patient");

        builder.HasKey(e => e.IdPatient);
        builder.Property(e => e.FirstName).HasMaxLength(100);
        builder.Property(e => e.LastName).HasMaxLength(100);

        builder.HasData(new List<Patient>()
        {
            new() { IdPatient = 1, FirstName = "Jillian", LastName = "Smith", Birthdate = new DateTime(1980, 03, 4) },
            new() { IdPatient = 2, FirstName = "Patric", LastName = "Summers", Birthdate = new DateTime(2000, 09, 20) },
            new() { IdPatient = 3, FirstName = "Linda", LastName = "Taylor", Birthdate = new DateTime(1990, 12, 8) }
        });
    }
}