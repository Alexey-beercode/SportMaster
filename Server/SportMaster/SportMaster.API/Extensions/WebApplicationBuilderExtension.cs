using System.Text;
using AuthService.BLL.Interfaces.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SportMaster.API.Validators;
using SportMaster.BLL.Interfaces.Services;
using SportMaster.BLL.Mappers;
using SportMaster.BLL.Services;
using SportMaster.DAL.Config;
using SportMaster.DAL.Infrastructure;
using SportMaster.DAL.Interfaces;
using SportMaster.DAL.Interfaces.Repositories;
using SportMaster.DAL.Repositories;

namespace SportMaster.API.Extensions;

public static class WebApplicationBuilderExtension
{
    public static void AddSwaggerDocumentation(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition(
                "Bearer",
                new OpenApiSecurityScheme
                {
                    Description = @"Enter JWT Token please.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                }
            );
            options.AddSecurityRequirement(
                new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                        },
                        new List<string>()
                    }
                }
            );
        });
    }

    public static void AddMapping(this WebApplicationBuilder builder)
    {
        builder.Services.AddAutoMapper(
            typeof(UserProfile).Assembly,
            typeof(ActionHistoryProfile).Assembly,
            typeof(CustomGoalProfile).Assembly,
            typeof(ExerciseLogProfile).Assembly,
            typeof(FoodLogProfile).Assembly,
            typeof(GoalProfile).Assembly,
            typeof(NotificationProfile).Assembly,
            typeof(ProgressProfile).Assembly,
            typeof(WaterLogProfile).Assembly,
            typeof(StepLogProfile).Assembly
        );
    }
    public static void AddDatabase(this WebApplicationBuilder builder)
    {
        string? connectionString = builder.Configuration.GetConnectionString("ConnectionString");

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        });

        builder.Services.AddScoped<ApplicationDbContext>();
    }


    public static void AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IAuthService, BLL.Services.AuthService>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IFoodLogRepository, FoodLogRepository>();
        builder.Services.AddScoped<IExerciseLogRepository, ExerciseLogRepository>();
        builder.Services.AddScoped<IGoalRepository, GoalRepository>();
        builder.Services.AddScoped<IProgressRepository, ProgressRepository>();
        builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
        builder.Services.AddScoped<IActionHistoryRepository, ActionHistoryRepository>();
        builder.Services.AddScoped<IPersonalDataRepository, PersonalDataRepository>();
        builder.Services.AddScoped<IRecommendationRepository, RecommendationRepository>();
        builder.Services.AddScoped<ICustomGoalRepository, CustomGoalRepository>();
        builder.Services.AddScoped<IWaterLogRepository, WaterLogRepository>();
        builder.Services.AddScoped<IStepLogRepository, StepLogRepository>();
        builder.Services.AddScoped<ITokenService, TokenService>();

        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IFoodService, FoodService>();
        builder.Services.AddScoped<IExerciseService, ExerciseService>();
        builder.Services.AddScoped<IGoalService, GoalService>();
        builder.Services.AddScoped<INotificationService, NotificationService>();
        builder.Services.AddScoped<IActionHistoryService, ActionHistoryService>();
        builder.Services.AddScoped<IRecommendationService, RecommendationService>();
        builder.Services.AddScoped<IWaterService, WaterService>();
        builder.Services.AddScoped<IStepService, StepService>();
        builder.Services.AddScoped<ICalorieService, CalorieService>();

        builder.Services.AddControllers();
    }

    public static void AddValidation(this WebApplicationBuilder builder)
    {
        builder
            .Services.AddFluentValidationAutoValidation()
            .AddFluentValidationClientsideAdapters();

        builder.Services.AddValidatorsFromAssemblyContaining<LoginDTOValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<ActionHistoryRequestDTOValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<CreateCustomGoalRequestDTOValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<CreateGoalRequestDTOValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<ExerciseLogRequestDTOValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<FoodLogRequestDTOValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<GoalRequestDTOValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<NotificationRequestDTOValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<ProgressRequestDTOValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<RecommendationRequestDTOValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<UpdateUserRequestDTOValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<UserRequestDTOValidator>();
    }

    public static void AddIdentity(this WebApplicationBuilder builder)
    {
        var jwtSettings = builder.Configuration.GetSection("Jwt");

        var key = Encoding.UTF8.GetBytes(jwtSettings["Secret"]);
        var issuer = jwtSettings["Issuer"];
        var audience = jwtSettings["Audience"];

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = issuer,
                ValidAudience = audience,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                LogValidationExceptions = true
            };
        });

        builder.Services.AddAuthorization(options =>
        {
            options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .Build();
        });
    }
}
