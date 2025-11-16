using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Vilcan_Andrea_Lab2.Data;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy =>
        policy.RequireRole("Admin"));
});

builder.Services.AddDbContext<Vilcan_Andrea_Lab2Context>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Vilcan_Andrea_Lab2Context")));

builder.Services.AddDbContext<LibraryIdentityContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("LibraryIdentityContextConnection")));


builder.Services
    .AddDefaultIdentity<IdentityUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false; 
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<LibraryIdentityContext>();


builder.Services.AddRazorPages(options =>
{
    
    options.Conventions.AllowAnonymousToPage("/Books/Index");
    options.Conventions.AllowAnonymousToPage("/Books/Details");
    options.Conventions.AuthorizeFolder("/Members", "AdminPolicy");
    options.Conventions.AuthorizeFolder("/Publishers", "AdminPolicy");
    options.Conventions.AuthorizeFolder("/Categories", "AdminPolicy");
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    string[] roleNames = { "Admin", "User" };

    foreach (var roleName in roleNames)
    {
        var roleExists = roleManager.RoleExistsAsync(roleName).Result;
        if (!roleExists)
        {
            roleManager.CreateAsync(new IdentityRole(roleName)).Wait();
        }
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();