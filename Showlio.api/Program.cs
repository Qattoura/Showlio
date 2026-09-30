using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Showlio.api;
using Showlio.api.Data;
using Showlio.api.Identity;
using System.Text;


var builder = WebApplication.CreateBuilder(args);


// Add connection string
builder.Services.AddDbContext<ApplicationDbContext>(options =>
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

builder.AddServiceRegistrations();


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
            ["PublicPortfolio"] = 1,
            ["Account"] = 2,
            ["Portfolio"] = 3,
            ["Skill"] = 4,
            ["Experience"] = 5,
            ["Project"] = 6,
            ["Certificate"] = 7,
            ["Education"] = 8,
            ["Service"] = 9,
            ["ContactItem"] = 10,
            ["Template"] = 11
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