namespace Web_App.ModelDTOs;

public class SignUpDTO
{
    public required UserDTO User { get; set; }
    public string GroupName { get; set; } = "";
}
