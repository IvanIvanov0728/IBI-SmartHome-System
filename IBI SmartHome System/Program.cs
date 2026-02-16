using IBI_SmartHome_System.Data;
using IBI_SmartHome_System.Service;
using IBI_SmartHome_System.Service.ClimateService;
using IBI_SmartHome_System.Service.DashboardService;
using IBI_SmartHome_System.Service.LightingService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Define a CORS policy name
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add CORS service
builder.Services.AddCors(options =>
{
	options.AddPolicy(name: MyAllowSpecificOrigins,
					  policy =>
					  {
						  policy.WithOrigins("http://localhost:5000") // React app's default dev server port
								.AllowAnyHeader()
								.AllowAnyMethod();
					  });
});

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddHostedService<MqttService>();

builder.Services.AddHttpClient<WeatherService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddScoped<IClimateService, ClimateService>();
builder.Services.AddScoped<ILightingService, LightingService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCors(MyAllowSpecificOrigins); // Use the defined CORS policy

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
