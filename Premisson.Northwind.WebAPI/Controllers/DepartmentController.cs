using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    public class DepartmentController : ControllerBase
    {
        private readonly IDeparmentService _deparmentService;
        public DepartmentController(IDeparmentService deparmentService)
        {
            _deparmentService = deparmentService;
        }

        [HttpGet("departments")]
        [Authorize]
        public ActionResult GetDepartment()
        {
            var department = _deparmentService.GetDepartments();

            return Ok(department);
        }
    }
}
