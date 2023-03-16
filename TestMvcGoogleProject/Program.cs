using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using TestMvcGoogleProject.Data.DataContext;
using Microsoft.AspNetCore.Identity;
using TestMvcGoogleProject.Data.Entity;
using TestMvcGoogleProject.Data.Repository;
using TestMvcGoogleProject.Data.Repository.Interfaces;
using TestMvcGoogleProject.Services;
using TestMvcGoogleProject.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var configuration = builder.Configuration;

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

builder.Services.AddTransient<IPostRepository, PostRepository>();
builder.Services.AddTransient<IAccountRepository, AccountRepository>();
builder.Services.AddTransient<IPostService, PostService>();
builder.Services.AddTransient<IAccountService, AccountService>();

builder.Services.AddAuthentication(options => options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme)
    .AddGoogle(googleOptions => {
        googleOptions.SignInScheme = IdentityConstants.ExternalScheme;
        googleOptions.Events.OnCreatingTicket = context =>
        {
            var identity = (ClaimsIdentity)context.Principal.Identity;
            var picture = context.User.GetProperty("picture").GetString();
            identity.AddClaim(new Claim("picture", picture ?? "/images/user.png"));
            return Task.CompletedTask;
        };
    googleOptions.ClientId = configuration["Google:ClientId"];
    googleOptions.ClientSecret = configuration["Google:ClientSecret"];
});

builder.Services.AddRazorPages();
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

