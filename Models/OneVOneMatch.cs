using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LOS.Models
{
    public class OneVOneMatch
    {
        public Summoner Sum1;
        public Summoner Sum2;
        public int MatchId;
        public bool Sum1Accept;
        public bool Sum2Accept;

        public OneVOneMatch(Summoner sum1, Summoner sum2, int matchId)
        {
            Sum1 = sum1;
            Sum2 = sum2;
            MatchId = matchId;
            Sum1Accept = false;
            Sum2Accept = false;
        }

    }
}
