namespace Nava.Entities;

public class DownloadedMusic
{
    public int ID { get; set; }
    public Music Music { get; set; }
    public UserInfo UserInfo { get; set; }
}