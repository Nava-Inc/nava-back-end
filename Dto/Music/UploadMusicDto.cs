namespace Nava.Dto;

public class UploadMusicDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public IFormFile File { get; set; }
}