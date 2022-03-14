namespace IF3250_2022_24_APPTS_Backend.Models.User;
using System.ComponentModel.DataAnnotations;

public class RegisterRequest
{
    [Required]
    public string email { get; set; }

    [Required]
    public string full_name { get; set; }

    [Required]
    public string password { get; set; }

    [Required]
    public string phone_number { get; set; }
    [Required]
    public string type { get; set; }
}