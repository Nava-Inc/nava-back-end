namespace Nava.Dto;

public class UploadMusicDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public byte[] FileContent { get; set; }
}