using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CherryCitySoftware.MedicalOffice.Models
{
    public class DocFile
    {
        [Key]
        public int ID { get; set; }

        [StringLength(1024)]
        [Required]
        public string Description { get; set; }

        [Required]
        public byte[] Content { get; set; }

        [Required]
        [StringLength(512)]
        public string FileType { get; set; }

        [Required]
        [StringLength(1024)]
        public string FileName { get; set; }
        // For Message Attachment
        virtual public Message Message { get; set; }

    }
}