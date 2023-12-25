using Nava.Entities;

namespace Nava.Interface
{
    public interface IMusicRepository
    {
        ICollection<Music> GetMusics();

        Music GetMusic(int id);
        bool MusicExists(int id); 
    }
}
