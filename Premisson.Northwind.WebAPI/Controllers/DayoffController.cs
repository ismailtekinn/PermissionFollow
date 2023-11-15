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
    public class DayoffController : ControllerBase
    {
        private readonly IDayoffService _dayoffService;
        public DayoffController(IDayoffService dayoffService)
        {
            _dayoffService = dayoffService;
        }

        [HttpPost]
        [Route("Add-Permission")]
        public ActionResult AddPermission(PermissionDto permissionDto)
        {
            var response = _dayoffService.AddPermission(permissionDto);
            return Ok(response);
        }

        [HttpPost]
        [Route("Update-Permission")]
        public ActionResult UpdatePermission(UpdateDayoffDto permissionModel)
        {
            var updateResponse = _dayoffService.UpdateDayoff(permissionModel);
            return Ok(updateResponse);
        }

        [HttpGet("Get-Permissions")]
        public ActionResult GetPermission()
        {
            var permissionType = _dayoffService.GetPermissions();
            return Ok(permissionType);
        }

        [HttpGet("dayoff-list")]
        public ActionResult GetDayoffList(int page, int limit)
        {
            var dayoffModel = _dayoffService.GetDayoffList(page, limit);
            return Ok(dayoffModel);
        }

        [HttpDelete("permission-Delete")]
        public ActionResult DeletePermission(int dayoffId)
        {
            var deleteResponse = _dayoffService.DeleteDayoff(dayoffId);
            return Ok(deleteResponse);
        }

        [HttpGet("permission-list")]
        public ActionResult GetPermissionList(int page, int limit)
        {
            var permissionModel = _dayoffService.GetPermissionList(page, limit);
            return Ok(permissionModel);
        }

        [HttpPost("to-approve")]
        public ActionResult ConfirmPermission(DayoffConfirmDto model)
        {
            var response = _dayoffService.Approve(model);
            return Ok(response);

        }
    }
}
