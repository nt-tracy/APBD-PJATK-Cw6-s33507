using System.ComponentModel.DataAnnotations;

namespace C06_APBD.DTOs;

public class CreateAppointmentRequestDto
{
    [Required]
    public int IdPatient { get; set; }

    [Required]
    public int IdDoctor { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime AppointmentDate { get; set; }

    [Required]
    [MaxLength(250)]
    public string Reason { get; set; } = string.Empty;
}