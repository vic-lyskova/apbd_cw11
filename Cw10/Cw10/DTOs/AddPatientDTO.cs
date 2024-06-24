using System.ComponentModel.DataAnnotations;

namespace Cw10.DTOs;

public class AddPatientDTO
{
    public int IdPatient { get; set; }
    [MaxLength(100)]
    public string FirstName { get; set; } = string.Empty;
    [MaxLength(100)]
    public string LastName { get; set; } = string.Empty;
    public DateTime Birthdate { get; set; }
}