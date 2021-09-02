using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WattEsportsCore.Models.ViewModels
{
    public class RocketLeagueTeamsViewModel
    {
        public IEnumerable<RocketLeague> TeamOneList { get; set; }
        public IEnumerable<RocketLeague> TeamTwoList { get; set; }
        public IEnumerable<RocketLeague> TeamThreeList { get; set; }
        public IEnumerable<RocketLeague> TeamFourList { get; set; }
        public IEnumerable<RocketLeague> TeamFiveList { get; set; }

    }
}
