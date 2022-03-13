namespace IF3250_2022_24_APPTS_Backend.Entities;

using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;


public class Applicant
{
    [Key]
    public int applicant_id { get; set; }
    public string applicant_name { get; set; } = string.Empty;
    public string email { get; set; } = string.Empty;
    public string? profile_picture { get; set; } = string.Empty;
    public DateOnly? birthdate { get; set; } = null;
    public string? phone_number { get; set; } = string.Empty;
    public string? gender { get; set; } = string.Empty;
    public string? country { get; set; } = string.Empty;
    public string? city { get; set; } = string.Empty;
    public string? headline { get; set; } = string.Empty;
    public string? description { get; set; } = string.Empty;
    public string? status { get; set; } = string.Empty;

    [JsonIgnore]
    public string password { get; set; } = string.Empty;
}
