using Cw10.Models;

namespace Cw10.DTOs;

public class AddPrescriptionDTO
{
    public AddPatientDTO Patient { get; set; }
    public List<AddMedicamentToPrescriptionDTO> Medicaments { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
}