using Cw10.Context;
using Cw10.DTOs;
using Cw10.Models;
using Microsoft.EntityFrameworkCore;

namespace Cw10.Services;

public class DbService : IDbService
{
    private readonly ApbdContext _context;

    public DbService(ApbdContext context)
    {
        _context = context;
    }

    public async Task<bool> DoesPatientExist(int idPatient)
    {
        return await _context.Patients
            .AnyAsync(e => e.IdPatient == idPatient);
    }

    public async Task<ICollection<Patient>> GetPatientsInfo(int idPatient)
    {
        return await _context.Patients
            .Include(e => e.Prescriptions)
            .ThenInclude(e => e.PrescriptionMedicaments)
            .ThenInclude(e => e.Medicament)
            .Include(e => e.Prescriptions)
            .ThenInclude(e => e.Doctor)
            .Where(e => e.IdPatient == idPatient)
            .ToListAsync();
    }

    public async Task AddPatient(AddPatientDTO addPatientDto)
    {
        await _context.Patients.AddAsync(new Patient()
        {
            // IdPatient = addPatientDto.IdPatient,
            FirstName = addPatientDto.FirstName,
            LastName = addPatientDto.LastName,
            Birthdate = addPatientDto.Birthdate
        });
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DoesDoctorExist(int idDoctor)
    {
        return await _context.Doctors
            .AnyAsync(d => d.IdDoctor == idDoctor);
    }

    public async Task<bool> DoesMedicamentExist(AddMedicamentToPrescriptionDTO addMedicamentToPrescriptionDto)
    {
        return await _context.Medicaments
            .AnyAsync(e => e.IdMedicament == addMedicamentToPrescriptionDto.IdMedicament);
    }

    public async Task AddPrescription(AddPrescriptionDTO addPrescriptionDto, int idDoctor)
    {
        var prescriptionToAdd = new Prescription()
        {
            Date = addPrescriptionDto.Date,
            DueDate = addPrescriptionDto.DueDate,
            IdPatient = addPrescriptionDto.Patient.IdPatient,
            IdDoctor = idDoctor
        };
        await _context.Prescriptions.AddAsync(prescriptionToAdd);

        foreach (var medicament in addPrescriptionDto.Medicaments)
        {
            await _context.PrescriptionMedicaments.AddAsync(new PrescriptionMedicament()
            {
                IdMedicament = medicament.IdMedicament,
                Prescription = prescriptionToAdd,
                Dose = medicament.Dose,
                Details = medicament.Description
            });
        }

        await _context.SaveChangesAsync();
    }
}