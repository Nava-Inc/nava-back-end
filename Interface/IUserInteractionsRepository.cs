namespace Nava.Interface;

public interface IUserInteractionsRepository
{
    public bool ToggleLike(int userId, int musicId);
    public bool PostComment(int userId, int musicId, string text);
}