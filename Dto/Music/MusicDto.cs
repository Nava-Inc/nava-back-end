namespace Nava.Dto
{
    public class MusicDto
    {
        public int ID { get; set; }
        public int ArtistId { get; set; }
        public int NumberOfPlays { set; get; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime UploadedAt { get; set; }
    }
}
