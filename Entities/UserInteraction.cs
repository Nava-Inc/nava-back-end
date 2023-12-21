namespace Nava.Entities;

public class UserInteraction
{
    public int ID { get; set; }
    public bool IsLiked { get; set; }
    public UserInfo User { get; set; }
    public Music music { get; set; }
    public ICollection<Comment> Comments { get; set; }
}
