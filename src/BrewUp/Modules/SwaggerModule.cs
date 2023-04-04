using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace Brewup.Modules;

public sealed class SwaggerModule : IModule
{
	public bool IsEnabled => true;
	public int Order => 0;

	public const string BearerId = "Bearer";

	public void RegisterModule(WebApplicationBuilder builder)
	{
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen(SetSwaggerGenOptions);
	}

	public void MapEndpoints(IEndpointRouteBuilder endpoints)
	{
		// no endpoints
	}

	#region Helpers
	private void SetSwaggerGenOptions(SwaggerGenOptions options)
	{
		options.OperationFilter<SecurityRequirementsOperationFilter>();
		options.SwaggerDoc("v1", new OpenApiInfo
		{
			Title = "BrewUp Api",
			Version = "v1"
		});
		options.AddSecurityDefinition(BearerId, new OpenApiSecurityScheme
		{
			Type = SecuritySchemeType.Http,
			In = ParameterLocation.Header,
			Name = "Authorization",
			Scheme = JwtBearerDefaults.AuthenticationScheme,
			BearerFormat = "JWT",
			Description = "Please enter a valid token"
		});

		ConfigureXmlComments(options);
	}

	private void ConfigureXmlComments(SwaggerGenOptions options)
	{
		var xmlFile = Path.Combine(
			Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!,
			$"{GetType().Assembly.GetName().Name}.xml");

		// Tells swagger to pick up the output XML document file
		if (!File.Exists(xmlFile))
			return;

		var currentAssembly = Assembly.GetExecutingAssembly();
		options.IncludeXmlComments(xmlFile);

		// Collect all referenced projects output XML document file paths
		var xmlDocs = currentAssembly.GetReferencedAssemblies()
			.Union(new[] { currentAssembly.GetName() })
			.Select(a => Path.Combine(Path.GetDirectoryName(currentAssembly.Location)!, $"{a.Name}.xml"))
			.Where(File.Exists).ToArray();

		Array.ForEach(xmlDocs, (d) => { options.IncludeXmlComments(d); });
	}
	#endregion
}

// ReSharper disable once ClassNeverInstantiated.Global
internal class SecurityRequirementsOperationFilter : IOperationFilter
{
	public void Apply(OpenApiOperation operation, OperationFilterContext context)
	{
		var requiredScopes = context.MethodInfo
			.GetCustomAttributes(true)
			.OfType<AuthorizeAttribute>()
			.Select(attr => attr.AuthenticationSchemes)
			.Distinct()
			.ToArray();

		var requireAuth = false;
		var id = string.Empty;

		if (requiredScopes.Contains(JwtBearerDefaults.AuthenticationScheme))
		{
			requireAuth = true;
			id = SwaggerModule.BearerId;
		}

		if (!requireAuth || string.IsNullOrEmpty(id)) return;

		operation.Responses.Add("401", new OpenApiResponse
		{
			Description = "Unauthorized",
		});
		operation.Responses.Add("403", new OpenApiResponse
		{
			Description = "Forbidden",
		});

		operation.Security = new List<OpenApiSecurityRequirement>
		{
			new()
			{
				{
					new OpenApiSecurityScheme
					{
						Reference = new OpenApiReference
						{
							Type = ReferenceType.SecurityScheme,
							Id = id
						}
					},
					Array.Empty<string>()
				}
			}
		};
	}
}