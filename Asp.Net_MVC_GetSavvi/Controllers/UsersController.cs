using Asp.Net_MVC_GetSavvi.Models;
using Entities.Models;
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

        #region Methods
        [HttpPost]
        public JsonResult CheckIDDuplicate(string idNumber)
        {
        

            bool isDuplicate = _userService.IsIdNumberDuplicated(idNumber);

            return Json(new { duplicate = isDuplicate });
        }

        #endregion
        [HttpPost]
        public ActionResult Create(UserModel useModel)
        {

            Users user = new Users();

           //  useModel.loginId = 1;

           if (ModelState.IsValid)
            {

                user.Name = useModel.Name;
                user.Surname = useModel.Surname;
                user.IdNumber = useModel.IdNumber;
                user.Email = useModel.Email;
                user.Mobile = useModel.Mobile;
                //user.loginId = useModel.loginId;

                _userService.Insert(user);
           }

            return RedirectToAction("IndexDisplay");
        }

        #region 

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
                user.Surname = userModel.Surname;
                user.IdNumber = userModel.IdNumber;
                user.Name = userModel.Name;
                user.Mobile = userModel.Mobile;
                user.Email = userModel.Email;
                user.usersId = userModel.usersId;

                _userService.Update(id.Value, user);
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
            _userService.Delete(id,user);

            return RedirectToAction("IndexDisplay");
        }
        #endregion
    }
}