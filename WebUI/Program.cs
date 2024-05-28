using Entities.Dtos;
using Entities.Models;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Repositories.EfCore;
using WebUI.ValidationRules.RegisterationRules;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddHttpClient();
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<RepositoryContext>();
builder.Services.AddIdentityCore<User>()
    .AddEntityFrameworkStores<RepositoryContext>();
builder.Services.AddScoped<UserManager<User>>();
builder.Services.AddTransient<IValidator<UserRegisterationDto>, RegisterationValidator>();
builder.Services.AddControllersWithViews().AddFluentValidation();
builder.Services.AddMvc();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddSession();
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

app.UseEndpoints(endpoints =>
{
    app.UseSession();
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Register}/{action=Index}"
    );

    endpoints.MapRazorPages();
});

//app.MapRazorPages();


//app.MapControllerRoute(
//    name: "default",
    
//    pattern: "{controller=Login}/{action=Index}"

//);


app.Run();
