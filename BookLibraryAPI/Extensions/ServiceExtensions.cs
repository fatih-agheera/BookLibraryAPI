using System.Reflection;
using System.Text;
using BookLibraryAPI.MiddlewareHandlers;
using BusinessLayer.Exceptions;
using BusinessLayer.Infrastructure.Mapper;
using BusinessLayer.Infrastructure.Validators.Book;
using BusinessLayer.Services.Contracts;
using BusinessLayer.Services.Implementations;
using DataLayer.Utils;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;

namespace BookLibraryAPI.Extensions
{
	public static class ServiceExtensions
	{
		public static void ConfigureMongoClient(this IServiceCollection services, IConfiguration config)
		{
			services.AddSingleton<IMongoClient>(sp =>
			{
				var configuration = sp.GetRequiredService<IOptions<MongoDbConfiguration>>().Value;
				var settings = MongoClientSettings.FromConnectionString(configuration.ConnectionURI);
				return new MongoClient(settings);
			});
		}

		public static void ConfigureAutoMapper(this IServiceCollection services)
		{
			services.AddAutoMapper(typeof(MappingProfile));
		}

		public static void ConfigureFluentValidation(this IServiceCollection services)
		{
			services.AddFluentValidationAutoValidation();
			services.AddValidatorsFromAssembly(typeof(CreateBookDtoValidator).Assembly); // Register all validators in the assembly
		}

		public static void ConfigureBusinessServices(this IServiceCollection services)
		{
			services.AddScoped<IBookService, BookService>();
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<ITokenService, JwtTokenService>();
			services.AddScoped<IPasswordService, PasswordService>();
		}

		public static void AppendGlobalErrorHandler(this IApplicationBuilder builder)
		{
			builder.UseMiddleware<GlobalErrorHandler>();
		}

		public static void ConfigureJwtAuthentication(this IServiceCollection services, IConfiguration config)
		{
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			.AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidIssuer = config["JwtSettings:Issuer"] ?? throw new ConfigurationKeyNotFoundException("Issuer key is null!"),
					ValidateAudience = true,
					ValidAudience = config["JwtSettings:Audience"] ?? throw new ConfigurationKeyNotFoundException("Audience key is null!"),
					ValidateLifetime = true,
					IssuerSigningKey = new SymmetricSecurityKey(
						Encoding.UTF8.GetBytes(
							config["JwtSettings:Key"] ?? throw new ConfigurationKeyNotFoundException("JWT key is null!")
						)
					),
					ValidateIssuerSigningKey = true,
				};
			});
		}

		public static void ConfigureSwagger(this IServiceCollection services)
		{
			services.AddSwaggerGen(options =>
			{
				options.SwaggerDoc("v1", new OpenApiInfo
				{
					Title = "BookLibrary API",
					Version = "v1",
					Description = "BookLibrary API with JWT Authentication"
				});

				options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					Description = "JWT Authorization header using the Bearer scheme.",
					Name = "Authorization",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.Http,
					Scheme = "Bearer"
				});

				options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
						new string[] { }
					}
				});

				var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
				options.IncludeXmlComments(xmlPath);
			});
		}
	}
}
