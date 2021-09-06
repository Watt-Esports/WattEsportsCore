using System.Collections.Generic;

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
