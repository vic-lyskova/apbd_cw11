using Cw10.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cw10.Configurations;

public class PrescriptionConfiguration : IEntityTypeConfiguration<Prescription>
{
    public void Configure(EntityTypeBuilder<Prescription> builder)
    {
        builder.ToTable("Prescription");

        builder.HasKey(e => e.IdPrescription);

        builder.HasOne(e => e.Patient)
            .WithMany(e => e.Prescriptions)
            .HasForeignKey(e => e.IdPatient);

        builder.HasOne(e => e.Doctor)
            .WithMany(e => e.Prescriptions)
            .HasForeignKey(e => e.IdDoctor);

        builder.HasData(new List<Prescription>()
        {
            new()
            {
                IdPrescription = 1,
                Date = new DateTime(2024, 10, 12),
                DueDate = new DateTime(2024, 6, 8),
                IdPatient = 1,
                IdDoctor = 2
            },
            new()
            {
                IdPrescription = 2,
                Date = new DateTime(2004, 12, 1),
                DueDate = new DateTime(2030, 12, 1),
                IdPatient = 2,
                IdDoctor = 1
            },
            new()
            {
                IdPrescription = 3,
                Date = new DateTime(2024, 6, 8),
                DueDate = new DateTime(2024, 6, 20),
                IdPatient = 3,
                IdDoctor = 3
            }
        });
    }
}