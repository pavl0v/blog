using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Blog.Common.Dto;
using Blog.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Blog.Service.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BlogApiControllerBase
    {
        public AuthController(RepositoryFacade repositoryFacade) : base(repositoryFacade)
        {
            //
        }

        [HttpPost("token")]
        public async Task Token([FromBody] UserDto user)
        {
            var identity = GetIdentity(user?.Login, user?.Password);
            if (identity == null)
            {
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync("Invalid username or password.");
                return;
            }

            var now = DateTime.UtcNow;
            var authParameters = new AuthParameters();
            var token = new JwtSecurityToken(
                issuer: authParameters.Issuer,
                audience: authParameters.Audience,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(authParameters.Lifetime)),
                signingCredentials: new SigningCredentials(authParameters.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            string encodedToken = new JwtSecurityTokenHandler().WriteToken(token);
            var response = new
            {
                access_token = encodedToken,
                username = identity.Name
            };

            Response.ContentType = "application/json";
            await Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }

        private ClaimsIdentity GetIdentity(string login, string password)
        {
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
                return null;

            var user = RepositoryFacade.Users.Get(login, password);
            if (user == null)
                return null;

            // User ID is stored in default role claim
            // TODO : create custom ID claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.UserId.ToString())
            };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            return claimsIdentity;
        }
    }
}