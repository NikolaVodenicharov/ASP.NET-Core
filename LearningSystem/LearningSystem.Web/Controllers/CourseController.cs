using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LearningSystem.Web.Controllers
{
    [Route("Course")]
    public class CourseController : Controller
    {
        [Route(nameof(All))]
        public IActionResult All()
        {
            return View();
        }
    }
}