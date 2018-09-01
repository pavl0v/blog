using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Blog.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Blog.Api.Controllers.Api
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
        public async Task Token()
        {
            var login = Request.Form["login"];
            var password = Request.Form["password"];

            var identity = GetIdentity(login, password);
            if (identity == null)
            {
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync("Invalid username or password.");
                return;
            }

            var now = DateTime.UtcNow;
            var authParameters = new AuthParameters();
            var jwt = new JwtSecurityToken(
                issuer: authParameters.Issuer,
                audience: authParameters.Audience,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(authParameters.Lifetime)),
                signingCredentials: new SigningCredentials(authParameters.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            string encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };

            Response.ContentType = "application/json";
            await Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }

        private ClaimsIdentity GetIdentity(string login, string password)
        {
            var user = RepositoryFacade.Users.Get(login, password);
            if (user == null)
                return null;

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Id)
            };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            return claimsIdentity;
        }
    }
}