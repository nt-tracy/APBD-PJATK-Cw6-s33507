using C06_APBD.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using C06_APBD.DTOs;

namespace C06_APBD.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppointmentsController :ControllerBase
{
    private string _connectionString = null;

    public AppointmentsController(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Default");
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? status, [FromQuery] string? patientLastName)
    {
        var appointments = new List<AppointmentListDto>();

        await using var connection = new SqlConnection(_connectionString);
        await using var command = new SqlCommand("""
            SELECT 
                a.IdAppointment, 
                a.AppointmentDate, 
                a.Status, 
                a.Reason, 
                p.FirstName + ' ' + p.LastName AS PatientFullName, 
                p.Email AS PatientEmail
            FROM dbo.Appointments a
            JOIN dbo.Patients p ON p.IdPatient = a.IdPatient
            WHERE (@Status IS NULL OR a.Status = @Status)
              AND (@PatientLastName IS NULL OR p.LastName LIKE @PatientLastName + '%')
            ORDER BY a.AppointmentDate;
            """, connection);

        command.Parameters.AddWithValue("@Status", (object?)status ?? DBNull.Value);
        command.Parameters.AddWithValue("@PatientLastName", (object?)patientLastName ?? DBNull.Value);

        await connection.OpenAsync();
        await using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            appointments.Add(new AppointmentListDto
            {
                IdAppointment = (int)reader["IdAppointment"],
                AppointmentDate = (DateTime)reader["AppointmentDate"],
                Status = (string)reader["Status"],
                Reason = (string)reader["Reason"],
                PatientFullName = (string)reader["PatientFullName"],
                PatientEmail = (string)reader["PatientEmail"]
            });
        }

        return Ok(appointments);
    }  
    
    
    [HttpGet]
    [Route("{idAppointment:int}")]
    public async Task<IActionResult> GetAppointmentById(int idAppointment)
    {
        var appointment = new AppointmentDetailsDto();
    
        await using var connection = new SqlConnection(_connectionString);
        await using var command = new SqlCommand("""
            SELECT  a.InternalNotes, a.CreatedAt, p.Email, p.PhoneNumber, d.LicenseNumber
            
            FROM Appointments a, Patients p, Doctors D 
            WHERE a.IdDoctor = d.IdDoctor AND p.IdPatient = a.IdPatient
            AND a.IdAppointment = @idAppointment;
            """, connection);
    
    
        command.Parameters.AddWithValue("@idAppointment", idAppointment);
        await connection.OpenAsync();
        await using var reader = await command.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {

            appointment = new AppointmentDetailsDto
            {
                InternalNotes = reader.IsDBNull(0) ? null : reader.GetString(0),
                CreatedAt = (DateTime)reader.GetValue(1),
                PatientEmail = (string)reader.GetValue(2),
                PatientPhone = (string)reader.GetValue(3),
                DoctorLicenseNumber = (string)reader.GetValue(4)
            };
            return Ok(appointment);

        }

        return NotFound();

    } 
    
    
    
    
    
    
}

    
    
