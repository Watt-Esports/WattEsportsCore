using System.Collections.Generic;

namespace WattEsportsCore.Models.ViewModels
{
    public class LeagueOfLegendsTeamsViewModel
    {
        public IEnumerable<LeagueOfLegends> TeamOneList { get; set; }
        public IEnumerable<LeagueOfLegends> TeamTwoList { get; set; }
        public IEnumerable<LeagueOfLegends> TeamThreeList { get; set; }
        public IEnumerable<LeagueOfLegends> TeamFourList { get; set; }
        public IEnumerable<LeagueOfLegends> TeamFiveList { get; set; }

    }
}
