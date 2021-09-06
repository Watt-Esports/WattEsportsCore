using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
