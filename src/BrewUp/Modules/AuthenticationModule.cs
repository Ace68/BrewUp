using Brewup.Shared.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Brewup.Modules;

public class AuthenticationModule : IModule
{
	public bool IsEnabled => true;
	public int Order => 0;

	public void RegisterModule(WebApplicationBuilder builder)
	{
		var tokenAuthentication = new TokenAuthentication();
		builder.Configuration.GetSection("BrewUp:TokenAuthentication").Bind(tokenAuthentication);
		var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(GetSecretKey(tokenAuthentication.SecretKey)));

		var tokenValidationParameters = new TokenValidationParameters
		{
			// The signing key must match!
			ValidateIssuerSigningKey = true,
			IssuerSigningKey = signingKey,

			// Validate the JWT Issuer (iss) claim
			ValidateIssuer = true,
			ValidIssuer = tokenAuthentication.Issuer,

			// Validate the JWT Audience (aud) claim
			ValidateAudience = true,
			ValidAudience = tokenAuthentication.Audience,

			// Validate the token expiry
			ValidateLifetime = true,

			// If you want to allow a certain amount of clock drift, set that here:
			ClockSkew = TimeSpan.Zero
		};

		builder.Services.AddAuthentication(sharedOptions =>
		{
			sharedOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			sharedOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		}).AddJwtBearer(jwtBearerOptions =>
		{
			jwtBearerOptions.TokenValidationParameters = tokenValidationParameters;
			jwtBearerOptions.Events = new JwtBearerEvents
			{
				OnAuthenticationFailed = authenticationContext =>
				{
					if (authenticationContext.Exception.GetType() == typeof(SecurityTokenExpiredException))
						authenticationContext.Response.Headers.Add("Is-Token-Expired", "true");

					authenticationContext.NoResult();

					authenticationContext.Response.StatusCode = 500;
					authenticationContext.Response.ContentType = "text/plain";

					return authenticationContext.Response.WriteAsync(
						$"An error occurred processing your authentication. Details: {authenticationContext.Exception}");
				}
			};
		});

		builder.Services.AddAuthorization(options =>
		{
			options.AddPolicy("Administrators", policy => policy.RequireClaim("user_roles", "[Administrator]"));
		});
	}

	public void MapEndpoints(IEndpointRouteBuilder endpoints)
	{
		// no endpoints
	}

	#region Helpers
	private static string GetSecretKey(string secretKey) =>
		Encoding.UTF8.GetString(Encoding.Unicode.GetBytes(secretKey));
	#endregion
}