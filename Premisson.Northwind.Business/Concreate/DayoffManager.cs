using System;
using System.Collections.Generic;
using Premisson.Northwind.Business.Abstract;
using System.Text;
using Premisson.Northwind.Core.Utils.Response;
using Premisson.Northwind.Entities.DTO;
using Premisson.Northwind.Data.Acces.Abstract;
using Premisson.Northwind.Entities.Concreate;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Linq;
using Premisson.Northwind.Core.Utils.Enums;
using Microsoft.EntityFrameworkCore.Internal;

namespace Premisson.Northwind.Business.Concreate
{
    public class DayoffManager : IDayoffService
    {
        private readonly IDayoffDal _dayoffDal;
        private readonly IDayoffTypeDal _dayoffTypeDal;
        private readonly IUserDepartment _userDepartment;
        private readonly IUserDal _user;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public DayoffManager(IDayoffDal dayoffDal, IDayoffTypeDal dayoffTypeDal, IUserDepartment userDepartment,IUserDal userDal, IHttpContextAccessor httpContextAccessor)
        {
            _dayoffDal = dayoffDal;
            _dayoffTypeDal = dayoffTypeDal;
            _userDepartment = userDepartment;
            _user = userDal;
            _httpContextAccessor = httpContextAccessor;
        }
        public Response<bool> AddPermission(PermissionDto permissionModel)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            Dayoff dayoff = new Dayoff
            {
                DayoffTypeId = permissionModel.DayoffTypeId,
                
                CreatedAt = DateTime.Now,
                Start_Date = permissionModel.Start_Date,
                End_Date = permissionModel.End_Date,
                Dayoff_Location = permissionModel.Dayoff_Location,
                ProxyUserId = permissionModel.ProxyUserId,
                UserId = Convert.ToInt32(userId),
                DayoffDescription = permissionModel.DayoffDescription,
                IsApprove= false
            };
            int ID = dayoff.UserId;

            _dayoffDal.Add(dayoff);
            bool isSucces = _dayoffDal.Complate();
            if (!isSucces)
            {
                return new Response<bool>(false, "Bir Hata Oluştu");
            }
            return new Response<bool>(true);

        }


        public List<PermissionTypeDto> GetPermissions()
        {
            List<DayoffType> dayoffTypes = _dayoffTypeDal.GetList();

            var returnModel = dayoffTypes.Select(x => new PermissionTypeDto
            { 
              Id = x.Id,
              Name = x.Name
            }).ToList();

            return returnModel;
        }
        
        public List<DayoffListDto> GetDayoffList(int page, int limit)
        {
            var query = _dayoffDal.GetQueryable(x => x.User.RoleId == (int)RoleEnums.Personel);

            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

           
            if (!string.IsNullOrEmpty(userId))
            {
                int userID = Convert.ToInt32(userId);
                query = query.Where(x => x.UserId == userID);
            }
            if (limit < 5)
            {
                limit = 3;
            }
            if (page <= 0)
            {
                page = 1;
            }
            if (page == 1)
            {
                query = query.Take(limit);
            }
            else
                query = query.Skip((page - 1) * limit).Take(limit);

            var returnModel = query.Select(x => new DayoffListDto
            {
                Id = x.Id,
                DayoffTypeId = x.DayoffTypeId,
                DayoffTypeName = x.DayoffType.Name,
                Start_Date = x.Start_Date,
                End_Date = x.End_Date,
                Dayoff_Location = x.Dayoff_Location,
                ProxyUser_Id = x.ProxyUserId,
                Dayoff_Description = x.DayoffDescription,
                IsApprove = x.IsApprove
            }).ToList();

            return returnModel;
        }

        public Response<bool> UpdateDayoff(UpdateDayoffDto updateModel)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var dayoff = _dayoffDal.Get(x => x.Id == updateModel.Id);

            dayoff.DayoffTypeId = updateModel.DayoffTypeId;
            dayoff.Start_Date = updateModel.Start_Date;
            dayoff.End_Date = updateModel.End_Date;
            dayoff.Dayoff_Location = updateModel.Dayoff_Location;
            dayoff.ProxyUserId = updateModel.ProxyUserId;
            //dayoff.UserId = Convert.ToInt32(userId);
            //dayoff.UserId = 6;
            _dayoffDal.Update(dayoff);

            bool isSucces = _dayoffDal.Complate();
            if (!isSucces)
            {
                return new Response<bool>(false, "Bir hata oluştu");
            }
            return new Response<bool>(true);
        }

        public Response<bool> DeleteDayoff(int dayoffId)
        {
            Dayoff dayoff = _dayoffDal.Get(X => X.Id == dayoffId);
            _dayoffDal.Delete(dayoff);

            bool isSucces = _dayoffDal.Complate();
            if (!isSucces)
            {
                return new Response<bool>(false, "Bir Hata Oluştu");
            }
            return new Response<bool>(true);
        }

        public List<DayoffListDto> GetPermissionList(int page, int limit)
        {
            var query = _dayoffDal.GetQueryable();
            var departmentIdStr = _httpContextAccessor.HttpContext.User.FindFirstValue("DepartmentId");

            int departmentId = Convert.ToInt32(departmentIdStr);
            
            query = _dayoffDal.GetQueryable(x => x.User.UserDepartment.DepartmentId == departmentId);

            if (limit < 5)
            {
                limit = 3;
            }
            if (page <= 0)
            {
                page = 1;
            }
            if (page == 1)
            {
                query = query.Take(limit);
            }
            else
                query = query.Skip((page - 1) * limit).Take(limit);


            var returnModel = query.Select(x => new DayoffListDto
            {
                
                Id = x.Id,
                DayoffTypeId = x.DayoffTypeId,
                Name = x.User.Name,
                Surname = x.User.Surname,
                DayoffTypeName = x.DayoffType.Name,
                Start_Date = x.Start_Date,
                End_Date = x.End_Date,
                Dayoff_Location = x.Dayoff_Location,
                ProxyUser_Id = x.ProxyUserId,
                Dayoff_Description = x.DayoffDescription,
                IsApprove = x.IsApprove
            }).ToList();

            return returnModel;
        }

        public Response<bool> Approve(DayoffConfirmDto model)
        {
            Dayoff dayoff = _dayoffDal.Get(x => x.Id == model.DayoffId);
            dayoff.IsApprove = model.IsApprove;


            _dayoffDal.Update(dayoff);
            bool isSuccess = _dayoffDal.Complate();
            if (!isSuccess)
            {
                return new Response<bool>(false, "Bir Hata Oluştu");
            }
            return new Response<bool>(true);

        }

        //public Response<bool> Approve(int id)
        //{
        //    Dayoff dayoff = _dayoffDal.Get(x => x.Id == id);
        //    dayoff.IsApprove = true;


        //    _dayoffDal.Update(dayoff);
        //    bool isSuccess = _dayoffDal.Complate();
        //    if (!isSuccess)
        //    {
        //        return new Response<bool>(false, "Bir Hata Oluştu");
        //    }
        //    return new Response<bool>(true);

        //}

        public Response<bool> Reject(int id)
        {
            Dayoff dayoff = _dayoffDal.Get(x => x.Id == id);
            dayoff.IsApprove = false;

            _dayoffDal.Update(dayoff);

            bool isSuccess = _dayoffDal.Complate();
            if (!isSuccess)
            {
                return new Response<bool>(false, "Bir Hata Oluştu");
            }
            return new Response<bool>(true);
        }
    }
}
