using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Showlio.api.Data;
using Showlio.api.Dtos;
using Showlio.api.Filters;
using Showlio.api.Identity;
using Showlio.api.Interfaces.IRepositories;
using Showlio.api.Interfaces.IService;
using Showlio.api.Interfaces.IServices;
using Showlio.api.Repositories;
using Showlio.api.Services;
using Showlio.api.validators.Experience;
using Showlio.api.validators.Portfolio;
using Showlio.api.validators.Project;
using Showlio.api.validators.Skill;
using System.Text;


var builder = WebApplication.CreateBuilder(args);


// Add connection string
builder.Services.AddDbContext<ApplicationDbContext>( options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Identity registration
builder.Services
    .AddIdentity<AppUser, IdentityRole<Guid>>()
    .AddEntityFrameworkStores<ApplicationDbContext>();


builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidAudience = builder.Configuration["JWT:Audience"],

            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    builder.Configuration["JWT:Secret"]!))
        };
    });

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddScoped<IPortfolioRepository, PortfolioRepository>();
builder.Services.AddScoped<IPortfolioService, PortfolioService>();
builder.Services.AddValidatorsFromAssemblyContaining<CreatePortfolioDtoValidator>();
builder.Services.AddScoped<ISkillService, SkillService>();
builder.Services.AddScoped<ISkillRepository, SkillRepository>();
builder.Services.AddScoped<ISkillService, SkillService>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateSkillDtoValidator>();
builder.Services.AddScoped<IPortfolioAuthorizationService,PortfolioAuthorizationService>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateProjectDtoValidator>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IExperienceRepository, ExperienceRepository>();
builder.Services.AddScoped<IExperienceService, ExperienceService>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateExperienceDtoValidator>();



//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter your JWT token."
    });

    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("Bearer", document)] = new List<string>()
    });

    options.OrderActionsBy(apiDesc =>
    {
        // Controls order of operations WITHIN a tag,
        // and (indirectly) the order of tags themselves
        var controllerOrder = new Dictionary<string, int>
        {
            ["Account"] = 1,
            ["Portfolio"] = 2,
            ["Skill"] = 3,
            ["Experience"] = 4,
            ["Project"] = 5,
        };

        var controller = apiDesc.ActionDescriptor.RouteValues["controller"] ?? "";
        var order = controllerOrder.TryGetValue(controller, out var o) ? o : 999;
        return $"{order:D4}_{apiDesc.RelativePath}";
    });
});

var app = builder.Build();

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