using LCUSharp;
using LOS.Models.DataToObject;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LOS.Models
{
    public class CustomGamesManager
    {

        public async void CreateNewFiveOnFiveGame(string Name, long Id)
        {
            ILeagueClient league = await LeagueClient.Connect(@"E:\Riot Games\League of Legends");
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
            ILeagueClient league = await LeagueClient.Connect(@"E:\Riot Games\League of Legends");
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
                    if (item.SummonerId == Enemyid)
                    {
                        AllIn = true;
                    }
                }
            }
            await league.MakeApiRequest(HttpMethod.Post, "/lol-lobby/v1/lobby/custom/start-champ-select", new StartGame());
        }

        public async void JoinGame(long enemy, string match)
        {
            ILeagueClient league = await LeagueClient.Connect(@"E:\Riot Games\League of Legends");
            while (true)
            {
                var response = await league.MakeApiRequest(HttpMethod.Get, "/lol-lobby/v2/received-invitations");
                var invites = JsonConvert.DeserializeObject<List<InviteModel>>(await response.Content.ReadAsStringAsync());

                foreach (var item in invites)
                {
                    if (item.FromSummonerId == enemy)
                    {
                        await league.MakeApiRequest(HttpMethod.Post, "/lol-lobby/v2/received-invitations/" + item.InvitationId + "/accept");
                        System.Net.Http.HttpClient http = new System.Net.Http.HttpClient();
                        var data = await http.GetAsync("http://matchmakingapi.azurewebsites.net/oneVone/KillMatch?match=" + match);
                        break;
                    }
                }
                Thread.Sleep(100);
            }
        }



    }
}
