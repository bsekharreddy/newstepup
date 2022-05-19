using StepUpBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StepUPAPI.Controllers
{
    public class TraineeDetailsController : ApiController
    {
        StepUpBusinessLayer blObj;
       
        public TraineeDetailsController()
        {
            blObj = new StepUpBusinessLayer();
        }
        [HttpGet]
        public HttpResponseMessage GetTraineeFullDetails()
        {
            try
            {
                var result = blObj.GetTraineeDetails();
                if (result.Count > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,"No Trainee Details Found");
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
