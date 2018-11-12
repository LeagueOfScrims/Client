using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LOS.Models;
using LCUSharp;
using LOS.Models.DataToObject;

namespace LOS.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var league = await LeagueClient.Connect(@"E:\Riot Games\League of Legends");
            Summoners sum = new Summoners(league);
            var prof = sum.GetCurrentSummoner();
            ViewBag.Name = prof.DisplayName;

            CustomGamesManager cgm = new CustomGamesManager();
            cgm.CreateOneOnOneGame("los", 20289202);
           
            return View(prof);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
