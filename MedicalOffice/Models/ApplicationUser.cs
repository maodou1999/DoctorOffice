using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CherryCitySoftware.MedicalOffice.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        private bool forcechangpassword;
        public bool Forcechangpassword {
            get { return forcechangpassword; }
            set { forcechangpassword = value; }
            }
        //Add the new name propertites
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDay { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        // Use a sensible display name for views:
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }
        // Concatenate the Name  info for display in tables and such:
        
        [Display(Name="Name")]
        public string DisplayName 
        { get
            {
                string dspFirstname =
                        string.IsNullOrWhiteSpace(this.FirstName) ? "" : this.FirstName;
                string dspMiddlename =
                           string.IsNullOrWhiteSpace(this.MiddleName) ? "" : this.MiddleName;
                string dspLastname =
                           string.IsNullOrWhiteSpace(this.LastName) ? "" : this.LastName;
                return string
                       .Format("{0} {1} {2} ", dspFirstname, dspMiddlename, dspLastname);
            }
        }
        // Concatenate the address info for display in tables and such:

        [Display(Name="Address")]
        public string DisplayAddress
        {
            get
            {
                string dspAddress =
                    string.IsNullOrWhiteSpace(this.Address) ? "" : this.Address;
                string dspCity =
                    string.IsNullOrWhiteSpace(this.City) ? "" : this.City;
                string dspState =
                    string.IsNullOrWhiteSpace(this.State) ? "" : this.State;
                string dspPostalCode =
                    string.IsNullOrWhiteSpace(this.PostalCode) ? "" : this.PostalCode;
                return string
                    .Format("{0} {1} {2} {3}", dspAddress, dspCity, dspState, dspPostalCode);
            }
        }

  //      public virtual ICollection<Message> Messages { get; set; }
        public ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}