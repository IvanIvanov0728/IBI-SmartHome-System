using IBI_SmartHome_System.Data;
using IBI_SmartHome_System.Service;
using IBI_SmartHome_System.Service.ClimateService;
using IBI_SmartHome_System.Service.DashboardService;
using IBI_SmartHome_System.Service.EnergyService;
using IBI_SmartHome_System.Service.LightingService;
using IBI_SmartHome_System.Service.SceneService;
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
		  policy.WithOrigins("http://localhost:5000", "http://localhost:5173", "http://localhost:3000") // React app's default dev server ports
				.AllowAnyHeader()
				.AllowAnyMethod()
				.AllowCredentials();
		});
});

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IBI_SmartHome_System.Data.Entity.ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
	.AddRoles<IdentityRole>()
	.AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddHttpContextAccessor();

builder.Services.ConfigureApplicationCookie(options =>
{
	// These settings allow the cookie to move between port 5173 and 7244
	options.Cookie.SameSite = SameSiteMode.None;
	options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Required for SameSite.None

	// Ensure the cookie is accessible to the request
	options.Cookie.HttpOnly = true;
	options.Cookie.Name = "SmartHome_Auth";

	options.Events.OnRedirectToLogin = context =>
	{
		if (context.Request.Path.StartsWithSegments("/api"))
		{
			context.Response.StatusCode = StatusCodes.Status401Unauthorized;
			return Task.CompletedTask;
		}
		context.Response.Redirect(context.RedirectUri);
		return Task.CompletedTask;
	};
});

builder.Services.AddControllersWithViews();

builder.Services.AddHostedService<MqttService>();

builder.Services.AddHttpClient<WeatherService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddScoped<IClimateService, ClimateService>();
builder.Services.AddScoped<ILightingService, LightingService>();
builder.Services.AddScoped<ISceneService, SceneService>();
builder.Services.AddScoped<IEnergyService, EnergyService>();
//builder.Services.AddScoped<ISecurityService, SecurityService>();
//builder.Services.AddScoped<ISettingsService, SettingsService>();

var app = builder.Build();

// Seed identity data
using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;
	try
	{
		await IBI_SmartHome_System.Data.Seeding.IdentitySeeder.SeedAsync(services);
	}
	catch (Exception ex)
	{
		var logger = services.GetRequiredService<ILogger<Program>>();
		logger.LogError(ex, "An error occurred seeding the Identity DB.");
	}
}

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Dashboard}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
