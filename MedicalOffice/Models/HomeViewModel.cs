using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CherryCitySoftware.MedicalOffice.Models
{
    public class HomeViewModel
    {
        public List<DocFile> Documents { get; set; }
        public ContactInformation Contact { get; set; }

    }
}