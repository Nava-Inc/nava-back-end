namespace Nava.Entities;

public class PlayListMusic
{
    public int MusicId { get; set; }
    public int PlayListId { get; set; }
    public PlayList PlayList { get; set; }
    public Music Music { get; set; }

}