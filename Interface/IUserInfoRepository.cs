using Nava.Dto;
using Nava.Entities;

namespace Nava.Interface;

public interface IUserInfoRepository
{
    public UserInfo? GetUserInfo(int id);
    public UserInfo? UpdateUserInfo(int id, UpdateUserInfoDto userInfoDto);
}