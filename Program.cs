var builder = WebApplication.CreateBuilder(args);

// Register controllers to handle API requests
builder.Services.AddControllers();

// Configure Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register services for Dependency Injection
builder.Services.AddSingleton<ParkingService>(); // Manages parking logic
builder.Services.AddSingleton<UserService>();    // Manages user-related operations

var app = builder.Build();

// Enable Swagger only in development mode to prevent exposure in production
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enforce HTTPS redirection for secure communication
app.UseHttpsRedirection();

// Enable authentication/authorization (if implemented)
app.UseAuthorization();

// Map API controllers to handle incoming requests
app.MapControllers();

app.Run();