using System;
using Bookmark.Manager.Core.Payloads;
using Bookmark.Manager.Logic.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookmark.Manager.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
	public class BookmarkController : BookmarkManagerControllerBase
	{
        private readonly IBookmarkService _bookmarkService;

        public BookmarkController(IHttpContextAccessor httpContextAccessor, IBookmarkService bookmarkService) : base(httpContextAccessor)
		{
            _bookmarkService = bookmarkService;
        }

        [HttpGet("GetBookmark")]
        public async Task<IActionResult> GetBookmark([FromQuery] int bookmarkId)
        {
            try
            {
                var bookmark = await _bookmarkService.GetBookmark(UserId, bookmarkId);
                return Ok(bookmark);
            }
            catch(Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("GetBookmarks")]
        public async Task<IActionResult> GetBookmarks([FromQuery] int folderId)
        {
            try
            {
                var bookmarks = await _bookmarkService.GetFolderBookmarks(UserId, folderId);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("AddBookmark")]
        public async Task<IActionResult> AddBookmark([FromBody] EditableBookmarkPayload editableBookmark)
        {
            try
            {
                await _bookmarkService.AddBookmark(editableBookmark);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut("UpdateBookmark")]
        public async Task<IActionResult> UpdateBookmark([FromBody] EditableBookmarkPayload editableBookmark)
        {
            try
            {
                await _bookmarkService.AddBookmark(editableBookmark);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete("RemoveBookmark")]
        public async Task<IActionResult> RemoveBookmark([FromQuery] int bookmarkId)
        {
            try
            {
                await _bookmarkService.RemoveBookmark(UserId, bookmarkId);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}

