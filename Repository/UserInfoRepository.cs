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

    public UserInfo? AuthenticateUser(string username, string password)
    {
        var user = _context.userInfos.FirstOrDefault(u => u.Username.Equals(username) && u.Password.Equals(password));
        return user;
    }

    public UserInfo? UserExists(string username)
    {
        var user = _context.userInfos.FirstOrDefault(u => u.Username.Equals(username));
        return user;
    }

    public UserInfo? CreateUser(string username, string password, string email, int accountType)
    {
        if (UserExists(username)!= null)
        {
            return null;
        }

        var user = _context.userInfos.Add(new UserInfo
        {
            Username = username,
            Password = password,
            Email = email,
            AccountType = accountType,
            CreatedAt = DateTime.Now
        });
        _context.SaveChanges();
        return user.Entity;
    }
}