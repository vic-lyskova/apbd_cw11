using Cw10.Configurations;
using Cw10.Models;
using Microsoft.EntityFrameworkCore;

namespace Cw10.Context;

public class ApbdContext : DbContext
{
    protected ApbdContext()
    {
    }

    public ApbdContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        /*modelBuilder.ApplyConfiguration(new DoctorConfiguration());
        modelBuilder.ApplyConfiguration(new PatientConfiguration());
        modelBuilder.ApplyConfiguration(new MedicamentConfiguration());
        modelBuilder.ApplyConfiguration(new PrescriptionConfiguration());
        modelBuilder.ApplyConfiguration(new PrescriptionMedicamentConfiguration());*/
        
        modelBuilder.Entity<Doctor>().HasData(new List<Doctor>()
        {
            new() { IdDoctor = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@gmail.com" },
            new() { IdDoctor = 2, FirstName = "Ann", LastName = "Smith", Email = "ann-smith@mail" },
            new() { IdDoctor = 3, FirstName = "Jack", LastName = "Taylor", Email = "jack.t@gmail.com" }
        });
        
        modelBuilder.Entity<Medicament>().HasData(new List<Medicament>()
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
        
        modelBuilder.Entity<Patient>().HasData(new List<Patient>()
        {
            new() { IdPatient = 1, FirstName = "Jillian", LastName = "Smith", Birthdate = new DateTime(1980, 03, 4) },
            new() { IdPatient = 2, FirstName = "Patric", LastName = "Summers", Birthdate = new DateTime(2000, 09, 20) },
            new() { IdPatient = 3, FirstName = "Linda", LastName = "Taylor", Birthdate = new DateTime(1990, 12, 8) }
        });
        
        modelBuilder.Entity<Prescription>().HasData(new List<Prescription>()
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
        
        modelBuilder.Entity<PrescriptionMedicament>().HasData(new List<PrescriptionMedicament>()
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

        modelBuilder.Entity<User>().HasData(new List<User>()
        {
            new()
            {
                Login = "test",
                Password = "test",
                RefreshToken = "test",
                RefreshTokenExp = DateTime.Now,
                Salt = "123"
            }
        });
    }
}