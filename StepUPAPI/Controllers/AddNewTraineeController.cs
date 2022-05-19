using StepUpBL;
using StepUpDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StepUPAPI.Controllers
{
    public class AddNewTraineeController : ApiController
    {
        StepUpBusinessLayer blObj;
        public AddNewTraineeController()
        {
            blObj = new StepUpBusinessLayer();
        }
        [HttpPost]
        public HttpResponseMessage AddTraineeDetails(TraineeDetailsDataTransferObject traineeObj)

        {

            try
            {
                int retValue = blObj.AddTraineeDetails(traineeObj);
                if (retValue == 1)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Department Added Successfully");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,"No Content");
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
