using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Common.Dto;
using Blog.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Service.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BlogApiControllerBase
    {
        public UsersController(RepositoryFacade repositoryFacade) : base(repositoryFacade)
        {
            //
        }

        [HttpGet("{id}")]
        public ActionResult<UserDto> Get(string id)
        {
            return RepositoryFacade.Users.Get(id);
        }
    }
}