namespace IF3250_2022_24_APPTS_Backend.Entities;


using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
public class JobApplication
{
    [Key]
    public int application_id { get; set; }
    public int? job_id { get; set; } = null;
    public int? applicant_id { get; set; } = null;
    public DateOnly? apply_date { get; set; } = null;
    public string? requirement_link { get; set; } = null;
    public string? status { get; set; } = null;
    public string? applicant_name { get; set; } = null;
    public string? applicant_email { get; set; } = null;
    public string? applicant_telp { get; set; } = null;
    public DateOnly? interview_date { get; set; } = null;
    public TimeOnly? interview_time { get; set; } = null;
    public string? interview_link { get; set; } = null;
   
}
