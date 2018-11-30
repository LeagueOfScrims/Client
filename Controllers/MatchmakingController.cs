using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using LCUSharp;
using LOS.Models;
using LOS.Models.DataToObject;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
        public async void CreateGame(string summonerId, string match)
        {
            HttpClient http = new HttpClient();
            var data = await http.GetAsync("https://localhost:44311/oneVone/GetMatchInfo?matchId=" + match);
            OneVOneMatch Match = JsonConvert.DeserializeObject<OneVOneMatch>(await data.Content.ReadAsStringAsync());
            Random r = new Random(10000);
            long enemy;
            if (Match.Sum1.SummonerID == summonerId)
            {
                enemy = Convert.ToInt64(Match.Sum2.SummonerID);
                CustomGamesManager cgm = new CustomGamesManager();
                cgm.CreateOneOnOneGame("los" + r.Next(), enemy);
            }
            else
            {
                enemy = Convert.ToInt64(Match.Sum1.SummonerID);
            }
       

        }
        


    }
    public class foo
    {
        public string ID { get; set; }
    }
}