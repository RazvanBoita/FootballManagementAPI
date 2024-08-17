using System.Text;
using FluentValidation;
using FootballMgm.Api.Data;
using FootballMgm.Api.Dtos;
using FootballMgm.Api.Repositories;
using FootballMgm.Api.Repositories.Announcement;
using FootballMgm.Api.Services;
using FootballMgm.Api.Services.Announcement;
using FootballMgm.Api.Validators.FootballValidators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

ConfigureMiddleware(app);

app.Run();

void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    services.AddControllers();

    ConfigureSwagger(services);
    ConfigureDatabase(services, configuration);
    ConfigureAuthentication(services, configuration);
    ConfigureAuthorization(services);

    RegisterDependencies(services);
}

void ConfigureSwagger(IServiceCollection services)
{
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen(opt =>
    {
        opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
        opt.EnableAnnotations();
        opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "bearer"
        });

        opt.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });
    });
}

void ConfigureDatabase(IServiceCollection services, IConfiguration configuration)
{
    var connectionString = configuration.GetConnectionString("DbConnection") ??
                           throw new InvalidOperationException("Connection string not found");

    services.AddDbContext<FootballDbContext>(options =>
    {
        options.UseSqlServer(connectionString);
    });
}

void ConfigureAuthentication(IServiceCollection services, IConfiguration configuration)
{
    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]))
            };
        });
}

void ConfigureAuthorization(IServiceCollection services)
{
    services.AddAuthorization(options =>
    {
        options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
        options.AddPolicy("RequireCoachRole", policy => policy.RequireRole("Coach", "Admin"));
        options.AddPolicy("RequireFootballerRole", policy => policy.RequireRole("Footballer", "Admin"));
        options.AddPolicy("RequireBaseRole", policy => policy.RequireRole("Base"));
        options.AddPolicy("RequireAdminOrCoachRole", policy =>
            policy.RequireAssertion(context =>
                context.User.IsInRole("Admin") ||
                context.User.IsInRole("Coach")));
        options.AddPolicy("RequireFootballerOrCoachRole", policy =>
            policy.RequireAssertion(context =>
                context.User.IsInRole("Footballer") ||
                context.User.IsInRole("Coach")));
    });
}

void RegisterDependencies(IServiceCollection services)
{
    // TODO Register la Validators
    services.AddScoped<IValidator<FootballerDto>, FootballValidator>();
    services.AddScoped<IValidator<AnnouncementDto>, AnnouncementValidator>();
    services.AddScoped<IValidator<AuthDto>, AuthValidator>();
    services.AddScoped<IValidator<CoachDto>, CoachValidator>();

    // TODO Register la Services
    services.AddScoped<JwtService>();
    services.AddScoped<AuthService>();
    services.AddScoped<IPromotionService, PromotionService>();
    services.AddScoped<IRequestService, RequestService>();
    services.AddScoped<IAnnouncementService, AnnouncementService>();

    // TODO Register la Repositories
    services.AddScoped<ITeamRepository, TeamRepository>();
    services.AddScoped<IFootballerRepository, FootballerRepository>();
    services.AddScoped<ICoachRepository, CoachRepository>();
    services.AddScoped<IAppUserRepository, AppUserRepository>();
    services.AddScoped<IFootballerRequestsRepository, FootballerRequestsRepository>();
    services.AddScoped<ICoachRequestsRepository, CoachRequestsRepository>();
    services.AddScoped<IAnnouncementRepository, AnnouncementRepository>();
}

void ConfigureMiddleware(WebApplication app)
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();
    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
}
