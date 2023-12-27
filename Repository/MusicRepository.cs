using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using Nava.Data;
using Nava.Dto;
using Nava.Entities;
using Nava.Interface;

namespace Nava.Repository
{
    public class MusicRepository : IMusicRepository
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public MusicRepository(DataContext context, IConfiguration configuration, IMapper mapper)
        {
            _context = context;
            _configuration = configuration;
            _mapper = mapper;
        }

        public MusicDto? GetMusicInfo(int id)
        {
            var musicInfo = _mapper.Map<MusicDto>(_context.musics.FirstOrDefault(m => m.ID == id && !m.isDeleted));

            return musicInfo ?? null;
        }

        public UploadMusicDto? UploadMusic(int userId, UploadMusicDto musicDto) // todo
        {
            try
            {
                var music = new Music();
                music.ArtistId = userId;
                music.Name = musicDto.Name;
                music.isDeleted = false;
                // music.Duration ==                           // todo
                music.Description = musicDto.Description;
                music.UploadedAt = DateTime.Now;
                music.NumberOfPlays = 0;
                music.FilePath = $"{Guid.NewGuid().ToString()}_{musicDto.Name}"; // unique file name
                File.WriteAllBytes(_configuration["FilesPath"] ?? "./Files" + music.FilePath, Convert.FromBase64String(musicDto.FileContent));

                _context.musics.Add(music);
                _context.SaveChanges();
                return musicDto;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public MusicContentDto? GetMusicContent(int id)
        {
            var musicContent =
                _mapper.Map<MusicContentDto>(_context.musics.FirstOrDefault(m => m.ID == id && !m.isDeleted));
            if (musicContent == null)
            {
                return null;
            }

            musicContent.FileContent = File.ReadAllBytes(
                "C:\\Hosain\\Uni\\TERM7\\SE\\proj2\\nava-back-end\\Files\\Sting-Shape-of-My-Heart-128.mp3"); // todo: delete
            return musicContent;
            // return music == null ? null : File.ReadAllBytes(_configuration["FilesPath"] ?? "./Files" + music.FilePath);
        }

        public MusicDto? DeleteMusic(int id)
        {
            var music = _context.musics.FirstOrDefault(m => m.ID.Equals(id));
            if (music == null)
            {
                return null;
            }

            music.isDeleted = true;
            _context.SaveChanges();
            return _mapper.Map<MusicDto>(music);
        }

        public ICollection<Music> GetMusics()
        {
            return _context.musics.OrderBy(m => m.ID).ToList();
        }

        public bool MusicExists(int id)
        {
            return _context.musics.Any(m => m.ID == id && !m.isDeleted);
        }
    }
}