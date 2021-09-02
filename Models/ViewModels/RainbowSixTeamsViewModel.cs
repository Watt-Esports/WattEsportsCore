using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WattEsportsCore.Models.ViewModels
{
    public class RainbowSixTeamsViewModel
    {
        public IEnumerable<RainbowSix> TeamOneList { get; set; }
        public IEnumerable<RainbowSix> TeamTwoList { get; set; }
        public IEnumerable<RainbowSix> TeamThreeList { get; set; }
        public IEnumerable<RainbowSix> TeamFourList { get; set; }
        public IEnumerable<RainbowSix> TeamFiveList { get; set; }

    }
}
