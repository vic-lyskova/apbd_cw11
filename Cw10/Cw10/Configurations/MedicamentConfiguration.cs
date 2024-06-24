using Cw10.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cw10.Configurations;

public class MedicamentConfiguration : IEntityTypeConfiguration<Medicament>
{
    public void Configure(EntityTypeBuilder<Medicament> builder)
    {
        builder.ToTable("Medicament");

        builder.HasKey(e => e.IdMedicament);
        builder.Property(e => e.Name).HasMaxLength(100);
        builder.Property(e => e.Description).HasMaxLength(100);
        builder.Property(e => e.Type).HasMaxLength(100);

        builder.HasData(new List<Medicament>()
        {
            new()
            {
                IdMedicament = 1, 
                Name = "Ibuprofen", 
                Description = "Pain and fever away",
                Type = "Anti-inflammatory drug"
            },
            new()
            {
                IdMedicament = 2, 
                Name = "Penicillin", 
                Description = "Natural product of mould", 
                Type = "Antibiotic"
            },
            new()
            {
                IdMedicament = 3, 
                Name = "Insulin",
                Description = "Regulates the metabolism of carbohydrates, fats and protein", 
                Type = "Hormone"
            }
        });
    }
}