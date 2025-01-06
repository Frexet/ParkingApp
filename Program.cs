var builder = WebApplication.CreateBuilder(args);

// Lägg till Controllers
builder.Services.AddControllers();

// Lägg till Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Lägg till tjänster för Dependency Injection (valfritt men rekommenderat)
builder.Services.AddSingleton<ParkingService>();
builder.Services.AddSingleton<UserService>();

var app = builder.Build();

// Använd Swagger endast i utvecklingsläge
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Routing & Controllers
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();