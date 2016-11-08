﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CherryCitySoftware.MedicalOffice.Models
{
    public class OfficeHour
    {
        
        [Key]
        public int Id { get; set; }
        [StringLength(128)]
        public string Monday { get; set; }
        [StringLength(128)]
        public string Tuesday { get; set; }
        [StringLength(128)]
        public string Wednesday { get; set; }
        [StringLength(128)]
        public string Thursday { get; set; }
        [StringLength(128)]
        [Display(Name="Friday")]
        public string Friday { get; set; }
        [StringLength(128)]
        public string Saturday { get; set; }
        [StringLength(128)]
        public string Sunday { get; set; }
    }
}