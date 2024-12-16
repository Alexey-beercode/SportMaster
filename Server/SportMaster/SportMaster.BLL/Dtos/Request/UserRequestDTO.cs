namespace SportMaster.BLL.Dtos.Request;

public class UserRequestDTO
{
    public string Username { get; set; }
    public string Email { get; set; }
    public int Age { get; set; }
    public decimal Height { get; set; }
    public decimal Weight { get; set; }
    public string Gender { get; set; }
    public string Password { get; set; }
}
