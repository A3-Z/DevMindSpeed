using DevMindSpeed.BusinessLayer.Services.Abstraction;
using DevMindSpeed.BusinessLayer.Services.Implementation;
using DevMindSpeed.Common.Db.Models;
using DevMindSpeed.DataAccessLayer;
using DevMindSpeed.DataAccessLayer.Repositories.Abstraction;
using DevMindSpeed.DataAccessLayer.Repositories.Implementation;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
var provider = builder.Services.BuildServiceProvider();
var configuration = provider.GetRequiredService<IConfiguration>();


builder.Services.AddScoped<RequestUserEntity>();

builder.Services.AddHttpContextAccessor();

var vConnectionString = configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(vConnectionString, ServerVersion.AutoDetect(vConnectionString)));

using (var scope = builder.Services.BuildServiceProvider().CreateScope())
{
    var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    
}

// Add services to the container.

//Repositories
builder.Services.AddScoped<IGameRepository, GameRepository>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();

//Services
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IQuestionService, QuestionService>();


builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Use CORS
app.UseRouting().UseCors("AllowFrontend");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
