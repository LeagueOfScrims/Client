using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LOS.Models;
using LCUSharp;
using LOS.Models.DataToObject;
using Newtonsoft.Json;
using System.Net.Http;
using LCUSharp.DataObjects;

namespace LOS.Controllers
{
    public class HomeController : Controller
    {
        public static HttpClient http;
        public ILeagueClient League;
        public async Task<IActionResult> Index()
        {
            try
            {
                League = await LeagueClient.Connect(@"E:\Riot Games\League of Legends");
                Summoners sum = new Summoners(League);
                var prof = sum.GetCurrentSummoner();
                if(prof.AccountId == 0)
                {
                    return View("NoApi");
                }
                ViewBag.Name = prof.DisplayName;
                await GetUserInfoAsync();
                return View(prof);

            }
            catch (Exception e)
            {
                
                return View("NoApi");
            }
            //CustomGamesManager cgm = new CustomGamesManager();
            //cgm.CreateOneOnOneGame("los", 20289202);

        }

        public IActionResult NoApi()
        {
            return View();
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
        public async Task GetUserInfoAsync()
        {

            var region = await League.MakeApiRequest(LCUSharp.HttpMethod.Get, "/riotclient/region-locale");
            var locals = JsonConvert.DeserializeObject<Region>(region.Content.ReadAsStringAsync().Result);

            Summoners sum = new Summoners(League);
            var player = sum.GetCurrentSummoner();

            Summoner user = new Summoner();
            user.SummonerID = player.SummonerId.ToString();
            user.SummonerName = player.DisplayName;
            user.Region = locals.RegionRegion;
            user.Role = "Test";

            http = new HttpClient();
            await http.PostAsJsonAsync("https://lossummonerinfoapi.azurewebsites.net/api/AddSummoner", user);
        }
    }
}
