using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LCUSharp;
using LOS.Models;
using LOS.Models.DataToObject;
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
        public void CreateGame(string summonerId, OneVOneMatch match)
        {
            Random r = new Random(10000);
            long enemy;
            if (match.Sum1.SummonerID == summonerId)
            {
                enemy = Convert.ToInt64(match.Sum2.SummonerID);
                CustomGamesManager cgm = new CustomGamesManager();
                cgm.CreateOneOnOneGame("los" + r.Next(), enemy);
            }
            else
            {
                enemy = Convert.ToInt64(match.Sum1.SummonerID);
            }
       

        }



    }
}