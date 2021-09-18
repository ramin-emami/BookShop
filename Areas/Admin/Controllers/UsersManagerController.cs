using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookShop.Areas.Identity.Data;
using BookShop.Classes;
using BookShop.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReflectionIT.Mvc.Paging;

namespace BookShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersManagerController : Controller
    {
        private readonly IApplicationUserManager _userManager;
        private readonly IApplicationRoleManager _roleManager;
        private readonly IConvertDate _converDate;
        public UsersManagerController(IApplicationUserManager userManager, IApplicationRoleManager roleManager, IConvertDate convertDate)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _converDate = convertDate;
        }

        public async Task<IActionResult> Index(string Msg,int page=1,int row=10)
        {
            if(Msg=="Success")
            {
                ViewBag.Alert = "عضویت با موفقیت انجام شد.";
            }

            var PagingModel = PagingList.Create(await _userManager.GetAllUsersWithRolesAsync(), row, page);
            return View(PagingModel);
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
                return NotFound();
            else
            {
                var User = await _userManager.FindUserWithRolesByIdAsync(id);
                if (User == null)
                    return NotFound();
                else
                    return View(User);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
                return NotFound();
            var User =await _userManager.FindUserWithRolesByIdAsync(id);
            if (User == null)
                return NotFound();
            else
            {
                ViewBag.AllRoles = _roleManager.GetAllRoles();
                User.PersianBirthDate = _converDate.ConvertMiladiToShamsi(User.BirthDate, "yyyy/MM/dd");
                return View(User);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UsersViewModel ViewModel)
        {
            if(ModelState.IsValid)
            {
                var User = await _userManager.FindByIdAsync(ViewModel.Id);
                if (User == null)
                    return NotFound();
                else
                {
                    IdentityResult Result;
                    var RecentRoles = await _userManager.GetRolesAsync(User);
                    var DeleteRoles = RecentRoles.Except(ViewModel.Roles);
                    var AddRoles = ViewModel.Roles.Except(RecentRoles);

                    Result= await _userManager.RemoveFromRolesAsync(User,DeleteRoles);
                    if(Result.Succeeded)
                    {
                        Result = await _userManager.AddToRolesAsync(User, AddRoles);
                        if(Result.Succeeded)
                        {
                            User.FirstName = ViewModel.FirstName;
                            User.LastName = ViewModel.LastName;
                            User.Email = ViewModel.Email;
                            User.PhoneNumber = ViewModel.PhoneNumber;
                            User.UserName = ViewModel.UserName;
                            User.BirthDate = _converDate.ConvertShamsiToMiladi(ViewModel.PersianBirthDate);

                            Result = await _userManager.UpdateAsync(User);
                            if(Result.Succeeded)
                            {
                                ViewBag.AlertSuccess = "ذخیره تغییرات با موفقیت انجام شد.";
                            }
                        }
                    }

                    if(Result!=null)
                    {
                        foreach(var item in Result.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                    }
                }
            }

            ViewBag.AllRoles = _roleManager.GetAllRoles();
            return View(ViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
                return NotFound();
            var User = await _userManager.FindByIdAsync(id);
            if (User == null)
                return NotFound();
            else
                return View(User);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> Deleted(string id)
        {
            if (id == null)
                return NotFound();
            var User = await _userManager.FindByIdAsync(id);
            if (User == null)
                return NotFound();
            else
            {
                var Result=await _userManager.DeleteAsync(User);
                if (Result.Succeeded)
                    return RedirectToAction("Index");
                else
                    ViewBag.AlertError = "در حذف اطلاعات خطایی رخ داده است.";

                return View(User);
            }
        }
    }
}