using Cw10.Models;

namespace Cw10.DTOs;

public class GetPatientInfoDTO
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime Birthdate { get; set; }
    public ICollection<GetPrescriptionDTO> Prescriptions { get; set; } = new HashSet<GetPrescriptionDTO>();
    
}