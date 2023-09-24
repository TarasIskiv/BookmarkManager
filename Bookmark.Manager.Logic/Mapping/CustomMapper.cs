using Bookmark.Manager.Core.Models;
using Bookmark.Manager.Core.Payloads;

namespace Bookmark.Manager.Logic.Mapping
{
    public static class CustomMapper
    {
        public static User ToUser(this UserSignUpPayload userSignUp)
        {
            return new User()
            {
                Email = userSignUp.Email,
                Password = userSignUp.Password
            };
        }

        public static Folder ToFolder(this EditableFolderPayload editableFolder)
        {
            return new Folder()
            {
                Name = editableFolder.Name,
                ParentFolderId = editableFolder.ParentFolderId,
                UserId = editableFolder.UserId
            };
        }

        public static UserBookmark ToBookmark(this EditableBookmarkPayload editableBookmark)
        {
            return new UserBookmark()
            {
                FolderId = editableBookmark.FolderId,
                Name = editableBookmark.Name,
                UserId = editableBookmark.UserId,
                URL = editableBookmark.URL,
                Color = editableBookmark.Color
            };
        }
    }
}