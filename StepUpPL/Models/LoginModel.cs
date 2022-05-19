using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StepUpPL.Models
{
    public class LoginModel
    {
        [DisplayName("Login ID")]
        [Required(ErrorMessage = "Login Id Required")]
        public String loginId { get; set; }
        public String email { get; set; }
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string password { get; set; }
    }
    public class AdminModel
    {
        [Required(ErrorMessage = "Login Id Required")]
        [DisplayName("Admin ID")]
        public string adminId { get; set; }
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string password { get; set; }
    }
    public class Trainee
    {
        public int Trainee_PSNum { get; set; }
        public string Trainee_EmailiD { get; set; }
        public string CurrentSkillSet { get; set; }
        public string ExpectedTraining_Phase1 { get; set; }
        public string ExpectedTraining_Phase2 { get; set; }
        public DateTime TraineeDate { get; set; }

    }
    /* public class Loginids
     {
         public int Loginid { get; set; }
         public string Password { get; set; }
     }*/
    public class LoginModel1
    {
        [DisplayName("Enter New Login ID")]
        public String loginId { get; set; }
        [DisplayName("Enter Email ID")]
        public String email { get; set; }
        
        [DataType(DataType.Password)]
        public string password { get; set; }
    }
    public class FacultiesDTO1
    {
        [DisplayName("Login ID")]
        [Required(ErrorMessage = "Login Id Required")]
        public string Faculty_PS { get; set; }
     
      
        [DisplayName("Password")]
        [DataType(DataType.Password)]
        public string Faculty_Password { get; set; }
    }

}