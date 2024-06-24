namespace Cw10.DTOs;

public class AddMedicamentToPrescriptionDTO
{
    public int IdMedicament { get; set; }
    public int? Dose { get; set; }
    public string Description { get; set; } = string.Empty;
}