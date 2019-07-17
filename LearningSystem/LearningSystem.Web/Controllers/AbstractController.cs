using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace LearningSystem.Web.Controllers
{
    public abstract class AbstractController : Controller
    {
        protected readonly IMapper mapper;
        public AbstractController(IMapper mapper)
        {
            this.mapper = mapper;
        }
    }
}