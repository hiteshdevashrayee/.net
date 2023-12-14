using JWTLibrary;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ASP_.NET_Core_Web_API_1._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OAuthController : ControllerBase
    {
        private readonly JWTBearerSettings jwtBearerSettingsValue;
        public OAuthController(IOptions<JWTBearerSettings> jwtBearerSettingsOptions)
        {
            jwtBearerSettingsValue = jwtBearerSettingsOptions.Value;
        }

        [HttpPost("token"), Consumes("application/x-www-form-urlencoded")]
        public IActionResult Token([FromForm(Name = "grant_type")] string grantType)
        {
            if (!grantType.Equals("hiteshd@webgility.com"))
            {
                return BadRequest();
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            //var token = new JwtSecurityToken();
            var now = DateTime.UtcNow;
            var expiry = now.Add(TimeSpan.FromHours(1));

            var jwtBearerAuthenticatedClient = new JWTBearerClient()
            {
                IsAuthenticated = true,
                AuthenticationType = JwtBearerDefaults.AuthenticationScheme,
                Name = "JWTBearer",
            };

            var token = tokenHandler.WriteToken(tokenHandler.CreateToken(
                new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(jwtBearerAuthenticatedClient,
                    new List<Claim>
                    {
                        {new Claim(JwtRegisteredClaimNames.Name, "JWT Bearer") }
                    }),
                    Expires = expiry,
                    Issuer = jwtBearerSettingsValue.Issuer,
                    Audience = jwtBearerSettingsValue.Audience,
                    SigningCredentials = new SigningCredentials
                    (
                        new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtBearerSettingsValue.SigningKey)
                        ), SecurityAlgorithms.HmacSha512Signature
                    ),
                }));

            return Ok(new
            {
                access_token = token,
                token_type = JwtBearerDefaults.AuthenticationScheme,
                expires_in = expiry.Subtract(DateTime.UtcNow).TotalSeconds.ToString("0")
            });
        }
    }
}
