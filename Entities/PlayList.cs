namespace Nava.Entities;

public class PlayList
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public UserInfo Owner { get; set; }
    public ICollection<PlayListMusic> PlayListMusics { get; set; }
}