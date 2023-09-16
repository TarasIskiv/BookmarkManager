using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookmark.Manager.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Bookmark.Manager.Database
{
    public class BookmarkManagerContext : DbContext
    {
        public BookmarkManagerContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<User> Users {get; set;}
        public DbSet<Folder> Folders {get; set;}
        public DbSet<UserBookmark> Bookmarks {get; set;}

    }
}