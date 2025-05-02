using Microsoft.EntityFrameworkCore;
using NabooFinalProject.Models; 

var builder = WebApplication.CreateBuilder(args);

// Add DbContext for database
builder.Services.AddDbContext<UsersDbContext>(options =>
{
    options.UseSqlite("Data Source=Users.db");
});

// Add Razor Pages
builder.Services.AddRazorPages();

// Add Authentication and Authorization
builder.Services.AddAuthentication("CookieAuth")
    .AddCookie("CookieAuth", options =>
    {
        options.LoginPath = "/AdminLogin"; // Redirect to login page if not authenticated
        options.AccessDeniedPath = "/AccessDenied"; // Redirect unauthorized users to an access denied page
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin")); // Define an Admin-only policy
});

var app = builder.Build();

// Apply database migrations and seed admin user
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var dbContext = services.GetRequiredService<UsersDbContext>();
        dbContext.Database.Migrate();

        // Seed an admin user
        var adminUser = dbContext.AdminUsers.FirstOrDefault(u => u.Email == "admin@example.com");
        if (adminUser == null)
        {
            adminUser = new AdminUser
            {
                Email = "admin@example.com",
                PasswordHash = "hashed_password_here", // Replace with a hashed password
                Role = "Admin"
            };
            dbContext.AdminUsers.Add(adminUser);
            dbContext.SaveChanges();
        }
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating the database.");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Add Authentication and Authorization middleware
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();