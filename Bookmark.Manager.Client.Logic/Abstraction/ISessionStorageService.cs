using System;
using Bookmark.Manager.Core.Helpers;

namespace Bookmark.Manager.Client.Logic.Abstraction
{
	public interface ISessionStorageService
	{
		Task SetData<T>(CacheKey key, T data);
		Task<T> Get<T>(CacheKey key);
	}
}

