using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WattEsportsCore.Models
{
    public class RocketLeague
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Full Name - Required.")]
        public String Name { get; set; }

        [Required]
        [Display(Name = "In Game Name - Required.")]

        public String IGN { get; set; }


        public String Rank { get; set; }

      
        public String InGameRole { get; set; }

        [Required]
        [Display(Name = "Team Number")]
        public string SelectedTeamNumber { get; set; }

        [NotMapped]
        [DisplayName("Team Number - Required")]
        public List<SelectListItem> TeamNumberItems { get; set; }


        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Image Name")]
        public string ImageName { get; set; }

        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile ImageFile { get; set; }

        public RocketLeague()
        {

        }
    }
}
