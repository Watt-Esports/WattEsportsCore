using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WattEsportsCore.Models
{
    public class PreviousCommittee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public String Name { get; set; }

        [Required]
        public String Role { get; set; }

        [Required]
        [DisplayName("Month & Year Elected i.e. March 2019")]
        public String Date { get; set; }

    }
}
