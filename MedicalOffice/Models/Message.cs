using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CherryCitySoftware.MedicalOffice.Models
{
    public class Message
    {
        [Key]
        public long Id { get; set; }
        [Required ]
        [StringLength(256)]
        public string Subject { get; set; }

        [Required ]
        [StringLength (8000)]
        public string Content { get; set; }

        [Required ]
        public DateTime Date { get; set; }

        public bool ReadByRecepient { get; set; }

        //public byte[] Attachment { get; set; }
        public ApplicationUser Sender { get; set; }
  //      [Required(ErrorMessage = "You must select a recepoent")]
        public ApplicationUser Recepient { get; set; }
        // For Attachment

       virtual public List<DocFile> DocFile { get; set; }


    }

    public class CreateMessageViewModel
    {
        [Required]
        [StringLength(256)]
        public string Subject { get; set; }

        [Required]
        [StringLength(8000)]
        public string Content { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name="Recepient")]
        public string RecepientId { get; set; }
    }


   
    public class MessageListViewModel
    {
        public long Id { get; set; }

        public string Subject { get; set; }
      
        public string Message { get; set; }
       
        public DateTime Date { get; set; }
        
        public string UserName { get; set; }

        public string UserId { get; set; }

        public bool ReadByRecepient { get; set; }
    }

    
    public class MessageReplyViewModel
    {
        public long Id { get; set; }
        [Required]
        [StringLength(256)]
        public string Subject { get; set; }

        [Required]
        [StringLength(4000)]
        public string Content { get; set; }

        public DateTime OriginalDate { get; set; }
    }
}