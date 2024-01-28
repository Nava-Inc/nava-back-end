using Nava.Dto;
using Nava.Entities;

namespace Nava.Interface;

public interface IUserInfoRepository
{
    public UserInfo? GetUserInfo(int id);
    public UserInfo? UpdateUserInfo(int id, UpdateUserInfoDto userInfoDto);
    public UserInfo? AuthenticateUser(string username, string password);
    public UserInfo? UserExists(string username);
    public UserInfo? CreateUser(string username, string password, string email, int accountType);
}