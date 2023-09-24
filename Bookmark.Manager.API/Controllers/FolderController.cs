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
	public class FolderController : BookmarkManagerControllerBase
	{
        private readonly IFolderService _folderService;

        public FolderController(IHttpContextAccessor httpContextAccessor, IFolderService folderService) : base(httpContextAccessor)
		{
            _folderService = folderService;
        }

		[HttpGet("GetFolders")]
		public async Task<IActionResult> GetFolders([FromQuery] int? parentFolderId = null)
		{
			try
			{
                var folders = await _folderService.GetNestedFolders(UserId, parentFolderId);
				return Ok(folders);
			}
			catch(Exception)
			{
				return BadRequest();
			}
		}

        [HttpGet("GetFolder")]
        public async Task<IActionResult> GetFolders([FromQuery] int folderId)
        {
            try
            {
                var folder = await _folderService.GetFolder(UserId, folderId);
                return Ok(folder);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("AddFolder")]
        public async Task<IActionResult> AddFolder([FromBody] EditableFolderPayload editableFolder)
        {
            try
            {
                await _folderService.CreateFolder(editableFolder);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut("UpdateFolder")]
        public async Task<IActionResult> UpdateFolder([FromQuery] int folderId, [FromBody] EditableFolderPayload editableFolder)
        {
            try
            {
                await _folderService.UpdateFolder(folderId, editableFolder);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete("RemoveFolder")]
        public async Task<IActionResult> RemoveFolder([FromQuery] int folderId)
        {
            try
            {
                await _folderService.RemoveFolder(UserId, folderId);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}

