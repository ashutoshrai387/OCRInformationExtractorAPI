using OCRInformationExtractorAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Configure logging
builder.Logging.ClearProviders(); // Optional: clear default providers
builder.Logging.AddConsole(); // Add console logging

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Specify the path to the tessdata directory
//string tessdataPath = Path.Combine(Directory.GetCurrentDirectory(), "tessdata");
builder.Services.AddSingleton<OcrService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
