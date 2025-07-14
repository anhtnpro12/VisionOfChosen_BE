namespace VisionOfChosen_BE.DTOs.Event
{
    public class EventCreateDto
    {
        public int UserId { get; set; }
        public string? Changer { get; set; }
        public string? Service { get; set; }
        public string? Name { get; set; }
        public int Status { get; set; }
        public DateTime Time { get; set; }
    }

}
