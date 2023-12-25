using Nava.Data;
using Nava.Entities;
using Nava.Interface;

namespace Nava.Repository
{
    public class MusicRepository : IMusicRepository
    {
        private readonly DataContext _context;

        public MusicRepository(DataContext context)
        {
            _context = context;
        }

        public Music GetMusic(int id)
        {
            return _context.musics.Where(m => m.ID == id).FirstOrDefault();
        }

        public ICollection<Music> GetMusics()
        {
            return _context.musics.OrderBy(m => m.ID).ToList();
        }

        public bool MusicExists(int id)
        {
            return _context.musics.Any(m => m.ID == id);
        }
    }
}
