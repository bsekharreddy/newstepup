using StepUpBL;
using StepUpDTO;
using StepUpPL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace StepUpPL.Controllers
{
    public class SessionController : Controller
    {
        StepUpBusinessLayer blObj;
        public SessionController()
        {
            blObj = new StepUpBusinessLayer();
        }
        // GET: Session
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult LoginDet()
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
        public ActionResult LoginDet(LoginModel loginDetails)
        {
            try
            {
                int result = 0;
                string message = "";

                LoginCredentials loginCreds = new LoginCredentials();
                loginCreds.loginId = loginDetails.loginId;
                loginCreds.password = loginDetails.password;
                List<LoginCredentials> validation = blObj.LoginValidate(loginCreds);
                foreach (LoginCredentials validationItem in validation)
                {
                    if (loginDetails.loginId == validationItem.loginId && loginDetails.password == validationItem.password)
                    {
                        result = 1;
                    }
                    else
                    {
                        result = 0;
                    }
                }
                if (result == 1)
                {
                    FormsAuthentication.SetAuthCookie(loginDetails.loginId, false);
                    message = "Alredy Logged";
                    return RedirectToAction("FetchTrainee", "Login");
                }
                else
                {
                    message = "Invalid ID or Password";
                    ViewBag.Message=message;
                    return View("LoginDet");
                }

            }
            catch (Exception ex)
            {

                return View("Error");
            }
        }
        [HttpGet]
        public ActionResult Admin()
        {
            try
            {
                return View();

            }
            catch (Exception)
            {

                return View("Error");
            }
        }
        [HttpPost]
        public ActionResult Admin(AdminModel AdminDetails)
        {
            try
            {
                int result = 0;

                AdminCredentials adminCreds = new AdminCredentials();
                adminCreds.adminId = AdminDetails.adminId;
                adminCreds.password = AdminDetails.password;
                List<AdminCredentials> validation = blObj.AdminValidate(adminCreds);
                foreach (AdminCredentials validationItem in validation)
                {
                    if (AdminDetails.adminId == validationItem.adminId && AdminDetails.password == validationItem.password)
                    {
                        result = 1;
                    }
                    else
                    {
                        result = 0;
                    }
                }
                if (result == 1)
                {
                    FormsAuthentication.SetAuthCookie(AdminDetails.adminId, false);

                    return RedirectToAction("FetchLogin","Login");
                }
                else
                {
                    ViewBag.Message = "Invalid ID or Password";
                    return View("Admin");
                }

            }
            catch (Exception ex)
            {

                return View("Error");
            }
        }
        public ActionResult logout()
        { 
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("LoginDet");
        }
        [HttpGet]
        public ActionResult Faculty()
        {
            try
            {
                return View();

            }
            catch (Exception)
            {

                return View("Error");
            }
        }
        [HttpPost]
        public ActionResult Faculty(FacultiesDTO1 FacultyDetails)
        {
            try
            {
                int result = 0;

                FacultDTO FacultyCreds = new FacultDTO();
                FacultyCreds.Faculty_PS = FacultyDetails.Faculty_PS;
                FacultyCreds.Faculty_Password = FacultyDetails.Faculty_Password;
                List<FacultDTO> validation = blObj.FacultyValidate(FacultyCreds);
                foreach (FacultDTO validationItem in validation)
                {
                    if (FacultyDetails.Faculty_PS == validationItem.Faculty_PS && FacultyDetails.Faculty_Password== validationItem.Faculty_Password)
                    {
                        result = 1;
                    }
                    else
                    {
                        result = 0;
                    }
                }
                if (result == 1)
                {
                    FormsAuthentication.SetAuthCookie(FacultyDetails.Faculty_PS, false);

                    return RedirectToAction("FetchTrainee1", "Login");
                }
                else
                {
                    ViewBag.Message = "Invalid ID or Password";
                    return View("Faculty");
                }

            }
            catch (Exception ex)
            {
               
                return View("Faculty");
            }
        }
    }
}