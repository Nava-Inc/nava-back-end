using Nava.Entities;

namespace Nava.Dto;

public class UserInfoDto
{
    public string Username { get; set; }
    public string Email { get; set; }
    public int AccountType { get; set; }
    public ICollection<DownloadedMusic> DownloadedMusics { get; set; }
    public ICollection<PlayList> PlayList { get; set; }
}