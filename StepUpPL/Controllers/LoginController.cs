using StepUpBL;
using StepUpDTO;
using StepUpPL.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Protocols.WSTrust;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using System.Web.Services.Description;
using PagedList;
using PagedList.Mvc;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Data;
using System.IO;
using ClosedXML.Excel;
using StepUpDAL;

namespace StepUpPL.Controllers
{
    [Authorize]
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
    public class LoginController : Controller
    {
        StepUpBusinessLayer blObj;
        public LoginController()
        {
            blObj = new StepUpBusinessLayer();
        }
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult FacultyTrainee()
        {
            return View("FacultyTrainee");
        }
     
        [HttpGet]
        public ActionResult FetchTrainee(int? page)
        {
            try
            {
                List<TraineeDetailsDataTransferObject> lsttrainee = blObj.GetTraineeDetails();
                List<Trainee> traineeMvc = new List<Trainee>();
                foreach (var item in lsttrainee)
                {
                    Trainee lstMvc = new Trainee();
                    lstMvc.Trainee_PSNum = item.Trainee_PSNum;
                    lstMvc.Trainee_EmailiD = item.Trainee_EmailiD;
                    lstMvc.CurrentSkillSet = item.CurrentSkillSet;
                    lstMvc.ExpectedTraining_Phase1 = item.ExpectedTraining_Phase1;
                    lstMvc.ExpectedTraining_Phase2 = item.ExpectedTraining_Phase2;
                    lstMvc.TraineeDate = item.TraineeDate;
                    traineeMvc.Add(lstMvc);
                }
                return View(traineeMvc.ToPagedList(page ?? 1, 6));
            }
            catch (Exception)
            {

                return View("Error");
            }

        }
        [HttpGet]
        public ActionResult FetchLogin()
        {
            try
            {
                List<LoginCredentials> lsttrainee = blObj.GetLoginDetails();
                List<LoginModel> traineeMvc = new List<LoginModel>();
                foreach (var item in lsttrainee)
                {
                    LoginModel lstMvc = new LoginModel();
                    lstMvc.loginId = item.loginId;
                    lstMvc.password = item.password;

                    traineeMvc.Add(lstMvc);
                }
                return View(traineeMvc);
            }
            catch (Exception)
            {

                return View("Error");
            }

        }
        [NonAction]
        public void SendTrainee( string email,string training)
        {
            
            var fromEmail = new MailAddress("hariharan18.12.1999@gmail.com", "StepUp");
            var toEmail = new MailAddress(email);
            var fromEmailPassword = "Stepup@54321"; 
            string subject = "Training Details";

            string body = "Your Next set of Training is on : "+training;

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);
        }




        public ActionResult AddTraineeDetails()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {

                return View("Error");
            }
        }



        [HttpPost]
        public ActionResult AddTraineeDetails(AddDetails1 fromUI)
        {
            try
            {

                bool Status = false;
                string text = "";
                TraineeDetailsDataTransferObject traineeDtoObject = new TraineeDetailsDataTransferObject();
                traineeDtoObject.Trainee_PSNum = fromUI.Trainee_PSNum;
                traineeDtoObject.Trainee_EmailiD = fromUI.Trainee_EmailiD;
                traineeDtoObject.CurrentSkillSet = fromUI.CurrentSkillSet;
                traineeDtoObject.ExpectedTraining_Phase1 = fromUI.ExpectedTraining_Phase1;
                traineeDtoObject.ExpectedTraining_Phase2 = fromUI.ExpectedTraining_Phase2;
                traineeDtoObject.TraineeDate = DateTime.Now;
                TempData["exp"] = traineeDtoObject.ExpectedTraining_Phase1.ToString();
                int returnValue = blObj.AddTraineeDetails(traineeDtoObject);
                if (returnValue == 1)
                {
                    string email = fromUI.Trainee_EmailiD;
                    String Training = fromUI.ExpectedTraining_Phase1;
                    SendTrainee(email,Training);

                    text = "Trainee details Added ";
                    ViewBag.Message=text;
                    return RedirectToAction("Drop");
                   
                }
                else
                {
                    ViewBag.Message = "Entered PS number already Exists";
                    return View("AddTraineeDetails");
                }
            }
            catch (Exception ex)
            {
            
                return View("AddTraineeDetails");
            }
        }

        public ActionResult AddLoginDetails()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }
        [HttpPost]
        public ActionResult AddLoginDetails(LoginModel1 fromUI)
        {
            try
            {
                LoginCredentials loginDtoObject = new LoginCredentials();
                loginDtoObject.loginId = fromUI.loginId;
                loginDtoObject.email = fromUI.email;
                loginDtoObject.password = CreatePassword(7);

                int returnValue = blObj.AddLoginDetails(loginDtoObject);
                if (returnValue == 1)
                {
                    string email=fromUI.email;
                    string id = fromUI.loginId;
                    string password=fromUI.password;
                    SendLogin(email,id,password);
                    ViewBag.Message = "Added Successfully";
                    return RedirectToAction("AddLoginDetails");
                }
                else
                {
                    ViewBag.Message = "Entered ID already Exists";
                    return View("AddLoginDetails");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Invalid Data";
                return View("AddLoginDetails");
            }
        }
        public string CreatePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }
        [NonAction]
        public void SendLogin(string email,string id,string password)
        {

            var fromEmail = new MailAddress("hariharan18.12.1999@gmail.com", "IT Support");
            var toEmail = new MailAddress(email);
            var fromEmailPassword = "Stepup@54321";
            string subject = "Login Credintials";

            string body = "Login ID : "+id+"<br></br>"+"Login Password : "+password;

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);
        }
        public ActionResult RaiseTKT()
        {
            
            return View("RaiseTKT");
        }
        [HttpGet]
        public ActionResult UpdatePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UpdatePassword(LoginModel fromUI)
        {
            try
            {
                LoginCredentials loginDtoObject = new LoginCredentials();
                loginDtoObject.loginId = fromUI.loginId;
                loginDtoObject.email = fromUI.email;
                loginDtoObject.password = fromUI.password;

                int returnValue = blObj.UpdatePasswordDetails(loginDtoObject);
                if (returnValue == 1)
                {
                    
                    return RedirectToAction("FetchTrainee");
                }
                else
                {
                    return View("Error");
                }
            }
            catch (Exception ex)
            {

                return View("Error");
            }
        }

        public ActionResult Test()
        {
            return View("Test");
        }
        [HttpGet]
        public ActionResult FetchByName(string traineePSNum )
        {
            try
            {
                List<TraineeDetailsDataTransferObject> lsttrainee = blObj.GetTraineeByPSNum(traineePSNum);
                List<Trainee> traineeMvc = new List<Trainee>();
                foreach (var item in lsttrainee)
                {
                    Trainee lstMvc = new Trainee();
                    lstMvc.Trainee_PSNum = item.Trainee_PSNum;
                    lstMvc.Trainee_EmailiD = item.Trainee_EmailiD;
                    lstMvc.CurrentSkillSet = item.CurrentSkillSet;
                    lstMvc.ExpectedTraining_Phase1 = item.ExpectedTraining_Phase1;
                    lstMvc.ExpectedTraining_Phase2 = item.ExpectedTraining_Phase2;
                    lstMvc.TraineeDate = item.TraineeDate;
                    traineeMvc.Add(lstMvc);
                }
                return View(traineeMvc);
            }
            catch (Exception)
            {

                return View("Error");
            }

        }
        public ActionResult BulkUpload()
        {
            return View();
        }
        [HttpPost]
        public ActionResult BulkUpload(HttpPostedFileBase file)
        {
            try
            {
                DataTable dt = new DataTable();
                //Checking file content length and Extension must be .xlsx  
                if (file != null && file.ContentLength > 0 && System.IO.Path.GetExtension(file.FileName).ToLower() == ".xlsx")
                {
                    string path = Path.Combine(Server.MapPath("~/Excel"), Path.GetFileName(file.FileName));
                    //Saving the file  
                    file.SaveAs(path);
                    //Started reading the Excel file.  
                    using (XLWorkbook workbook = new XLWorkbook(path))
                    {
                        IXLWorksheet worksheet = workbook.Worksheet(1);
                        bool FirstRow = true;
                        //Range for reading the cells based on the last cell used.  
                        string readRange = "1:1";
                        foreach (IXLRow row in worksheet.RowsUsed())
                        {
                            //If Reading the First Row (used) then add them as column name  
                            if (FirstRow)
                            {
                                //Checking the Last cellused for column generation in datatable  
                                readRange = string.Format("{0}:{1}", 1, row.LastCellUsed().Address.ColumnNumber);
                                foreach (IXLCell cell in row.Cells(readRange))
                                {
                                    dt.Columns.Add(cell.Value.ToString());

                                }
                                FirstRow = false;
                            }
                            else
                            {
                                //Adding a Row in datatable  
                                dt.Rows.Add();
                                int cellIndex = 0;
                                //Updating the values of datatable  
                                foreach (IXLCell cell in row.Cells(readRange))
                                {
                                    dt.Rows[dt.Rows.Count - 1][cellIndex] = cell.Value.ToString();
                                    cellIndex++;
                                }
                            }
                        }
                        //If no data in Excel file  
                        if (FirstRow)
                        {
                            ViewBag.Message = "Empty Excel File!";
                        }
                    }
                }
                else
                {
                    //If file extension of the uploaded file is different then .xlsx  
                    ViewBag.Message = "Excel File not Selected";

                }
                string conString = string.Empty;

                conString = ConfigurationManager.ConnectionStrings["SetUpConnectionString"].ConnectionString;

                using (SqlConnection con = new SqlConnection(conString))
                {

                    using (SqlBulkCopy bulk = new SqlBulkCopy(con))
                    {
                        
                        bulk.DestinationTableName = "[TraineeProgram].[dbo].[TraineeInformation]";

                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            bulk.ColumnMappings.Add(i, i);
                        }

                        con.Open();
                        bulk.WriteToServer(dt);
                        con.Close();
                       
                    }
                   
                }
           
                return View("BulkUpload");
            }
            catch (Exception)
            {
                ViewBag.Message = "Excel Has Duplicate or Non Unique Data";
                return View("BulkUpload");
            }



        }
        public ActionResult Drop()
        {
            List<SelectListItem> FacultyList = GetFaculty();
            return View(FacultyList);
        }
        [HttpPost]
        public ActionResult Drop(string ddlFaculty)
        {
            List<SelectListItem> customerList = GetFaculty();
            if (!string.IsNullOrEmpty(ddlFaculty))
            {

          

                string mailed = ddlFaculty.ToString();
                SendTrainee1(mailed);



            }

            return RedirectToAction("AddTraineeDetails");
        }
        public List<SelectListItem> GetFaculty()
        {
            string track = (string)TempData["exp"];

            TraineeProgramContext entities = new TraineeProgramContext();
           
            List<SelectListItem> FacultyLst = (from p in entities.Faculties.AsEnumerable()
                                                 where p.Track == track
                                                 select new SelectListItem
                                                 {

                                                     Text = p.FacultyName,
                                                     Value = p.EmailID,
                                             
                                                 }).ToList();


            //Add Default Item at First Position.
            FacultyLst.Insert(0, new SelectListItem { Text = "--Select faculty--", Value = "" });
            return FacultyLst;
        }
        [NonAction]
        public void SendTrainee1(string ValueS)
        {

            var fromEmail = new MailAddress("hariharan18.12.1999@gmail.com", "StepUp");
            var toEmail = new MailAddress(ValueS);
            var fromEmailPassword = "Stepup@54321";
            string subject = "Training Details";

            string body = "Trainee Details";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);
        }
        public ActionResult AddFacultyDetails()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {

                return View("Error");
            }
        }



        [HttpPost]
        public ActionResult AddFacultyDetails(FacultiesDTO fromUI)
        {
            try
            {

                bool Status = false;
                string message = "";
                FacultDTO facultyDtoObject = new FacultDTO();
                facultyDtoObject.Faculty_PS = fromUI.Faculty_PS;
                facultyDtoObject.Faculty_Name = fromUI.Faculty_Name;
                facultyDtoObject.Faculty_Email = fromUI.Faculty_Email;
                facultyDtoObject.Faculty_Track = fromUI.Faculty_Track;
                facultyDtoObject.Faculty_Password = CreatePassword(7);
               
            
                int returnValue = blObj.AddFacultyDetails(facultyDtoObject);
                if (returnValue == 1)
                {
                    string email = fromUI.Faculty_Email;
                    string id = fromUI.Faculty_PS.ToString();
                    string password = facultyDtoObject.Faculty_Password;
                    SendLogin(email, id, password);

                    return RedirectToAction("AddFacultyDetails");

                }
                else
                {
                    ViewBag.Message = "Ps Number Already Exists";
                    return View("AddFacultyDetails");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Invalid Data";

                return View("AddFacultyDetails");
            }
        }
        [HttpGet]
        public ActionResult FetchTrainee1(int? page)
        {
            try
            {
                List<TraineeDetailsDataTransferObject> lsttrainee = blObj.GetTraineeDetails1();
                List<Trainee> traineeMvc = new List<Trainee>();
                foreach (var item in lsttrainee)
                {
                    Trainee lstMvc = new Trainee();
                    lstMvc.Trainee_PSNum = item.Trainee_PSNum;
                    lstMvc.Trainee_EmailiD = item.Trainee_EmailiD;
                    lstMvc.CurrentSkillSet = item.CurrentSkillSet;
                    lstMvc.ExpectedTraining_Phase1 = item.ExpectedTraining_Phase1;
                    lstMvc.ExpectedTraining_Phase2 = item.ExpectedTraining_Phase2;
                    lstMvc.TraineeDate = item.TraineeDate;
                    traineeMvc.Add(lstMvc);
                }
                return View(traineeMvc.ToPagedList(page ?? 1, 6));
            }
            catch (Exception)
            {

                return View("Error");
            }

        }
        [HttpGet]
        public ActionResult FetchByName1(string traineePSNum)
        {
            try
            {
                List<TraineeDetailsDataTransferObject> lsttrainee = blObj.GetTraineeByPSNum(traineePSNum);
                List<Trainee> traineeMvc = new List<Trainee>();
                foreach (var item in lsttrainee)
                {
                    Trainee lstMvc = new Trainee();
                    lstMvc.Trainee_PSNum = item.Trainee_PSNum;
                    lstMvc.Trainee_EmailiD = item.Trainee_EmailiD;
                    lstMvc.CurrentSkillSet = item.CurrentSkillSet;
                    lstMvc.ExpectedTraining_Phase1 = item.ExpectedTraining_Phase1;
                    lstMvc.ExpectedTraining_Phase2 = item.ExpectedTraining_Phase2;
                    lstMvc.TraineeDate = item.TraineeDate;
                    traineeMvc.Add(lstMvc);
                }
                return View(traineeMvc);
            }
            catch (Exception)
            {

                return View("Error");
            }

        }

        public JsonResult CheckUsernameAvailability(string userdata)
        {
            TraineeProgramContext entities = new TraineeProgramContext();
            System.Threading.Thread.Sleep(100);
            var SeachData = entities.TraineeInformations.Where(x => x.PSnumber.ToString() == userdata).SingleOrDefault();
            if (SeachData != null)
            {
                return Json(1);
            }
            else
            {
                return Json(0);
            }
            
        }
        public JsonResult CheckUsernameAvailability1(string userdata)
        {
            TraineeProgramContext entities = new TraineeProgramContext();
            System.Threading.Thread.Sleep(100);
            var SeachData = entities.Faculties.Where(x => x.FacultyPS.ToString() == userdata).SingleOrDefault();
            if (SeachData != null)
            {
                return Json(1);
            }
            else
            {
                return Json(0);
            }

        }
        public JsonResult CheckUsernameAvailability2(string userdata)
        {
            TraineeProgramContext entities = new TraineeProgramContext();
            System.Threading.Thread.Sleep(100);
            var SeachData = entities.LttsCredintials.Where(x => x.LoginID.ToString() == userdata).SingleOrDefault();
            if (SeachData != null)
            {
                return Json(1);
            }
            else
            {
                return Json(0);
            }

        }
    }
}