using System.Collections.Generic;

namespace WattEsportsCore.Models.ViewModels
{
    public class HearthstoneTeamsViewModel
    {
        public IEnumerable<Hearthstone> TeamOneList { get; set; }
        public IEnumerable<Hearthstone> TeamTwoList { get; set; }
        public IEnumerable<Hearthstone> TeamThreeList { get; set; }
        public IEnumerable<Hearthstone> TeamFourList { get; set; }
        public IEnumerable<Hearthstone> TeamFiveList { get; set; }

    }
}
