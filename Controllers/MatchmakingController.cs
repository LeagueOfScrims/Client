using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LOS.Controllers
{
    public class MatchmakingController : Controller
    {
        public IActionResult Index()
        {



            return View();
        }

        public IActionResult fivevfive()
        {
            return View();
        }

        [HttpPost]
        public void SearchForMatch()
        {



        }

    }
}