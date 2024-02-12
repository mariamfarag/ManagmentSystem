using Application_UI.AutoMapper;
using Application_UI.Utility;
using BusinessLogic.BaseRepository;
using BusinessLogic.ModelRepositories.Task;
using BusinessLogic.ModelRepositories.User;
using DataAccess;
using DataAccess.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var connectionString = builder.Configuration.GetConnectionString("ManagmentSystemConnectionString");
//builder.Services.AddDbContext<ManagementSystemDBContext>(options => options.UseSqlServer(connectionString));

//=======  adding connection string
builder.Services.AddDbContext<ManagementSystemDBContext>(options =>
{
    options.UseSqlServer(connectionString);
    options.EnableSensitiveDataLogging();
});
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    // Make the session cookie essential
    options.Cookie.IsEssential = true;
});
//add identity
builder.Services.AddIdentity<Users, IdentityRole>(
     opts =>
     {
         opts.Password.RequireDigit = true;
         opts.Password.RequireLowercase = true;
         opts.Password.RequireUppercase = true;
         opts.Password.RequireNonAlphanumeric = false;
         opts.Password.RequiredLength = 7;
     })
    .AddEntityFrameworkStores<ManagementSystemDBContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<SignInManager<Users>, SignInManager<Users>>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<ITaskRepository, TaskRepository>();

builder.Services.AddSession();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddHttpContextAccessor();
builder.Services.ConfigureApplicationCookie(options => {
    options.LoginPath = builder.Configuration.GetSection("AppSettings")["LoginPath"];
});

#region Repositories
builder.Services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
IdentityHelper.CreateRolesIfNotExistAsync(builder.Services).Wait();
#endregion

builder.Services.AddAutoMapper(typeof(UserProfile));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapAreaControllerRoute(
              name: "Identity",
              areaName: "Identity",
              pattern: "Identity/{controller=Login}/{action=Index}/{id?}"
              );
app.MapAreaControllerRoute(
              name: "Identity",
              areaName: "Identity",
              pattern: "Identity/{controller=Register}/{action=Index}/{id?}"
              );
app.MapAreaControllerRoute(
              name: "User",
              areaName: "User",
              pattern: "User/{controller=ManagingTasks}/{action=Index}/{id?}"
              );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
