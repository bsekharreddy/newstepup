using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace StepUpPL.Models
{
    public class AddDetails
    {
        public int Trainee_PSNum { get; set; }
        public string Trainee_EmailiD { get; set; }
        public string CurrentSkillSet { get; set; }
        public string ExpectedTraining_Phase1 { get; set; }
        public string ExpectedTraining_Phase2 { get; set; }
        public DateTime TraineeDate { get; set; }
    }
    public class AddDetails1
    {
        [DisplayName("Enter Trainee PS Number")]
        public int Trainee_PSNum { get; set; }
        [DisplayName("Enter Mail ID")]
        public string Trainee_EmailiD { get; set; }
        [DisplayName("Enter Current Skillset")]
        public string CurrentSkillSet { get; set; }
        [DisplayName("Enter Expected Training Phase 1")]
        public string ExpectedTraining_Phase1 { get; set; }
        [DisplayName("Enter Expected Training Phase 2")]
        public string ExpectedTraining_Phase2 { get; set; }
      
        public DateTime TraineeDate { get; set; }
    }
    public class FacultiesDTO
    {
        [DisplayName("Enter Faculty PSnumber")]
        public string Faculty_PS { get; set; }
        [DisplayName("Enter Faculty Name")]
        public string Faculty_Name { get; set; }
        [DisplayName("Enter Faculty Email ID")]
        public string Faculty_Email { get; set; }
        [DisplayName("Enter Faculty Track")]
        public string Faculty_Track { get; set; }
        
        public string Faculty_Password { get; set; }
    }
  
}