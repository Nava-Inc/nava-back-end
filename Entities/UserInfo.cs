namespace Nava.Entities;

public class UserInfo
{
    public int ID { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public int AccountType { get; set; }
    public DateTime CreatedAt { get; set; }
    public ICollection<DownloadedMusic> DownloadedMusics { get; set; }
    public ICollection<PlayList> PlayList { get; set; }
    public ICollection<UserInteraction> UserInteractions { get; set; }
}