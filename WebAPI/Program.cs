using Business.Abstract;
using Business.Concrete;
using Core.Utilities.Security.Encryption;
using Core.Utilities.Security.JWT;
using Data.Abstract;
using Data.Concrete;
using Data.Concrete.EntityFramework;
using Data.UnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Dependency resolvers
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICommercialActivityService, CommercialActivityService>();

builder.Services.AddScoped<ICustomerDal, CustomerDal>();
builder.Services.AddScoped<ICommercialActivityDal, CommercialActivityDal>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserDal, UserDal>();

builder.Services.AddScoped<IUnitofWork, UnitofWork>();
builder.Services.AddScoped<ITokenHelper, JWTHelper>();

// Add database connection string 
builder.Services.AddDbContext<PostgreSqlContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSqlCon"), action =>
    {
        //Migration dosyasýnýn konumu belirttik
        action.MigrationsAssembly("Data");
    });
});


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opts =>
{
    var tokenOptions = builder.Configuration.GetSection("TokenOption").Get<Core.Utilities.Security.JWT.TokenOptions>();
    opts.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidIssuer = tokenOptions.Issuer,
        ValidAudience = tokenOptions.Audience,
        IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey),

        ValidateIssuerSigningKey = true,
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Add UseAuthentication middleware
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
