using Cw10.DTOs;
using Cw10.Models;

namespace Cw10.Services;

public interface IDbService
{
    Task<bool> DoesPatientExist(int idPatient);
    Task<ICollection<Patient>> GetPatientsInfo(int idPatient);
    Task AddPatient(AddPatientDTO addPatientDto);
    Task<bool> DoesDoctorExist(int idDoctor);
    Task<bool> DoesMedicamentExist(AddMedicamentToPrescriptionDTO addMedicamentToPrescriptionDto);
    Task AddPrescription(AddPrescriptionDTO addPrescriptionDto, int idDoctor);
}