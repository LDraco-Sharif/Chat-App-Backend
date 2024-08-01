namespace Web_App.ModelDTOs
{
    public class ShowMessageDTO: CreateMessageDTO
    {
        public int Id { get; set; } 
        public string UserName { get; set; } = string.Empty;
    }
}
