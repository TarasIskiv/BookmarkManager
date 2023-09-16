namespace Bookmark.Manager.Core.Helpers
{
    public class SecuritySettings
    {
        public string Salt { get; set; } = default!;
        public int KeySize { get; set; }
        public int Iterations { get; set; }
    }
}