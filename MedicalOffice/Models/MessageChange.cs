using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CherryCitySoftware.MedicalOffice.Models
{
    public class MessageChange
    {

        [Key]
        public long Id { get; set; }
        [Required]
        [StringLength(256)]
        public string Subject { get; set; }

        [Required]
        [StringLength(8000)]
        public string Content { get; set; }

        [Required]
        public DateTime Date { get; set; }
       
        [Required(ErrorMessage = "You must select a recepient")]
        public string RecepientId { get; set; }
     
        public string  SenderId{ get; set; }

        public bool ReadByRecepient { get; set; }

        //public byte[] Attachment { get; set; }
        [ForeignKey ("SenderId")]
        public ApplicationUser Sender { get; set; }
        //  [Required(ErrorMessage = "You must select a recepoent")]
        [ForeignKey("RecepientId")]
        public ApplicationUser Recepient { get; set; }
        // For Attachment

        virtual public List<DocFile> DocFile { get; set; }

    }
    
}