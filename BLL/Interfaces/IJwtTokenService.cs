
using Domain.Models;
using Google.Apis.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
	public interface IJwtTokenService
	{
		Task<GoogleJsonWebSignature.Payload> VerifyGoogleToken(string tokenId);
		Task<string> CreateToken(ApplicationUser user);
	}
}
