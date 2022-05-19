using StepUpDTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepUpDAL
{
    public class StepUpDataAccessLayer
    {
        SqlConnection conObj;
        SqlCommand cmdObj;
        TraineeProgramContext contxtObj;
        public StepUpDataAccessLayer()
        {
            conObj = new SqlConnection(ConfigurationManager.ConnectionStrings["SetUpConnectionString"].ConnectionString);
            cmdObj = new SqlCommand();
            contxtObj=new TraineeProgramContext();
        }
        public List<TraineeDetailsDataTransferObject> FetchTraineeDetails()
        {
            try
            {
                var result = contxtObj.TraineeInformations.ToList();
                List<TraineeDetailsDataTransferObject> lstOfTraineeDetails = new List<TraineeDetailsDataTransferObject>();
                foreach (var traineeResult in result)
                {
                    lstOfTraineeDetails.Add(new TraineeDetailsDataTransferObject()
                    {
                       
                        Trainee_PSNum = traineeResult.PSnumber,
                        Trainee_EmailiD = traineeResult.EmailID,
                        CurrentSkillSet=traineeResult.CurrentSkillSet,
                        ExpectedTraining_Phase1 = traineeResult.ExpectedTraining_Phase1,
                        ExpectedTraining_Phase2 = traineeResult.ExpectedTraining_Phase2,
                        TraineeDate = Convert.ToDateTime(traineeResult.Date)
                    });

                }
                return lstOfTraineeDetails;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public List<LoginCredentials> Fetchlogin()
        {
            try
            {
                var result = contxtObj.LttsCredintials.ToList();
                List<LoginCredentials> lstOfTraineeDetails = new List<LoginCredentials>();
                foreach (var traineeResult in result)
                {
                    lstOfTraineeDetails.Add(new LoginCredentials()
                    {
                        loginId = traineeResult.LoginID,
                        
                        password = traineeResult.LoginPassword
                       
                    });

                }
                return lstOfTraineeDetails;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public List<AdminCredentials> AdminValidation(AdminCredentials adminCreds)
        {
            var result = contxtObj.AdminCredintials.Where(w => w.AdminID == adminCreds.adminId && w.Password == adminCreds.password).ToList();
            List<AdminCredentials> AdminDetails = new List<AdminCredentials>();
            foreach (var item in result)
            {
                AdminDetails.Add(new AdminCredentials()
                {
                    adminId = item.AdminID,
                    password = item.Password,
                });

            }
            return AdminDetails;

        }
        public List<LoginCredentials> LoginValidation(LoginCredentials loginCreds)
        {
            try
            {
                
                var result = contxtObj.LttsCredintials.Where(w => w.LoginID == loginCreds.loginId && w.LoginPassword == loginCreds.password).ToList();
                List<LoginCredentials> loginDetails = new List<LoginCredentials>();
                foreach (var loginResult in result)
                {
                    loginDetails.Add(new LoginCredentials()
                    {
                        loginId = loginResult.LoginID,
                        password = loginResult.LoginPassword
                    });
                }
                return loginDetails;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public int UpdateCredintialsDetails(LoginCredentials dtObj)
        {
            try
            {
                cmdObj.CommandText = @"uspRequestCredintials";
                cmdObj.CommandType = System.Data.CommandType.StoredProcedure;
                cmdObj.Connection = conObj;
                cmdObj.Parameters.AddWithValue("@LoginID", dtObj.loginId);
                cmdObj.Parameters.AddWithValue("@Email",dtObj.email);
                cmdObj.Parameters.AddWithValue("@LoginPassword", dtObj.password);
              

                SqlParameter ParRetValue = new SqlParameter();
                ParRetValue.Direction = ParameterDirection.ReturnValue;
                ParRetValue.SqlDbType = SqlDbType.Int;
                cmdObj.Parameters.Add(ParRetValue);

                conObj.Open();
                cmdObj.ExecuteNonQuery();
                return Convert.ToInt32(ParRetValue.Value);
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                conObj.Close();
            }
        }
        public int UpdateTraineeDetails(TraineeDetailsDataTransferObject dtObj)
        {
            try
            {
                cmdObj.CommandText = @"uspRequestDetails";
                cmdObj.CommandType = System.Data.CommandType.StoredProcedure;
                cmdObj.Connection = conObj;
                cmdObj.Parameters.AddWithValue("@PSno", dtObj.Trainee_PSNum);
                cmdObj.Parameters.AddWithValue("@Emailid", dtObj.Trainee_EmailiD);
                cmdObj.Parameters.AddWithValue("@CurrentSkillSet", dtObj.CurrentSkillSet);
                cmdObj.Parameters.AddWithValue("@expectedTraining_Phase1", dtObj.ExpectedTraining_Phase1);
                cmdObj.Parameters.AddWithValue("@expectedTraining_Phase2", dtObj.ExpectedTraining_Phase2);
                cmdObj.Parameters.AddWithValue("@date",dtObj.TraineeDate);

                SqlParameter ParRetValue = new SqlParameter();
                ParRetValue.Direction = ParameterDirection.ReturnValue;
                ParRetValue.SqlDbType = SqlDbType.Int;
                cmdObj.Parameters.Add(ParRetValue);

                conObj.Open();
                cmdObj.ExecuteNonQuery();
                return Convert.ToInt32(ParRetValue.Value);
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                conObj.Close();
            }


        }

        public int UpdatePassword(LoginCredentials dtObj)
        {
            try
            {
                cmdObj.CommandText = @"uspUpdatepass";
                cmdObj.CommandType = System.Data.CommandType.StoredProcedure;
                cmdObj.Connection = conObj;
                cmdObj.Parameters.AddWithValue("@LoginID", dtObj.loginId);
                cmdObj.Parameters.AddWithValue("@Email", dtObj.email);
                cmdObj.Parameters.AddWithValue("@LoginPassword", dtObj.password);


                SqlParameter ParRetValue = new SqlParameter();
                ParRetValue.Direction = ParameterDirection.ReturnValue;
                ParRetValue.SqlDbType = SqlDbType.Int;
                cmdObj.Parameters.Add(ParRetValue);

                conObj.Open();
                cmdObj.ExecuteNonQuery();
                return Convert.ToInt32(ParRetValue.Value);
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                conObj.Close();
            }
        }
        public List<TraineeDetailsDataTransferObject> FetchTraineeByPSNum(string traineePSNum)
        {
            var result = contxtObj.TraineeInformations.Where(w => (w.PSnumber+w.EmailID+w.CurrentSkillSet).ToString().Contains(traineePSNum)).ToList();
            List<TraineeDetailsDataTransferObject> lstOfTraineeDetails = new List<TraineeDetailsDataTransferObject>();
            foreach (var traineeResult in result)
            {
                lstOfTraineeDetails.Add(new TraineeDetailsDataTransferObject()
                {

                    Trainee_PSNum = traineeResult.PSnumber,
                    Trainee_EmailiD = traineeResult.EmailID,
                    CurrentSkillSet = traineeResult.CurrentSkillSet,
                    ExpectedTraining_Phase1 = traineeResult.ExpectedTraining_Phase1,
                    ExpectedTraining_Phase2 = traineeResult.ExpectedTraining_Phase2,
                    TraineeDate = Convert.ToDateTime(traineeResult.Date)
                });

            }
            return lstOfTraineeDetails;
        }
        public int UpdateFacultyDetails(FacultDTO dtObj)
        {
            try
            {
                cmdObj.CommandText = @"uspFacultyDetails";
                cmdObj.CommandType = System.Data.CommandType.StoredProcedure;
                cmdObj.Connection = conObj;
                cmdObj.Parameters.AddWithValue("@FacultyPS", dtObj.Faculty_PS);
                cmdObj.Parameters.AddWithValue("@FacultyName", dtObj.Faculty_Name);
                cmdObj.Parameters.AddWithValue("@EmailID", dtObj.Faculty_Email);
                cmdObj.Parameters.AddWithValue("@Track", dtObj.Faculty_Track);
                cmdObj.Parameters.AddWithValue("@Password", dtObj.Faculty_Password);
                

                SqlParameter ParRetValue = new SqlParameter();
                ParRetValue.Direction = ParameterDirection.ReturnValue;
                ParRetValue.SqlDbType = SqlDbType.Int;
                cmdObj.Parameters.Add(ParRetValue);

                conObj.Open();
                cmdObj.ExecuteNonQuery();
                return Convert.ToInt32(ParRetValue.Value);
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                conObj.Close();
            }


        }

        public List<FacultDTO> FacultyValidation(FacultDTO adminCreds)
        {
            var result = contxtObj.Faculties.Where(w => w.FacultyPS == adminCreds.Faculty_PS && w.Password == adminCreds.Faculty_Password).ToList();
            List<FacultDTO> FacultyDetails = new List<FacultDTO>();
            foreach (var item in result)
            {
                FacultyDetails.Add(new FacultDTO()
                {
                    Faculty_PS = item.FacultyPS,
                  Faculty_Password= item.Password,
                });

            }
            return FacultyDetails;

        }
           public List<TraineeDetailsDataTransferObject> FetchTraineeDetails1()
    {
        try
        {
            var result = contxtObj.TraineeInformations.ToList();
            List<TraineeDetailsDataTransferObject> lstOfTraineeDetails = new List<TraineeDetailsDataTransferObject>();
            foreach (var traineeResult in result)
            {
                lstOfTraineeDetails.Add(new TraineeDetailsDataTransferObject()
                {

                    Trainee_PSNum = traineeResult.PSnumber,
                    Trainee_EmailiD = traineeResult.EmailID,
                    CurrentSkillSet = traineeResult.CurrentSkillSet,
                    ExpectedTraining_Phase1 = traineeResult.ExpectedTraining_Phase1,
                    ExpectedTraining_Phase2 = traineeResult.ExpectedTraining_Phase2,
                    TraineeDate = Convert.ToDateTime(traineeResult.Date)
                });

            }
            return lstOfTraineeDetails;
        }
        catch (Exception ex)
        {

            throw;
        }
    }
        public List<TraineeDetailsDataTransferObject> FetchTraineeByPSNum1(string traineePSNum)
        {
            var result = contxtObj.TraineeInformations.Where(w => (w.PSnumber + w.EmailID + w.CurrentSkillSet).ToString().Contains(traineePSNum)).ToList();
            List<TraineeDetailsDataTransferObject> lstOfTraineeDetails = new List<TraineeDetailsDataTransferObject>();
            foreach (var traineeResult in result)
            {
                lstOfTraineeDetails.Add(new TraineeDetailsDataTransferObject()
                {

                    Trainee_PSNum = traineeResult.PSnumber,
                    Trainee_EmailiD = traineeResult.EmailID,
                    CurrentSkillSet = traineeResult.CurrentSkillSet,
                    ExpectedTraining_Phase1 = traineeResult.ExpectedTraining_Phase1,
                    ExpectedTraining_Phase2 = traineeResult.ExpectedTraining_Phase2,
                    TraineeDate = Convert.ToDateTime(traineeResult.Date)
                });

            }
            return lstOfTraineeDetails;
        }
    }

 
}
