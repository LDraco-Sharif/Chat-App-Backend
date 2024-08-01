using Microsoft.AspNetCore.Identity;
using Web_App.Models;
using Microsoft.EntityFrameworkCore;
using Web_App.Data;
using Web_App.Services.Interfaces;
using Web_App.Services;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<WebAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("WebAppContext") ?? throw new InvalidOperationException("Connection string 'WebAppContext' not found.")));

builder.Services.AddAuthorization();
builder.Services.AddAuthentication().AddCookie(IdentityConstants.ApplicationScheme);

builder.Services.AddIdentityCore<User>()
    .AddEntityFrameworkStores<WebAppContext>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IGroupService, GroupService>();
builder.Services.AddTransient<IMessageService, MessageService>();

builder.Services.AddCors(o => o.AddPolicy( "cors", builder =>
{
    builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
}));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("cors");
app.UseAuthorization();

app.MapControllers();

app.Run();
