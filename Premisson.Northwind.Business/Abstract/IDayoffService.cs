using Premisson.Northwind.Core.Utils.Response;
using Premisson.Northwind.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Premisson.Northwind.Business.Abstract
{
   public interface IDayoffService
    {
        Response<bool> AddPermission(PermissionDto permissionModel);
        List<PermissionTypeDto> GetPermissions();
        List<DayoffListDto> GetDayoffList(int page, int limit);
        List<DayoffListDto> GetPermissionList(int page, int limit);
        Response<bool> UpdateDayoff(UpdateDayoffDto updateModel);
        Response<bool> DeleteDayoff(int dayoff);
        Response<bool> Approve(DayoffConfirmDto model);
        Response<List<DayoffListDto>> GetDayoffPersonelList(int page, int limit);

    }
}
