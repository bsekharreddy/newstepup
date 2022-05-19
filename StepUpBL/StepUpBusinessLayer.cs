using StepUpDAL;
using StepUpDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepUpBL
{
    public class StepUpBusinessLayer
    {
        StepUpDataAccessLayer dlObj;

        public StepUpBusinessLayer()
        {
            dlObj = new StepUpDataAccessLayer();
        }
        public List<TraineeDetailsDataTransferObject> GetTraineeDetails()
        {
            try
            {
                List<TraineeDetailsDataTransferObject> lstFromDl = dlObj.FetchTraineeDetails();
                return lstFromDl;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<LoginCredentials> GetLoginDetails()
        {
            try
            {
                List<LoginCredentials> loginCredentials = dlObj.Fetchlogin();
                return loginCredentials;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public int AddTraineeDetails(TraineeDetailsDataTransferObject dtObj)
        {
            try
            {
                return dlObj.UpdateTraineeDetails(dtObj);

            }
            catch (Exception)
            {

                throw;
            }
        }
        public int AddLoginDetails(LoginCredentials dtObj)
        {
            try
            {
                return dlObj.UpdateCredintialsDetails(dtObj);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<LoginCredentials> LoginValidate(LoginCredentials loginCreds)
        {
            try
            {
                return dlObj.LoginValidation(loginCreds);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<TraineeDetailsDataTransferObject> GetTraineeByPSNum(string traineePSNum)
        {
            try
            {
                List<TraineeDetailsDataTransferObject> lstFromDl = dlObj.FetchTraineeByPSNum(traineePSNum);
                return lstFromDl;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<AdminCredentials> AdminValidate(AdminCredentials adminCreds)
        {

            try
            {
                return dlObj.AdminValidation(adminCreds);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int UpdatePasswordDetails(LoginCredentials dtObj)
        {
            try
            {
                return dlObj.UpdatePassword(dtObj);

            }
            catch (Exception)
            {

                throw;
            }
        }
        public int AddFacultyDetails(FacultDTO dtObj)
        {
            try
            {
                return dlObj.UpdateFacultyDetails(dtObj);

            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<FacultDTO> FacultyValidate(FacultDTO FacultyCreds)
        {

            try
            {
                return dlObj.FacultyValidation(FacultyCreds);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<TraineeDetailsDataTransferObject> GetTraineeDetails1()
        {
            try
            {
                List<TraineeDetailsDataTransferObject> lstFromDl = dlObj.FetchTraineeDetails1();
                return lstFromDl;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<TraineeDetailsDataTransferObject> GetTraineeByPSNum1(string traineePSNum)
        {
            try
            {
                List<TraineeDetailsDataTransferObject> lstFromDl = dlObj.FetchTraineeByPSNum(traineePSNum);
                return lstFromDl;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
