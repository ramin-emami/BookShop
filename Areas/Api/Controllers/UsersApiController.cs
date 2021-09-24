﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookShop.Areas.Api.Classes;
using BookShop.Areas.Identity.Data;
using BookShop.Classes;
using BookShop.Models.Repository;
using BookShop.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Areas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiResultFilter]
    public class UsersApiController : ControllerBase
    {
        private readonly IApplicationUserManager _userManager;
        private readonly IUsersRepository _usersRepository;
        public UsersApiController(IApplicationUserManager userManager, IUsersRepository usersRepository)
        {
            _userManager = userManager;
            _usersRepository = usersRepository;
        }

        [HttpGet]
        public async Task<ApiResult<List<UsersViewModel>>> Get()
        {
            return Ok(await _userManager.GetAllUsersWithRolesAsync());
        }

        [HttpGet("{id}")]
        //[HttpGet("[action]")]
        public async Task<ApiResult<UsersViewModel>> Get(string id)
        {
            var User = await _userManager.FindUserWithRolesByIdAsync(id);
            if (User == null)
                return NotFound();
            else
                return Ok(User);
        }

        [HttpPost("[action]")]
        //[HttpPost]
        public async Task<ApiResult> Register(RegisterBaseViewModel ViewModel)
        {
            var result = await _usersRepository.RegisterAsync(ViewModel);
            if (result.Succeeded)
            {
                return BadRequest("عضویت با موفقیت انجام شد.");
            }

            else
                return BadRequest(result.Errors);
        }

        [HttpPost("[action]")]
        //[HttpPost]
        public async Task<ApiResult<string>> SignIn(SignInBaseViewModel ViewModel)
        {
            var User = await _userManager.FindByNameAsync(ViewModel.UserName);
            if (User == null)
                return BadRequest("کاربری با این ایمیل یافت نشد.");
            else
            {
                var result = await _userManager.CheckPasswordAsync(User, ViewModel.Password);
                if (result)
                    return Ok("احراز هویت با موفقیت انجام شد.");
                else
                    return BadRequest("نام کاربری یا کلمه عبور شما صحیح نمی باشد.");
            }

        }
    }
}