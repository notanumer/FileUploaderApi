using FileUploader.Infrastructure.Abstractions.Interfaces;
using FileUploader.Infrastructure.DataAccess;
using FileUploader.Server.DependencyInjection;
using FileUploader.Server.Startup;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var jwtSecret = builder.Configuration["JwtAuth:Key"] ?? throw new ArgumentNullException("JwtAuth:Key");
var jwtIssuer = builder.Configuration["JwtAuth:Issuer"] ?? throw new ArgumentNullException("JwtAuth:Issuer");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(new JwtBearerOptionsSetup(jwtIssuer, jwtSecret).Setup);

builder.Services.AddDbContext<AppDbContext>(
            new DbContextOptionsSetup(builder.Configuration.GetConnectionString("AppDatabase")!).Setup);
builder.Services.AddAsyncInitializer<DatabaseInitializer>();
builder.Services.AddScoped<IAppDbContext, AppDbContext>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(new SwaggerGenOptionsSetup().Setup);

// AutoMapper.
AutoMapperModule.Register(builder.Services);

// MediatR.
MediatRModule.Register(builder.Services);

ApplicationModule.Register(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Map("/", context =>
{
    context.Response.Redirect("/swagger");
    return Task.CompletedTask;
});

await app.InitAsync();
await app.RunAsync();
