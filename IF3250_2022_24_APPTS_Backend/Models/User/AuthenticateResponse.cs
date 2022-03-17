namespace IF3250_2022_24_APPTS_Backend.Models.User;
public class AuthenticateResponse
{
    public int user_id { get; set; }
    public string email { get; set; }
    public string full_name { get; set; }
    public string Token { get; set; }
}