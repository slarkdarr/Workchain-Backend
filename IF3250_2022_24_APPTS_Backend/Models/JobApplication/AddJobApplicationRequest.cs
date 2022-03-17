namespace IF3250_2022_24_APPTS_Backend.Models.JobApplication;
using System.ComponentModel.DataAnnotations;

public class AddJobApplicationRequest
{
    [Required]
    public int? job_id { get; set; }
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

}