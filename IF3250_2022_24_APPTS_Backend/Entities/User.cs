namespace IF3250_2022_24_APPTS_Backend.Entities;

using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;


public class User
{
    [Key]
    public int user_id { get; set; }
    public string full_name { get; set; } = string.Empty;
    public string email { get; set; } = string.Empty;
    [JsonIgnore]
    public string password { get; set; } = string.Empty;
    public string? profile_picture { get; set; } = null;
    public DateOnly? birthdate { get; set; } = null;
    public string? phone_number { get; set; } = string.Empty;
    public string? gender { get; set; } = null;
    public string? country { get; set; } = null;
    public string? city { get; set; } = null;
    public string? headline { get; set; } = null;
    public string? description { get; set; } = null;
    public string? status { get; set; } = null;
    public string? type { get; set; } = string.Empty;
}
