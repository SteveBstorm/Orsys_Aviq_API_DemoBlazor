using API_DemoBlazor.Hubs;
using API_DemoBlazor.Services;
using API_DemoBlazor.Tools;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Data.SqlClient;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<SqlConnection>(cs => new SqlConnection(
        builder.Configuration.GetConnectionString("default")));

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<JwtGenerator>();

builder.Services.AddSingleton<MovieService>();

//Microsoft.AspNetCore.Authentication.JwtBearer
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
    options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtGenerator.secretKey)),
            ValidateLifetime = true,
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidAudience = "monapp.com",
            ValidIssuer = "monapi.com"
        };
    }
    );

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("adminPolicy", policy => policy.RequireRole("Admin"));
    options.AddPolicy("connectedPolicy", policy => policy.RequireAuthenticatedUser());
});

builder.Services.AddCors(option => option.AddPolicy("signalRPolicy", options =>
{
    options.WithOrigins("http://localhost:4200").AllowCredentials().AllowAnyHeader()
        .AllowAnyMethod();
}));

builder.Services.AddSignalR();

builder.Services.AddSingleton<MovieHub>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseCors(o=> o.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
app.UseCors("signalRPolicy");
//Dans ce sens 
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapHub<ChatHub>("chathub");
app.MapHub<MovieHub>("moviehub");

app.Run();
