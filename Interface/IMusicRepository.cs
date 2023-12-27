using Nava.Dto;
using Nava.Entities;

namespace Nava.Interface
{
    public interface IMusicRepository
    {
        ICollection<Music> GetMusics();

        bool MusicExists(int id);
        MusicDto? GetMusicInfo(int id);
        UploadMusicDto? UploadMusic(int userId, UploadMusicDto musicDto);
        MusicContentDto? GetMusicContent(int id);
        MusicDto? DeleteMusic(int id);
    }
}
