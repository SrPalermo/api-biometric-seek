using api_biometric_seek.Config.DependencyInjections;
using api_biometric_seek.Config.Settings;
using Microsoft.Extensions.Options;
using validations_biometric_seek.Config.DependencyInjections;

var builder = WebApplication.CreateBuilder(args);


builder.Services.Configure<Root>(builder.Configuration);

builder.Services.AddScoped(sp => sp.GetRequiredService<IOptions<Root>>().Value);

builder.Services.AddValidations();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
