using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Common.Dto;
using Blog.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly RepositoryFacade _repositoryFacade;

        public UsersController(RepositoryFacade repositoryFacade)
        {
            _repositoryFacade = repositoryFacade;
        }

        [HttpGet("{id}")]
        public ActionResult<UserDto> Get(string id)
        {
            return _repositoryFacade.Users.Get(id);
        }
    }
}