using Cw10.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cw10.Configurations;

public class PrescriptionMedicamentConfiguration : IEntityTypeConfiguration<PrescriptionMedicament>
{
    public void Configure(EntityTypeBuilder<PrescriptionMedicament> builder)
    {
        builder.ToTable("Prescription_Medicament");

        builder.HasKey(e => new { e.IdMedicament, e.IdPrescription });
        // builder.Property(e => e.Dose).IsOptional();

        builder.HasOne(e => e.Medicament)
            .WithMany(e => e.PrescriptionMedicaments)
            .HasForeignKey(e => e.IdMedicament);

        builder.HasOne(e => e.Prescription)
            .WithMany(e => e.PrescriptionMedicaments)
            .HasForeignKey(e => e.IdPrescription);

        builder.HasData(new List<PrescriptionMedicament>()
        {
            new()
            {
                IdMedicament = 1,
                IdPrescription = 1,
                Details = "Pain"
            },
            new()
            {
                IdMedicament = 3,
                IdPrescription = 2,
                Dose = 10,
                Details = "Diabetes"
            },
            new()
            {
                IdMedicament = 2,
                IdPrescription = 3,
                Dose = 3,
                Details = "Bacteria"
            }
        });
    }
}