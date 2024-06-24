using Cw10.DTOs;
using Cw10.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cw10.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientsController : ControllerBase
{
    private readonly IDbService _dbService;

    public PatientsController(IDbService dbService)
    {
        _dbService = dbService;
    }

    [Authorize]
    [HttpGet("{idPatient:int}")]
    public async Task<IActionResult> GetPatient(int idPatient)
    {
        if (!await _dbService.DoesPatientExist(idPatient))
        {
            return NotFound($"Patient with id {idPatient} not found");
        }

        var patients = await _dbService.GetPatientsInfo(idPatient);

        return Ok(patients.Select(p => new GetPatientInfoDTO()
        {
            IdPatient = p.IdPatient,
            FirstName = p.FirstName,
            LastName = p.LastName,
            Birthdate = p.Birthdate,
            Prescriptions = p.Prescriptions
                .Select(pr => new GetPrescriptionDTO()
                {
                    IdPrescription = pr.IdPrescription,
                    Date = pr.Date,
                    DueDate = pr.DueDate,
                    Medicaments = pr.PrescriptionMedicaments
                        .Select(pm => new GetMedicamentDTO()
                        {
                            IdMedicament = pm.IdMedicament,
                            Name = pm.Medicament.Name,
                            Dose = pm.Dose,
                            Description = pm.Medicament.Description
                        }).ToList(),
                    Doctor = new GetDoctorDTO()
                    {
                        IdDoctor = pr.IdDoctor,
                        FirstName = pr.Doctor.FirstName
                    }
                })
                // .OrderBy(pr => pr.DueDate)
                .ToList()
            
        }));
    }
}