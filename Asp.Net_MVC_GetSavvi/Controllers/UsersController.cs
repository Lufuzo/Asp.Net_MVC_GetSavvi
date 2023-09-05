using Asp.Net_MVC_GetSavvi.Models;
using Entities.Models;
using Microsoft.AspNet.Identity;
using ServiceLayer.IService;
using ServiceLayer.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Asp.Net_MVC_GetSavvi.Controllers
{

    //To Prevent URL Rewrite
   // [Authorize]
    public class UsersController : Controller
    {
      private readonly UserService _userService = new UserService();

        public UsersController()
        {

        }

        public UsersController(UserService userService)
        {
            _userService = userService;
        }


        #region CRUD methods

        [HttpGet]
       // GET: Users
        public ActionResult Index()
        {

            return View();
        }

        [HttpGet]
        public async Task<ActionResult> IndexDisplay()
        {
            var data = await _userService.GetAll();


            return View(data);
        }

        [HttpGet]
        public ActionResult Create()
        {

          return View();
        }

        ///[Authorize()]
        [HttpPost]
        public ActionResult Create(UserModel useModel)
        {

            Users user = new Users();

            var result = IsValid(useModel.IdNumber);
            if (user.IsIdExists == false)
            {
                if (result == true)
                {
                    user.Name = useModel.Name;
                    user.Surname = useModel.Surname;
                    user.IdNumber = useModel.IdNumber;
                    user.Email = useModel.Email;

                    //checking the phone number if its valvalid
                    if (useModel.Mobile == null || !(useModel.Mobile is string mobile))
                    {
                        ViewBag.ErrorMessage = "Phone Number is not valid";
                        return View();
                    }
                    // checking the leading zero
                    if (useModel.Mobile.StartsWith("0"))
                    {
                        // replacing leading zero with 27
                        useModel.Mobile = "27" + useModel.Mobile.Substring(1);

                        user.Mobile = useModel.Mobile;
                        _userService.Insert(user);
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = "ID Number is not valid";
                    return View();

                }
               
            }
           
            if (user.IsIdExists == true)
            {
                ViewBag.ErrorMessage = "Id Number already exists in the database ";
                return RedirectToAction("Create");
            }

            return RedirectToAction("IndexDisplay");
        }


        [HttpGet]
        [Route("{id:int}")]
        public ActionResult Update(int? id)
        {
            if (id.HasValue)
            {
                Users us = new Users();
                var record = _userService.Get(id.Value);

                if (record == null)
                {
                    return View();
                }
                us.Surname = record.Surname;
                us.Name = record.Name;
                us.Email = record.Email;
                us.IdNumber = record.IdNumber;
                us.Mobile = record.Mobile;

                return View(us);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Update(int? id, UserModel userModel)
        {

            Users user = new Users();
            if (id.HasValue)
            {

                var result = IsValid(userModel.IdNumber);
                if (result == true)
                {
                    user.Name = userModel.Name;
                    user.Surname = userModel.Surname;
                    user.IdNumber = userModel.IdNumber;
                    user.Email = userModel.Email;

                    //checking the phone number if its valvalid
                    if (userModel.Mobile == null || !(userModel.Mobile is string mobile))
                    {
                        ViewBag.ErrorMessage = "Phone Number is not valid";
                        return View();
                    }
                    // checking the leading zero
                    if (userModel.Mobile.StartsWith("0"))
                    {
                        // replacing leading zero with 27
                        userModel.Mobile = "27" + userModel.Mobile.Substring(1);

                        user.Mobile = userModel.Mobile;
                        _userService.Update(id.Value, user);
                    }
                }
            }

            return RedirectToAction("IndexDisplay");
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var record = _userService.Get(id);
            return View(record);
        }

        [HttpPost]
        public ActionResult Delete(int id, UserModel userModel)
        {
            Users user = new Users();

            user.Surname = userModel.Surname;
            user.IdNumber = userModel.IdNumber;
            user.Name = userModel.Name;
            user.Mobile = userModel.Mobile;
            user.Email = userModel.Email;
            user.usersId = userModel.usersId;
            _userService.Delete(id, user);

            return RedirectToAction("IndexDisplay");
        }

        #endregion

        #region Methods
        [HttpPost]
        public JsonResult CheckIDDuplicate(string idNumber)
        {
        

            bool isDuplicate = _userService.IsIdNumberDuplicated(idNumber);

            return Json(new { duplicate = isDuplicate });
        }
        #endregion



        #region SA ID Validation

        public bool IsValid(object value)
        {
            if (value == null || !(value is string idNumber))
                return false;

            if (idNumber.Length != 13 || !IsDigitsOnly(idNumber))
                return false;

            // Extract birthdate from ID number
            int year = int.Parse(idNumber.Substring(0, 2));
            int month = int.Parse(idNumber.Substring(2, 2));
            int day = int.Parse(idNumber.Substring(4, 2));

            int currentYear = DateTime.Now.Year % 100;
            int currentMonth = DateTime.Now.Month;
            int currentDay = DateTime.Now.Day;

            int birthYear = year + 1900;
            int yVal =  birthYear;

            if (year >= currentYear)
                birthYear += 100;

            DateTime birthdate = new DateTime(yVal, month, day);

            int age = DateTime.Now.Year - birthdate.Year;
            if (birthdate > DateTime.Now.AddYears(-age))
                age--;

            return age >= 18 && age <= 65;
        }

        private bool IsDigitsOnly(string input)
        {
            foreach (char c in input)
            {
                if (!char.IsDigit(c))
                    return false;
            }
            return true;
        }
    }

    #endregion
}
