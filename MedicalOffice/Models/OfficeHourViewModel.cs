using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CherryCitySoftware.MedicalOffice.Models
{
    public class OfficeHourViewModel
    {[Display(Name = "Regular Office Hours")]
        public OfficeHour Regualhour;
        [Display(Name="Current Week Office Hours")]
        public OfficeHour Currenthour;

        public HolidayNote Holiday;

   //     public List<HolidayNote> Holiday;

        public string CurrentHourLabelText { get { return "Current Week Office Hours"; } }
        public string RegularhourLabelText { get { return "Regular Office Hours"; } }
        public string HolidayNoteLabelText { get { return "We are closed on these holidays"; } }
    }
}