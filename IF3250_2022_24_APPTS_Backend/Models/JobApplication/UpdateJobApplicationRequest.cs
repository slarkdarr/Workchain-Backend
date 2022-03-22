using System.ComponentModel.DataAnnotations;
namespace IF3250_2022_24_APPTS_Backend.Models.JobApplication
{
    public class UpdateJobApplicationRequest
    {
        [Required]
        public int application_id { get; set; }
        public string? status { get; set; }
        public DateOnly? interview_date { get; set; }
        public TimeOnly? interview_time { get; set; }
        public string? interview_link { get; set; }
    }
}
