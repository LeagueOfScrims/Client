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
        public ILeagueClient League;
        public async Task<IActionResult> Index()
        {
            League = await LeagueClient.Connect(@"E:\Riot Games\League of Legends");
            var region = await League.MakeApiRequest(LCUSharp.HttpMethod.Get, "/riotclient/region-locale");
            var locals = JsonConvert.DeserializeObject<Region>(region.Content.ReadAsStringAsync().Result);

            Summoners sum = new Summoners(League);
            var player = sum.GetCurrentSummoner();

            Summoner user = new Summoner();
            user.SummonerID = player.SummonerId.ToString();
            user.SummonerName = player.DisplayName;
            user.Region = locals.RegionRegion;
            user.Role = "Test";
            return View(user);
        }

        public IActionResult fivevfive()
        {
            return View();
        }

        [HttpPost]
        public async void CreateGame(string summonerId, string match)
        {
            HttpClient http = new HttpClient();
            var data = await http.GetAsync("http://matchmakingapi.azurewebsites.net/oneVone/GetMatchInfo?matchId=" + match);
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
                CustomGamesManager cgm = new CustomGamesManager();
                cgm.JoinGame(enemy, match);
            }


        }
        


    }
}