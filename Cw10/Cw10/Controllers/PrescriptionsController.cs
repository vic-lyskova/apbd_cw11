using Cw10.DTOs;
using Cw10.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace Cw10.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PrescriptionsController : ControllerBase
{
    private readonly IDbService _dbService;

    public PrescriptionsController(IDbService dbService)
    {
        _dbService = dbService;
    }

    [HttpPost("{idDoctor:int}")]
    public async Task<IActionResult> AddPrescription(AddPrescriptionDTO addPrescriptionDto, int idDoctor)
    {
        if (!await _dbService.DoesPatientExist(addPrescriptionDto.Patient.IdPatient))
        {
            await _dbService.AddPatient(addPrescriptionDto.Patient);
        }

        if (!await _dbService.DoesDoctorExist(idDoctor))
        {
            return NotFound($"Doctor with id {idDoctor} doesn't exist");
        }

        foreach (var medicament in addPrescriptionDto.Medicaments)
        {
            if (!await _dbService.DoesMedicamentExist(medicament))
            {
                return NotFound($"Medicament with id {medicament.IdMedicament} doesn't exist");
            }
        }

        if (addPrescriptionDto.Medicaments.Count > 10)
        {
            return BadRequest("Can't add more than 10 medicaments to the prescription");
        }

        if (addPrescriptionDto.Date > addPrescriptionDto.DueDate)
        {
            return BadRequest("Prescription ends before prescribing");
        }

        await _dbService.AddPrescription(addPrescriptionDto, idDoctor);

        return Ok();
    }
}