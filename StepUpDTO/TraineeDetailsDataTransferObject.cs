using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepUpDTO
{
    public class TraineeDetailsDataTransferObject
    {
        public int Trainee_PSNum { get; set; }
        public string Trainee_EmailiD { get; set; }
        public string CurrentSkillSet { get; set; }
        public string ExpectedTraining_Phase1 { get; set; }
        public string ExpectedTraining_Phase2 { get; set; }
        public DateTime TraineeDate { get; set; }

    }
   
   
}
