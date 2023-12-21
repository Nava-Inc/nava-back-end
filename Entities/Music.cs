namespace Nava.Entities;

public class Music
{
    public int ID { get; set; }
    public int ArtistId { get; set; }
    public string Name { get; set; }
    public TimeSpan Duration { get; set; }
    public string Description { get; set; }
    public DateTime UploadedAt { get; set; }
    public int NumberOfPlays { get; set; }
    public DownloadedMusic DownloadedMusic { get; set; }
    public ICollection<PlayListMusic> PlayListMusics { get; set; }
    public ICollection<UserInteraction> UserInteractions { get; set; }
    
}