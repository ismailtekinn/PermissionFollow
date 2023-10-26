using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Premisson.Northwind.Business.Abstract;
using Premisson.Northwind.Core.Utils.Constants;
using Premisson.Northwind.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Premisson.Northwind.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("personel-list")]
        [Authorize(Roles = RoleConstants.ADMIN_MANAGER)]
        public ActionResult GetPersonelList(int page, int limit)
        {
            var personelList = _userService.GerPersonelList(page,limit);
            return Ok(personelList);

        }

        [HttpPost("personel-update")]
        [Authorize(Roles = RoleConstants.ADMIN)]

        public ActionResult PersonelUpdate(UpdatePersonelDto model)
        {
            var updateResponse = _userService.UpdatePersonel(model);
            return Ok(updateResponse);
        }
        [HttpDelete("personel-delete")]
        [Authorize(Roles = RoleConstants.ADMIN)]
        public ActionResult DeletePersonel(int userId)
        {
            var deleteResponse = _userService.DeleteUser(userId);
            return Ok(deleteResponse);
        }

        [HttpGet("Get-Users")]
        public ActionResult GetUsers()
        {
            var user = _userService.GetUsers();
            return Ok(user);
        }
        [HttpPost("user-password")]
        public ActionResult UpdatePassword(PasswordDto password)
        {
            var updateModel = _userService.UpdatePassword(password);

            return Ok(updateModel);
        }
    }
}
