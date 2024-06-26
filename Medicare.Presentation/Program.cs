using Medicare.Infrastructure.Options;
using Medicare.Infrastructure.Extensions;
using Medicare.Application.Extensions;
using Medicare.Presentation.Filters;
using Medicare.Application.Adapters;
using Medicare.Presentation.Adapters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<PublicFilter>();
builder.Services.AddScoped<SessionAuthenticationFilter>();
builder.Services.AddScoped<AdminAuthorizationFilter>();
builder.Services.AddScoped<AssistantAuthorizationFilter>();
builder.Services.AddScoped<IWebHostEnvironmentAdapter, WebHostEnvironmentAdapter>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.Configure<InfrastuctureOptions>(builder.Configuration.GetSection("InfrastructureOptions"));
var infrastructureConfig = builder.Configuration.GetSection("InfrastructureOptions").Get<InfrastuctureOptions>();
builder.Services.AddApplicationLayer();
builder.Services.AddInfrastructureLayer(infrastructureConfig);

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
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Public}/{action=Index}/{id?}");

app.Run();
