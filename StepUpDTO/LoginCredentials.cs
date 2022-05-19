using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepUpDTO
{
    public class LoginCredentials
    {
       // [Required(ErrorMessage = "Login Id Required")]
        //[RegularExpression(@"^[A-Z\sa-z]+$", ErrorMessage = "Current Skills must have only Character")]
        //[StringLength(20, MinimumLength = 1, ErrorMessage = "Current Skills  should be between 10 and 20 characters")]
        public string loginId { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }
  

    public class AdminCredentials
    {
       
        public string adminId { get; set; }
        public string password { get; set; }
    }

    public class LoginIDs
    {

        public string loginid { get; set; }
        public string password { get; set; }
    }
    public class FacultDTO
    {
        public string Faculty_PS { get; set; }
        public string Faculty_Name { get; set; }
        public string Faculty_Email { get; set; }
        public string Faculty_Track { get; set; }
        public string Faculty_Password { get; set; }
    }

}
