using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WattEsportsCore.Models
{
    public class Player
    {
        [Key]
        public String PlayerID { get; set; }

        [Required]
        public String Name { get; set; }

        [Required]
        public String IGN { get; set; }

        [Required]
        public String Rank { get; set; }

        [Required]
        public String InGameRole { get; set; }

        public Player()
        {

        }
    }
}
