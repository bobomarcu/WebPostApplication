using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PostApplication.Data;
using Microsoft.AspNetCore.Identity;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/");
    options.Conventions.AuthorizeFolder("/Index");
    options.Conventions.AuthorizeFolder("/PostOffices");
    options.Conventions.AllowAnonymousToPage("/PostOffices/Index");
    options.Conventions.AuthorizeFolder("/Users");
    options.Conventions.AuthorizeFolder("/Cashiers");
    options.Conventions.AuthorizeFolder("/Couriers");
    options.Conventions.AuthorizeFolder("/Packages");
});


builder.Services.AddDbContext<PostApplicationContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PostApplicationContext") ?? throw new InvalidOperationException("Connection string 'PostApplicationContext' not found.")));

builder.Services.AddDbContext<LibraryIdentityContext>(options =>

options.UseSqlServer(builder.Configuration.GetConnectionString("PostApplicationContext") ?? throw new InvalidOperationException("Connectionstring 'PostApplicationContext' not found.")));
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<LibraryIdentityContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
