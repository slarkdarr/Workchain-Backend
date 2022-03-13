namespace IF3250_2022_24_APPTS_Backend.Models.User;
public class AuthenticateResponse
{
    public int applicant_id { get; set; }
    public string email { get; set; }
    public string applicant_name { get; set; }
    public string Token { get; set; }
}