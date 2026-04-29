namespace C06_APBD.DTOs;

public class AppointmentDetailsDto
{
    public string? InternalNotes { get; set; }
    public DateTime CreatedAt { get; set; }

    // Dane Pacjenta
    public string PatientEmail { get; set; } = string.Empty;
    public string PatientPhone { get; set; } = string.Empty;

    // Dane Lekarza
    public string DoctorLicenseNumber { get; set; } = string.Empty;
}