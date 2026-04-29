namespace C06_APBD.DTOs;

public class AppointmentDetailsDto
{
    public int IdAppointment { get; set; }
    public DateTime AppointmentDate { get; set; }
    public string Status { get; set; } = string.Empty;
    public string Reason { get; set; } = string.Empty;
    public string? InternalNotes { get; set; }
    public DateTime CreatedAt { get; set; }

    // Dane Pacjenta
    public int IdPatient { get; set; }
    public string PatientFirstName { get; set; } = string.Empty;
    public string PatientLastName { get; set; } = string.Empty;
    public string PatientEmail { get; set; } = string.Empty;
    public string PatientPhone { get; set; } = string.Empty;

    // Dane Lekarza
    public int IdDoctor { get; set; }
    public string DoctorFullName { get; set; } = string.Empty;
    public string DoctorLicenseNumber { get; set; } = string.Empty;
    public string SpecializationName { get; set; } = string.Empty;
}