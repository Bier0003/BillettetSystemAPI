using BillettetSystemAPI.Interfaces;
using Microsoft.EntityFrameworkCore;


// Ensure the ModelsLibrary assembly is referenced in the project

using BillettetSystemAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Register ApplicationDbContext from the Model project
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));  // Modify as per your connection string

// Register repositories that use ApplicationDbContext
builder.Services.AddScoped<IEvent, EventRepository>();
builder.Services.AddScoped<ITicket, TicketRepository>();
builder.Services.AddScoped<ICategory, CategoryRepository>();

// Add Controllers, Swagger, etc.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
