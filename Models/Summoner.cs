﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LOS.Models
{
    public class Summoner
    {
        public string SummonerID { get; set; }
        public string SummonerName { get; set; }
        public string Region { get; set; }
        public string Role { get; set; }
        public int Elo { get; set; }
        public DateTime QueueTime { get; set; }
    }
}
