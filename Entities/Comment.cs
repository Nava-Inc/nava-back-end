namespace Nava.Entities;

public class Comment
{
    public int ID { get; set; }
    public string Text { get; set; }
    public DateTime CommentedAt { get; set; }
    public UserInteraction UserInteraction { get; set; }
}