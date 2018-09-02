using Blog.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Service.Controllers.Api
{
    public class BlogApiControllerBase : ControllerBase
    {
        protected RepositoryFacade RepositoryFacade { get; }

        public BlogApiControllerBase(RepositoryFacade repositoryFacade)
        {
            RepositoryFacade = repositoryFacade;
        }
    }
}
