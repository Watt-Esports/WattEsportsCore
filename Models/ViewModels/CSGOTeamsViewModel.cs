using System.Collections.Generic;

namespace WattEsportsCore.Models.ViewModels
{
    public class CSGOTeamsViewModel
    {
        public IEnumerable<Counterstrike> TeamOneList { get; set; }
        public IEnumerable<Counterstrike> TeamTwoList { get; set; }
        public IEnumerable<Counterstrike> TeamThreeList { get; set; }
        public IEnumerable<Counterstrike> TeamFourList { get; set; }
        public IEnumerable<Counterstrike> TeamFiveList { get; set; }

    }
}
