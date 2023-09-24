using System;
namespace Bookmark.Manager.Core.Helpers
{
	/*
	 Redis cache keys logic:
		AppName/UserId/Folders/ParentFolderId; ParentFolderId can be null
		AppName/UserId/Bookmarks/ParentFolderId; ParentBookmarkId cant be null
	 */
	public enum RedisCacheKey
	{
		Folders,
		Bookmarks
	}
}

