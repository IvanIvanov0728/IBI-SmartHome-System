using IBI_SmartHome_System.Data;
using IBI_SmartHome_System.Service.Hubs;
using IBI_SmartHome_System.Service.AdminService;
using IBI_SmartHome_System.Service.ClimateService;
using IBI_SmartHome_System.Service.DashboardService;
using IBI_SmartHome_System.Service.EnergyService;
using IBI_SmartHome_System.Service.LightingService;
using IBI_SmartHome_System.Service.MqttService;
using IBI_SmartHome_System.Service.SceneService;
using IBI_SmartHome_System.Service.SecurityService;
using IBI_SmartHome_System.Service.SettingsService;
using IBI_SmartHome_System.Service.Weather;
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
			policy.WithOrigins("https://ibi-smarthome-system-frontend-production.up.railway.app")
				  .AllowAnyHeader()
				  .AllowAnyMethod()
				  .AllowCredentials();
		});
});

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IBI_SmartHome_System.Data.Entity.ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
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

builder.Services.AddControllers();

builder.Services.AddHostedService<MqttService>();
builder.Services.AddSignalR();
builder.Services.AddHttpClient<WeatherService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddScoped<IClimateService, ClimateService>();
builder.Services.AddScoped<ILightingService, LightingService>();
builder.Services.AddScoped<ISceneService, SceneService>();
builder.Services.AddScoped<IEnergyService, EnergyService>();
builder.Services.AddScoped<ISecurityService, SecurityService>();
builder.Services.AddScoped<ISettingsService, SettingsService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IMqttMessageHandler, MqttMessageHandler>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

// Enable Swagger for everyone so we can test on Railway
app.UseSwagger();
app.UseSwaggerUI(c =>
{
	c.SwaggerEndpoint("/swagger/v1/swagger.json", "IBI SmartHome API V1");
	c.RoutePrefix = "swagger"; // This makes it live at /swagger
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseMigrationsEndPoint();
}
else
{
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<SmartHomeHub>("/smartHomeHub");

app.Run();
