namespace Web_App.Models;

public class Message
{
    public int Id { get; set; }
    public string MessageText { get; set; } = string.Empty;

    public required User User { get; set; }
    public required Group Group { get; set; }

}
