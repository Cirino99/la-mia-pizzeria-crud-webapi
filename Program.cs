using la_mia_pizzeria_static.Data;
using la_mia_pizzeria_static.Data.Repository;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("PizzeriaDbContextConnection");builder.Services.AddDbContext<PizzeriaDbContext>(options =>
    options.UseSqlServer(connectionString));builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<PizzeriaDbContext>();
// Add services to the container.
builder.Services.AddControllersWithViews();
// serve per risolvere il Circular Object References
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();  //autoload

builder.Services.AddDbContext<PizzeriaDbContext>();
//builder.Services.AddScoped<IPizzaRepository, InMemoryPizzaRepository>();
builder.Services.AddScoped<IPizzaRepository, DbPizzaRepository>();
//builder.Services.AddScoped<ICategoryRepository, InMemoryCategoryRepository>();
builder.Services.AddScoped<ICategoryRepository, DbCategoryRepository>();
//builder.Services.AddScoped<IIngredientRepository, InMemoryIngredientRepository>();
builder.Services.AddScoped<IIngredientRepository, DbIngredientRepository>();
builder.Services.AddScoped<IMessageRepository, DbMessageRepository>();
builder.Services.AddScoped<IReviewRepository, DbReviewRepository>();

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

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
