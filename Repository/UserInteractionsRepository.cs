using Nava.Data;
using Nava.Entities;
using Nava.Interface;

namespace Nava.Repository;

public class UserInteractionsRepository : IUserInteractionsRepository
{
    private readonly DataContext _context;

    public UserInteractionsRepository(DataContext context)
    {
        _context = context;
    }

    public bool ToggleLike(int userId, int musicId)
    {
        var user = _context.userInfos.FirstOrDefault(a => a.ID.Equals(userId));
        var music = _context.musics.FirstOrDefault(a => a.ID.Equals(musicId));
        if (user == null || music == null)
            return false;

        var interaction = _context.userInteractions.FirstOrDefault(a => a.User.Equals(user) && a.music.Equals(music));
        if (interaction == null)
        {
            _context.userInteractions.Add(new UserInteraction
            {
                User = user,
                music = music,
                IsLiked = true
            });
        }
        else
        {
            interaction.IsLiked = !interaction.IsLiked;
        }

        _context.SaveChanges();
        return true;
    }

    public bool PostComment(int userId, int musicId, string text)
    {
        try
        {
            var user = _context.userInfos.FirstOrDefault(a => a.ID.Equals(userId));
            var music = _context.musics.FirstOrDefault(a => a.ID.Equals(musicId));
            if (user == null || music == null)
                return false;

            var interaction = _context.userInteractions.FirstOrDefault(a => a.User.Equals(user) && a.music.Equals(music));
            if (interaction == null)
            {
                interaction = _context.userInteractions.Add(new UserInteraction
                {
                    User = user,
                    music = music,
                    IsLiked = false
                }).Entity;
            }

            _context.comments.Add(new Comment
            {
                Text = text,
                CommentedAt = DateTime.Now,
                UserInteraction = interaction
            });
            _context.SaveChanges();

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }
}