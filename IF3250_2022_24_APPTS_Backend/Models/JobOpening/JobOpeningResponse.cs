namespace IF3250_2022_24_APPTS_Backend.Models.JobOpening;
using System.ComponentModel.DataAnnotations;

public class JobOpeningResponse
{
    [Required]
    public int job_id { get; set; }
    [Required]
    public string? job_name { get; set; }
    [Required]
    public DateOnly? start_recruitment_date { get; set; }
    [Required]
    public DateOnly? end_recruitment_date { get; set; }
    [Required]
    public string? job_type { get; set; }
    [Required]
    public int? salary { get; set; }
    [Required]
    public string? description { get; set; }
    [Required]
    public int? company_id { get; set; }
    [Required]
    public string? company_name { get; set; }
    [Required]
    public string? company_city { get; set; }
    public string? company_profile_picture { get; set; }
}