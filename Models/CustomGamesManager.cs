using LCUSharp;
using LOS.Models.DataToObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LOS.Models
{
    public class CustomGamesManager
    {

        public async void CreateNewFiveOnFiveGame(string Name, long Id)
        {
            ILeagueClient league = await LeagueClient.Connect(@"C:\Riot Games\League of Legends");       
            ApiObject api = new ApiObject();
            var obj = api.createCustomGameFiveOnFive(Name);
            
            var response = league.MakeApiRequest(HttpMethod.Post, "/lol-lobby/v2/lobby", obj).Result;

            var invites = new List<LobbyInvitation>();

            invites.Add(new LobbyInvitation
            {
                ToSummonerId = 20289202
            });

            invites.Add(new LobbyInvitation
            {
                ToSummonerId = 92420917
            });

            await league.MakeApiRequest(HttpMethod.Post, "/lol-lobby/v2/lobby/invitations", invites);

        }

        public async void CreateOneOnOneGame(string LobbyName, long Enemyid)
        {
            Random r = new Random();
            ILeagueClient league = await LeagueClient.Connect(@"C:\Riot Games\League of Legends");
            ApiObject api = new ApiObject();
            var obj = api.createCustomGameOneOnOne(LobbyName);
            var response = league.MakeApiRequest(HttpMethod.Post, "/lol-lobby/v2/lobby", obj).Result;
            while (true)
            {
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    obj = api.createCustomGameOneOnOne(LobbyName + r.Next());
                    response = league.MakeApiRequest(HttpMethod.Post, "/lol-lobby/v2/lobby", obj).Result;
                }
                else
                {
                    break;
                }
            }
         
            var invites = new List<LobbyInvitation>();

            invites.Add(new LobbyInvitation
            {
                ToSummonerId = Enemyid
            });
            await league.MakeApiRequest(HttpMethod.Post, "/lol-lobby/v2/lobby/invitations", invites);
            bool AllIn = false;
            while (!AllIn)
            {
                LobbyPlayerInfo[] players = await league.MakeApiRequestAs<LobbyPlayerInfo[]>(HttpMethod.Get, "/lol-lobby/v2/lobby/members");
                foreach (var item in players)
                {
                    if(item.SummonerId == Enemyid)
                    {
                        AllIn = true;
                    }
                }
            }
            await league.MakeApiRequest(HttpMethod.Post, "/lol-lobby/v1/lobby/custom/start-champ-select", new StartGame());
        }




    }
}
