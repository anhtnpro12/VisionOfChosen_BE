namespace VisionOfChosen_BE.DTOs.Event
{
    public class EventUpdateDto
    {
        public string? Changer { get; set; }
        public string? Service { get; set; }
        public string? Name { get; set; }
        public int Status { get; set; }
        public DateTime Time { get; set; }
    }

}
