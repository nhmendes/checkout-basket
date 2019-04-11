namespace BasketService.Presentation.WebApi.Controllers
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Text;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.IdentityModel.Tokens;

    [Route("[controller]")]
    public class BasketAuthorizationController : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet]
        [Route("connect/token")]
        public IActionResult Token()
        {
            return Ok($"Bearer {JwtToken.GenerateToken(60)}");
        }

        class JwtToken
        {
            /// <summary>
            /// Use the below code to generate symmetric Secret Key
            ///     var hmac = new HMACSHA256();
            ///     var key = Convert.ToBase64String(hmac.Key);
            /// </summary>
            private const string Secret = "1eZyE1T87t+EiwQALT+rN1l8xvn4dnqK5aAHCbwtOuR3IBymRkPA46VNOa6zpdvFY6dpjnyEAJxVvOT4Pem+uA==";

            public static string GenerateToken(int expireMinutes)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Expires = DateTime.UtcNow.AddMinutes(expireMinutes),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
        }
    }
}