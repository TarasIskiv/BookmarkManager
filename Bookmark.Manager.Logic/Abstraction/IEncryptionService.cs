namespace Bookmark.Manager.Logic.Abstraction
{
    public interface IEncryptionService
    {
         Task<string> EncryptPassword(string password);
    }
}