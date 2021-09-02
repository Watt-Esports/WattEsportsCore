using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WattEsportsCore.Models.ViewModels
{
    public class CommitteeViewModel
    {
        public IEnumerable<Committee> CommitteeList { get; set; }
        public IEnumerable<PreviousCommittee> PreviousCommitteeList { get; set; }


    }
}
