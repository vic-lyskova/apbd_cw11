namespace Cw10.DTOs;

public class GetPrescriptionDTO
{
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public ICollection<GetMedicamentDTO> Medicaments { get; set; } = new HashSet<GetMedicamentDTO>();
    public GetDoctorDTO Doctor { get; set; } = null!;
}