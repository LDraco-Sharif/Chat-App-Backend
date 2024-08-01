namespace Web_App.ModelDTOs
{
    public class CreateMessageDTO
    {
        public string MessageText { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public int GroupId { get; set; }
    }
}
