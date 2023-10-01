using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Premisson.Northwind.Business.Abstract;
using Premisson.Northwind.Data.Acces.Concreate.EntityFramework;
using Premisson.Northwind.Entities.Concreate;
using Premisson.Northwind.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Premisson.Northwind.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }
        
        [HttpPost]
        [Route("Login")]
        public ActionResult Login(LoginDto model)
        {
            var response = _userService.Login(model);
            return Ok(response);
            
        }

        [HttpPost]
        [Route("Register")]
        public ActionResult Register(RegisterDto register)
        {
            var response = _userService.Register(register);
            return Ok(response);
        }


        [HttpGet("Me")]
        [Authorize]
        public ActionResult Me()
        {
            var firstName = HttpContext.User.FindFirstValue(ClaimTypes.Name);
            var lastName = HttpContext.User.FindFirstValue(ClaimTypes.Surname);
            var roleId = HttpContext.User.FindFirstValue(ClaimTypes.Role);

            MeResponseDto meResponseDto = new MeResponseDto
            {
                FirstName = firstName,
                LastName = lastName,
                RoleId = roleId


            };
            return Ok(meResponseDto);
        }
    }
}
