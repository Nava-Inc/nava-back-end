using Nava.Dto;
using Nava.Entities;

namespace Nava.Interface;

public interface IPlaylistRepository
{
    List<MusicDto>? GetMusicByPlaylist(int playlistId);
    PlayListMusic? AddMusicToPlaylist(int playlistId, int musicId);
    PlayListMusic? RemoveMusicFromPlaylist(int playlistId, int musicId);
    
}