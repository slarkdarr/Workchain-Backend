namespace IF3250_2022_24_APPTS_Backend.Entities;


using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
public class JobOpening
{
    [Key]
    public int job_id { get; set; }
    public int? company_id { get; set; } = null;
    public string? job_name { get; set; } = null;
    public DateOnly? start_recruitment_date { get; set; } = null;
    public DateOnly? end_recruitment_date { get; set; } = null;
    public string? job_type { get; set; } = null;
    public int? salary { get; set; } = null;
    public string? description { get; set; } = null;
}
