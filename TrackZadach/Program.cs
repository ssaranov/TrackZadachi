using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using StudyNoteProject.Services;
using TrackZadach.Data;
using TrackZadach.Middleware;
using TrackZadach.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=app.db"));

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(option =>
{
    option.IdleTimeout = TimeSpan.FromMinutes(30);
    option.Cookie.HttpOnly = true;
    option.Cookie.IsEssential = true;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddScoped<ITaskService, TaskService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseRouting();

app.UseSwagger();
app.UseSwaggerUI();


app.UseSession();
app.UseMiddleware<RequestLoggingMiddleware>();
app.UseMiddleware<AuthRedirectMiddleware>();

app.MapControllerRoute
    (
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
app.UseAuthorization();

app.MapControllers();

app.MapStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.Run();