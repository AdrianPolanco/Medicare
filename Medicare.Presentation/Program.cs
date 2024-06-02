using Medicare.Infrastructure.Options;
using Medicare.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.Configure<InfrastuctureOptions>(builder.Configuration.GetSection("InfrastructureOptions"));
var infrastructureConfig = builder.Configuration.GetSection("InfrastructureOptions").Get<InfrastuctureOptions>();
builder.Services.AddInfrastructure(infrastructureConfig);

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Public}/{action=Index}/{id?}");

app.Run();
