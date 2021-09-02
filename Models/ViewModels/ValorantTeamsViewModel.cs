using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WattEsportsCore.Models.ViewModels
{
    public class ValorantTeamsViewModel
    {
        public IEnumerable<Valorant> TeamOneList { get; set; }
        public IEnumerable<Valorant> TeamTwoList { get; set; }
        public IEnumerable<Valorant> TeamThreeList { get; set; }
        public IEnumerable<Valorant> TeamFourList { get; set; }
        public IEnumerable<Valorant> TeamFiveList { get; set; }

    }
}
