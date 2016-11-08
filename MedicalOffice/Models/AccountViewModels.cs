using CherryCitySoftware.MedicalOffice.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CherryCitySoftware.MedicalOffice.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string Action { get; set; }
        public string ReturnUrl { get; set; }
    }

    public class ManageUserViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string ReturnUrl { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        //Add the new name propertites
        [Display (Name="First Name")]
        public string FirstName { get; set; }
        [Display (Name="Middle Name")]
        public string MiddleName { get; set; }
        [Display (Name="Last Name")]
        public string LastName { get; set; }


        //public DateTime  BirthDay { get; set; }

        [Phone]
        [Display(Name="Phone Number")]
        public string PhoneNumber { get; set; }
        // Add the new address properties:
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        // Use a sensible display name for views:
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
      

        public string ConfirmPassword { get; set; }

        public RegisterViewModel() : this(null){}
        public RegisterViewModel(ApplicationUser user)
        {
            Roles = new List<SelectRoleEditorViewModel>();
            var Db = new ApplicationDbContext();
            // Add all available roles to the list of EditorViewModels:
            var allRoles = Db.ApplicationRoles.OrderBy(r=>r.Name);
           
            foreach (var role in allRoles)
            {
                var rvm = new SelectRoleEditorViewModel(role);
                this.Roles.Add(rvm);
            }
            if (user == null)
            {
                this.Roles.Where(r => r.RoleName.Equals("Patient", System.StringComparison.InvariantCultureIgnoreCase)).Single().Selected = true;
                return;
            }
            for (int i=0; i<this.Roles.Count; i++)
            {
                this.Roles[i].Selected = (user.Roles.FirstOrDefault(r => r.RoleId.Equals(Roles[i].RoleId, System.StringComparison.InvariantCultureIgnoreCase)) != null);
            }
        }

        public List<SelectRoleEditorViewModel> Roles { get; set; }

        public DateTime? BirthDay { get; set; }
    }
    public class EditUserViewModel
    {

        public string Id { get; set; }
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }
        //Add the new name propertites
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }
       
        // Add the Address Info:
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        
        // Use a sensible display name for views:
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }
        public EditUserViewModel() : this(null){}
        public EditUserViewModel(ApplicationUser user)
        {
            Roles = new List<SelectRoleEditorViewModel>();
            var Db = new ApplicationDbContext();
            // Add all available roles to the list of EditorViewModels:
            var allRoles = Db.ApplicationRoles.OrderBy(r=>r.Name);
           
            foreach (var role in allRoles)
            {
                var rvm = new SelectRoleEditorViewModel(role);
                this.Roles.Add(rvm);
            }
            if (user == null)
            {
                this.Roles.Where(r => r.RoleName.Equals("Patient", System.StringComparison.InvariantCultureIgnoreCase)).Single().Selected = true;
                return;
            }
            for (int i=0; i<this.Roles.Count; i++)
            {
                this.Roles[i].Selected = (user.Roles.FirstOrDefault(r => r.RoleId.Equals(Roles[i].RoleId, System.StringComparison.InvariantCultureIgnoreCase)) != null);
            }
        }
      public List<SelectRoleEditorViewModel> Roles { get; set; }


      public DateTime? BirthDay { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ResetPasswordByAdminViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
        public string DisplayName { get; set; }
    }
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
