using Antlr.Runtime.Misc;
using Asp.Net_MVC_GetSavvi.Models;
using Entities.Models;
using ServiceLayer.IService;
using ServiceLayer.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using static System.Net.Mime.MediaTypeNames;
using System.Web.UI.WebControls;
using Microsoft.Ajax.Utilities;
using System.Text;

namespace Asp.Net_MVC_GetSavvi.Controllers
{
    public class LoginController : Controller
    {
        LoginService _loginService = new LoginService();


        public LoginController()
        {
           
        }
        public LoginController(LoginService loginService)
        {
            _loginService = loginService;
          
        }
        


        // GET: Login
        public ActionResult Index()
        {
            
           
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Index(LoginModel loginModel)
        {

            if (ModelState.IsValid)
            {
                loginModel.FailedLoginAttempts = 0;
                var loginCredentials = await _loginService.GetLoginCredentialByUsername(loginModel.UserName);

                // to calculate attempts 
               /// loginModel.FailedLoginAttempts < 3
                if (loginCredentials != null && loginCredentials.Password == loginModel.Password  )
                {

                    int loginId = loginCredentials.loginId;
                    Session["LoginId"] = loginId;

                    // Valid login, redirect to a page
                    return RedirectToAction("DisplayCredentials", "Login");
                }
                else
                {
                    // Failed login
                    if (loginCredentials != null)
                    {
                   //   loginCredentials.FailedLoginAttempts++;
                       _loginService.UpdateLoggedUser(loginCredentials);
                    }

                    ModelState.AddModelError("", "Invalid username or password.");

                    //if (loginCredentials != null && loginCredentials.FailedLoginAttempts >= 3)
                    //{                       
                    //}
                }


            }
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> DisplayCredentials()
        {
            var data = await  _loginService.GetAll();
            int loginId = (int)Session["LoginId"];
            ViewBag.LoginId = loginId;

            return View(data);
        }
        [HttpGet]
        public ActionResult Register()
        {
           
            return View();
        }
        [HttpPost]
        public ActionResult Register(LoginModel login, string confirmPassword)
        {

            LoginCredentials loginEntity = new LoginCredentials();

            if (ModelState.IsValid)
            {
                if (login.Password == confirmPassword)
                {
                    loginEntity.UserName = login.UserName;

                    // Save password Base64 encryption 

                    string originalText = login.Password;
                    byte[] bytesToEncode = Encoding.UTF8.GetBytes(originalText);
                    string encodedText = Convert.ToBase64String(bytesToEncode);
                    loginEntity.Password = encodedText;

                    _loginService.Insert(loginEntity);

                }
                else {

                    ViewBag.ErrorMessage = "Password does not match";
                    return View();
                }         
            }
            return RedirectToAction("Index");
        }

        [HttpPut]
        [Route("{id:int}")]

        public async Task<ActionResult> Edit(int id, LoginModel loginModel)
        {


            
            if (id != loginModel.loginId)
            {
                return View();
            }

            LoginCredentials loginEntity = new LoginCredentials();

            if (ModelState.IsValid)
            {
                
                    loginEntity.UserName = loginModel.UserName;
                    loginEntity.Password = loginModel.Password;
                     await _loginService.Update(id, loginEntity);

                
            }
            return RedirectToAction("Index");


        }
        
    }
}