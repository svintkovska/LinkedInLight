using BLL.Interfaces;
using Domain.Models;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
	public class JwtTokenService : IJwtTokenService
	{
		private readonly IConfiguration _config;
		private readonly UserManager<ApplicationUser> _userManager;
		public JwtTokenService(IConfiguration configuration, UserManager<ApplicationUser> userManager)
		{
			_config = configuration;
			_userManager = userManager;
		}

		public async Task<string> CreateToken(ApplicationUser user)
		{
			IList<string> roles = await _userManager.GetRolesAsync(user);
			List<Claim> claims = new List<Claim>()
			{
				new Claim(ClaimTypes.Name, user.UserName),
				new Claim(ClaimTypes.NameIdentifier, user.Id)
			};

			foreach (var claim in roles)
			{
				claims.Add(new Claim(ClaimTypes.Role, claim));
			}
			var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetValue<String>("JWTSecretKey")));
			var signinCredentials = new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256);

			var jwt = new JwtSecurityToken(
				signingCredentials: signinCredentials,
				expires: DateTime.Now.AddDays(100),
				claims: claims
			);

			return new JwtSecurityTokenHandler().WriteToken(jwt);
		}

		public async Task<GoogleJsonWebSignature.Payload> VerifyGoogleToken(string tokenId)
		{
			string clientID = _config["GoogleAuthSettings:ClientId"];
			var setting = new GoogleJsonWebSignature.ValidationSettings()
			{
				Audience = new List<string> { clientID }
			};
			var payload = await GoogleJsonWebSignature.ValidateAsync(tokenId, setting);

			return payload;
		}
	}
}
