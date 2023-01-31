using ApiBase;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Models.Auth;
using Persistence.EF;
using Persistence.MySQL;
using Repository.BaseRepository;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
#region connectionString, DbContext`s
var MSSQL_connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
var MySql_connectionString = builder.Configuration.GetConnectionString("MySqlConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<MSSQL_DbContext>(options =>
    options.UseSqlServer(MSSQL_connectionString));

//builder.Services.AddDbContext<MySQL_DbContext>(options => 
//options.UseMySql(MySql_connectionString, ServerVersion.AutoDetect(MySql_connectionString)));
#endregion

builder.Services.AddDefaultIdentity<Users>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<MSSQL_DbContext>();


builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));


#region JWT Authentication settings
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
#endregion

#region Controllers, EndpointsApiExplorer 

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
#endregion

#region Cors
var corsPolicy = "corsPolicy";
builder.Services.AddCors(op =>
{
    op.AddPolicy(name: corsPolicy,
            policy => {
                policy.WithOrigins("*");
                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
                policy.AllowAnyOrigin();
            });
});
#endregion

#region Swagger
builder.Services.AddSwaggerGen(opt =>
{
    opt.OperationFilter<HeaderFilter>();
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header. \r\n\r\n Enter the token in the text input below."
    });
});
#endregion

#region app build
var app = builder.Build();
#endregion

#region Configure the HTTP request pipeline, Environments
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
#endregion

#region app settings
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseCors(corsPolicy);
app.ConfigureExceptionHandler(); // errorHandler extension 
app.Run();
#endregion
