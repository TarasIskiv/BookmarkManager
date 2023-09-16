using Bookmark.Manager.Logic.Abstraction;
using Bookmark.Manager.Logic.Implementation;
using Bookmark.Manager.Database;
using Bookmark.Manager.Repository.Abstraction;
using Microsoft.EntityFrameworkCore;
using Bookmark.Manager.Repository.Implementation;
using Bookmark.Manager.Core.Helpers;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<BookmarkManagerContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("BookmarkDatabase")));

var a = builder.Configuration.GetSection("SecuritySettings");
//Services
builder.Services.AddScoped<IBookmarkService, BookmarkService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IFolderService, FolderService>();
builder.Services.AddScoped<ICacheService, CacheService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IEncryptionService, EncryptionService>(
    x => new EncryptionService(new SecuritySettings()));

//Repositories
builder.Services.AddScoped<IBookmarkRepository, BookmarkRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IFolderRepository, FolderRepository>();

var app = builder.Build();

app.UseCors(cors => cors
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials()
);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseBlazorFrameworkFiles();
app.MapFallbackToFile("index.html");
app.UseStaticFiles();
app.UseHttpsRedirection();

app.MapControllers();

app.Run();
