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
using Newtonsoft.Json.Linq;

namespace Asp.Net_MVC_GetSavvi.Controllers
{
     // To Prevent URL ReWrite
    // [Authorize]
    public class LoginController : Controller
    {
      private readonly LoginService _loginService = new LoginService();

        public LoginController()
        {
           
        }
        public LoginController(LoginService loginService)
        {
            _loginService = loginService;
          
        }

        #region CRUD methods

        // GET: Login
        public ActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(LoginCredentialsModel loginModel)
        {

            if (ModelState.IsValid)
            {
                loginModel.FailedLoginAttempts = 0;
                // get crea
                var loginCredentials = await _loginService.GetLoginCredentialByUsername(loginModel.UserName);

                // to calculate attempts 
                /// loginModel.FailedLoginAttempts < 3
                if (loginCredentials != null && loginCredentials.Password == loginModel.Password)
                {

                    // int loginId = loginCredentials.loginId;
                    // Session["LoginId"] = loginId;
                    // Valid login, redirect to a page
                    return RedirectToAction("IndexDisplay", "Users");
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
                    // looking the account after 3 times failling to log

                    //if (loginCredentials != null && loginCredentials.FailedLoginAttempts >= 3)
                    //{                       
                    //}
                }

            }
            return View();
        }

      // [Authorize]
        [HttpGet]
        public async Task<ActionResult> DisplayCredentials()
        {
            var data = await _loginService.GetAll();


            return View(data);
        }
        [HttpGet]
        public ActionResult Register()
        {

            return View();
        }
        // [Authorize]
        [HttpPost]
        public ActionResult Register(LoginCredentialsModel login, string confirmPassword)
        {

            LoginCredentials loginEntity = new LoginCredentials();

            if (ModelState.IsValid)
            {
                if (login.Password == confirmPassword)
                {
                    loginEntity.UserName = login.UserName;

                    // validating password

                    if (login.Password == null || !(login.Password is string password))
                    {

                    }
                    // Check for password length and check if it meets the  (numeric, upper case, lower case)
                    if (login.Password.Length >= 8 &&
                        login.Password.Any(char.IsDigit) &&
                        login.Password.Any(char.IsUpper) &&
                        login.Password.Any(char.IsLower))
                    {
                        // Save password Base64 encryption 
                        // loginEntity.Password = login.Password;
                        string originalText = login.Password;
                        byte[] bytesToEncode = Encoding.UTF8.GetBytes(originalText);
                        string encodedText = Convert.ToBase64String(bytesToEncode);
                        loginEntity.Password = encodedText;

                        _loginService.Insert(loginEntity);
                    }
                }
                else
                {

                    ViewBag.ErrorMessage = "Password does not match";
                    return View();
                }
            }
            return RedirectToAction("DisplayCredentials");
        }


        [HttpGet]
        public ActionResult Edit(int? id)
        {

            if (id.HasValue)
            {
                LoginCredentials log = new LoginCredentials();
                var record = _loginService.GetById(id.Value);

                if (record == null)
                {
                    return View();
                }
                log.UserName = record.UserName;
                log.Password = record.Password;
                string encodedText = log.Password;
                byte[] bytesToDecode = Convert.FromBase64String(encodedText);
                string decodedText = Encoding.UTF8.GetString(bytesToDecode);
                log.Password = decodedText;

                return View(log);
            }
            else
            {
                return View();
            }
            // return View("Edit");
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int? id, LoginCredentialsModel loginModel)
        {

            LoginCredentials log = new LoginCredentials();

            if (id.HasValue)
            {
                log.UserName = loginModel.UserName;
                log.Password = loginModel.Password;

                // encrypting password
                string originalText = log.Password;
                byte[] bytesToEncode = Encoding.UTF8.GetBytes(originalText);
                string encodedText = Convert.ToBase64String(bytesToEncode);
                log.Password = encodedText;


                await _loginService.Update(id.Value, log);
            }

            return RedirectToAction("DisplayCredentials");
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            var record = _loginService.GetById(id);
            return View(record);
        }

        [HttpPost]
        public ActionResult Delete(int id, LoginCredentialsModel loginModel)
        {
            LoginCredentials login = new LoginCredentials();

            login.UserName = loginModel.UserName;
            login.Password = loginModel.Password;
            _loginService.Delete(id, login);


            return RedirectToAction("DisplayCredentials");
        }


        #endregion

    }
}