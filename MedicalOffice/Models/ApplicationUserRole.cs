using Microsoft.AspNet.Identity.EntityFramework;

namespace CherryCitySoftware.MedicalOffice.Models
{
    public class ApplicationUserRole : IdentityUserRole
    {
        public ApplicationUserRole()
            : base()
        { }

        public ApplicationRole Role { get; set; }
    }
}