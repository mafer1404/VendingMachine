using backend.Application;
using backend.Domain;

var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
  options.AddPolicy(name: MyAllowSpecificOrigins,
      policy =>
      {
        policy
              .WithOrigins("http://localhost:8080")
              .AllowAnyHeader()
              .AllowAnyMethod();
      });
});


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IVendingMachine, VendingMachine>();
builder.Services.AddSingleton<IChangeCalculator, ChangeCalculator>();
builder.Services.AddSingleton<IVendingMachineService, VendingMachineService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(MyAllowSpecificOrigins);

app.MapControllers();

app.Run();
