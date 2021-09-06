using System.Collections.Generic;


namespace WattEsportsCore.Models.ViewModels
{
    public class CommitteeViewModel
    {
        public IEnumerable<Committee> CommitteeList { get; set; }
        public IEnumerable<PreviousCommittee> PreviousCommitteeList { get; set; }


    }
}
