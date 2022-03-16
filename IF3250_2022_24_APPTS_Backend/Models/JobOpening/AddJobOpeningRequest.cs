namespace IF3250_2022_24_APPTS_Backend.Models.JobOpening;
using System.ComponentModel.DataAnnotations;

public class AddJobOpeningRequest
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

}