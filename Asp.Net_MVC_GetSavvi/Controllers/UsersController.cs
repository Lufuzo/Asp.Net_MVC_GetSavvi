using Asp.Net_MVC_GetSavvi.Models;
using Entities.Models;
using ServiceLayer.IService;
using ServiceLayer.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Asp.Net_MVC_GetSavvi.Controllers
{
    public class UsersController : Controller
    {
        UserService _userService = new UserService();

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

            // useModel.loginId = 1;

            if (ModelState.IsValid)
            {

                user.Name = useModel.Name;
                user.Surname = useModel.Surname;
                user.IdNumber = useModel.IdNumber;
                user.Email = useModel.Email;
                user.Mobile = useModel.Mobile;
                user.loginId = useModel.loginId;

                _userService.Insert(user);
            }

            return RedirectToAction("IndexDisplay");
        }

        #region CRUD To be completed

        [HttpPut]
        public ActionResult Edit(int id)
        {

            return RedirectToAction("IndexDisplay");
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {

            return RedirectToAction("IndexDisplay");
        }
        #endregion
    }
}