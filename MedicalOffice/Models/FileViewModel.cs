using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CherryCitySoftware.MedicalOffice.Models
{
    public class FileViewModel
    {
        [Key]
        public int ID { get; set; }

        [StringLength(1024)]
        [Required]
        [Display(Name="File description")]
        public string Description { get; set; }

        //[Required]
        [Display(Name = "Path to the File")]
        public HttpPostedFileBase Content { get; set; }
    }
}