namespace IF3250_2022_24_APPTS_Backend.Models.User;
public class UpdateRequest
{
    public string full_name { get; set; }
    public string? profile_picture { get; set; }
    public DateOnly? birthdate { get; set; }
    public string? phone_number { get; set; }
    public string? gender { get; set; }
    public string? country { get; set; }
    public string? city { get; set; }
    public string? headline { get; set; }
    public string? description { get; set; }
    public string? status { get; set; }
    public string? type { get; set; }
}