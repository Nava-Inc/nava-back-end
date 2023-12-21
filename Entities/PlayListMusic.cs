namespace Nava.Entities;

public class PlayListMusic
{
    public int MusicId { get; set; }
    public int PlayListId { get; set; }
    public PlayList PlayLists { get; set; }
    public Music Musics { get; set; }

}