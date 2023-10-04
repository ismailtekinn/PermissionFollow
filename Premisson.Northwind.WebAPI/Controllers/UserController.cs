﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Premisson.Northwind.Business.Abstract;
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
        public ActionResult GetPersonelList(int page, int limit)
        {
            var personelList = _userService.GerPersonelList(page,limit);
            return Ok(personelList);

        }

        [HttpPost("personel-update")]
        public ActionResult PersonelUpdate(UpdatePersonelDto model)
        {
            var updateResponse = _userService.UpdatePersonel(model);
            return Ok(updateResponse);
        }
    }
}