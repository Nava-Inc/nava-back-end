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

        public async Task<UploadMusicDto?> UploadMusic(int userId, UploadMusicDto musicDto) 
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
                music.FilePath = $"{Guid.NewGuid().ToString()}_{musicDto.File.FileName}"; // unique file name

                var directoryPath = /*_configuration["FilesPath"] ??*/ ".\\Files";
                if (Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true")
                {
                    directoryPath = "Files";
                }
                
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                var filePath = directoryPath + $"\\{music.FilePath}";
                if (Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true")
                {
                    filePath = directoryPath + $"/{music.FilePath}";
                }
                
                await using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await musicDto.File.CopyToAsync(stream);
                }

                _context.musics.Add(music);
                await _context.SaveChangesAsync();
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
            var music = _context.musics.FirstOrDefault(m => m.ID == id && !m.isDeleted);
            if (music == null)
            {
                return null;
            }

            var contentPath = ( /*_configuration["FilesPath"] ??*/ ".\\Files") + "\\" + music.FilePath;
            if (Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true")
            {
                contentPath = "Files/" + music.FilePath;
            }
            var musicContent = new MusicContentDto
            {
                Name = music.Name + music.FilePath[music.FilePath.LastIndexOf('.')..],
                FileContent = File.ReadAllBytes(contentPath)
            };
            
            return musicContent;
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

        public List<MusicDto>? SearchMusic(string query)
        {
            var searchResult = _mapper.Map<List<MusicDto>>(_context.musics.Where(m => m.Name.Contains(query)).ToList());
            return searchResult;
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