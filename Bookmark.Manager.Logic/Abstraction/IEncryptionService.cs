namespace Bookmark.Manager.Logic.Abstraction
{
    public interface IEncryptionService
    {
        string DecryptPassword(string password);
    }
}