using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TitanGate.WebSiteStore.Api.Models;
using TitanGate.WebSiteStore.Entities.Business;
using TitanGate.WebSiteStore.Entities.DB;
using TitanGate.WebSiteStore.Services;

namespace TitanGate.WebSiteStore.Api.Controllers
{
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly SecuritySettings _security;

        public UserController(IUserService userService, IOptions<SecuritySettings> security)
        {
            _userService = userService;
            _security = security.Value;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody]Authentication authentication)
        {
            User user = await _userService.Login(authentication.Username, authentication.Password);
            if (user == null)
            {
                return Unauthorized();
            }

            Tokens tokens = GenerateTokens(new JwtSecurityTokenHandler(), user);
            return Ok(tokens);
        }

        [HttpPost]
        [Route("refresh")]
        public async Task<IActionResult> Refresh([FromBody]string refreshToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            ClaimsPrincipal claimsPrincipal;
            SecurityToken securityToken;
            try
            {
                claimsPrincipal = tokenHandler.ValidateToken(refreshToken, new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_security.JwtEncryptionKey)),
                }, out securityToken);
            }
            catch (SecurityTokenException)
            {
                return Unauthorized();
            }

            string username = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier).Value;
            User user = await _userService.GetByUsername(username);

            if (user == null)
            {
                return Unauthorized();
            }

            Tokens tokens = GenerateTokens(tokenHandler, user);
            return Ok(tokens);
        }

        private SecurityTokenDescriptor GenerateTokenDescriptor(User user, bool isRefreshToken)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]{
                    new Claim(ClaimTypes.NameIdentifier, user.Username)
                }),
                Issuer = "TitanGate.WebSiteStore",
                Audience = "TitanGate.WebSiteStore",
                Expires = isRefreshToken 
                    ? DateTime.Now.AddHours(_security.RefreshTokenValidHours) 
                    : DateTime.Now.AddHours(_security.TokenValidHours),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_security.JwtEncryptionKey)), SecurityAlgorithms.HmacSha512Signature),
            };

            return tokenDescriptor;
        }

        private Tokens GenerateTokens(JwtSecurityTokenHandler tokenHandler, User user)
        {
            SecurityTokenDescriptor tokenDescriptor = GenerateTokenDescriptor(user, false);
            SecurityTokenDescriptor refreshTokenDescriptor = GenerateTokenDescriptor(user, true);

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            SecurityToken refreshToken = tokenHandler.CreateToken(refreshTokenDescriptor);

            return new Tokens
            {
                Token = tokenHandler.WriteToken(token),
                ValidTo = token.ValidTo,
                RefreshToken = tokenHandler.WriteToken(refreshToken),
                RefreshValidTo = refreshToken.ValidTo
            };
        }
    }
}
