using Nava.Data;
using Nava.Dto;
using Nava.Entities;
using Nava.Interface;

namespace Nava.Repository;

public class UserInfoRepository : IUserInfoRepository
{
    private readonly DataContext _context;

    public UserInfoRepository(DataContext context)
    {
        _context = context;
    }

    public UserInfo? GetUserInfo(int id)
    {
        return _context.userInfos.FirstOrDefault(a => a.ID.Equals(id));
    }

    public UserInfo? UpdateUserInfo(int id, UpdateUserInfoDto userInfoDto)
    {
        try
        {
            var user = GetUserInfo(id);
            user.Username = userInfoDto.Username;
            user.Email = userInfoDto.Email;
            user.Password = userInfoDto.Password;
            _context.SaveChanges();
            return user;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
}