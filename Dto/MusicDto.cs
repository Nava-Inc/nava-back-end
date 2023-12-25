namespace Nava.Dto
{
    public class MusicDto
    {
        public int ID { get; set; }
        public int ArtistId { get; set; }
        public string Name { get; set; }
        public TimeSpan Duration { get; set; }
        public string Description { get; set; }
        public DateTime UploadedAt { get; set; }
        public int NumberOfPlays { get; set; }
    }
}
