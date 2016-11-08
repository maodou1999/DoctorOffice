using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CherryCitySoftware.MedicalOffice.Models
{
    public class PatientPolicy
    {
        [Key]
        public long Id { get; set; }


        [Required]
        [StringLength(4000)]
        public string Policy { get; set; }


        //public byte[] Attachment { get; set; }
        public ApplicationUser Patient { get; set; }

       
    }
}