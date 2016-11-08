using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CherryCitySoftware.MedicalOffice.Models
{
    public class ResourceLink
    {
        [Key]
        public long Id { get; set; }    
        public string ResourceLinkName { get; set; }
        public string ResourceLinkAddress { get; set; }

        
    }
}