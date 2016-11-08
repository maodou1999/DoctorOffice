using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CherryCitySoftware.MedicalOffice.Models
{
    public class LoggingMessage
    {
        [Key]
        public long ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Category { get; set; }

        [StringLength(256)]
        public string UserName { get; set; }

        public DateTime EntryDate { get; set; }

        public string Message { get; set; }
    }
}