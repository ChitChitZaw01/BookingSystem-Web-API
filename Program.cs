using BookingSystem;
using BookingSystem.Data;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var key = builder.Configuration["AppSettings:SecretKey"];

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "BookingSystem",
            ValidAudience = "BookingSystemUsers",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("key"))
        };
    });

builder.Services.AddAuthorization(options =>
{
    // Default policy to require any authenticated user
    options.AddPolicy("Authenticated", policy => policy.RequireAuthenticatedUser());
});
var app = builder.Build();

//app.UseDeveloperExceptionPage();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

/* Scheduled Jobs with Hangfire  */
//public void Configure(IApplicationBuilder app, IBackgroundJobClient backgroundJobs)
//{
//    app.UseHangfireDashboard();
//    app.UseHangfireServer();

//    RecurringJob.AddOrUpdate(() => ProcessWaitlistAndRefundCredits(), Cron.Hourly);
//}
