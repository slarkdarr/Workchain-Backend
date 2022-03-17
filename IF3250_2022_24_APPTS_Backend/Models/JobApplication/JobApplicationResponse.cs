namespace IF3250_2022_24_APPTS_Backend.Models.JobApplication;
using System.ComponentModel.DataAnnotations;

public class JobApplicationResponse
{
    [Required]
    public int? application_id { get; set; }
    [Required]
    public int? job_id { get; set; }
    [Required]
    public int? applicant_id { get; set; }
    [Required]
    public DateOnly? apply_date { get; set; }
    [Required]
    public string? requirement_link { get; set; }
    [Required]
    public string? status { get; set; }
    [Required]
    public string? applicant_name { get; set; }
    [Required]
    public string? applicant_email { get; set; }
    [Required]
    public string? applicant_telp { get; set; }
    [Required]
    public string? job_name { get; set; }
    [Required]
    public string? company_name { get; set; }
    [Required]
    public string? company_picture { get; set; }
    [Required]
    public string? country { get; set; }
    [Required]
    public string? city { get; set; }
}