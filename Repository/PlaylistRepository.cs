using AutoMapper;
using Nava.Data;
using Nava.Dto;
using Nava.Entities;
using Nava.Interface;

namespace Nava.Repository;

public class PlaylistRepository : IPlaylistRepository
{
    private readonly DataContext _context;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;

    public PlaylistRepository(DataContext context, IConfiguration configuration, IMapper mapper)
    {
        _context = context;
        _configuration = configuration;
        _mapper = mapper;
    }

    public List<MusicDto>? GetMusicByPlaylist(int playlistId)
    {
        var playList = _context.playLists.FirstOrDefault(p => p.ID.Equals(playlistId));
        if (playList == null)
            return null;

        var musics = _context.playListMusics.Where(a => a.PlayList.Equals(playList))
            .Select(b => _mapper.Map<MusicDto>(b.Music));
        return musics.ToList();
    }

    public PlayListMusic? AddMusicToPlaylist(int playlistId, int musicId)
    {
        var playList = _context.playLists.FirstOrDefault(p => p.ID.Equals(playlistId));
        var music = _context.musics.FirstOrDefault(m => m.ID.Equals(musicId));
        if (playList == null || music == null)
            return null;

        var playlistMusic =
            _context.playListMusics.FirstOrDefault(a => a.Music.Equals(music) && a.PlayList.Equals(playList));
        if (playlistMusic == null)
        {
            playlistMusic = _context.Add(new PlayListMusic
            {
                Music = music,
                PlayList = playList
            }).Entity;
            _context.SaveChanges();
        }

        return playlistMusic;
    }

    public PlayListMusic? RemoveMusicFromPlaylist(int playlistId, int musicId)
    {
        var playListMusic =
            _context.playListMusics.FirstOrDefault(a => a.Music.ID.Equals(musicId) && a.PlayList.ID.Equals(playlistId));

        if (playListMusic == null)
        {
            return null;
        }

        _context.playListMusics.Remove(playListMusic);
        _context.SaveChanges();
        return playListMusic;
    }
}