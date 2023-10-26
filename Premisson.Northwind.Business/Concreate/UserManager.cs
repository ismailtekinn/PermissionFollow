using Microsoft.AspNetCore.Http;
using Premisson.Northwind.Business.Abstract;
using Premisson.Northwind.Core.Utils.Enums;
using Premisson.Northwind.Core.Utils.Response;
using Premisson.Northwind.Core.Utils.Token;
using Premisson.Northwind.Data.Acces.Abstract;
using Premisson.Northwind.Entities.Concreate;
using Premisson.Northwind.Entities.DTO;
using System;

using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Premisson.Northwind.Business.Concreate
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;
        private readonly IUserDepartment _userDepartment;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IHttpContextAccessor _httpContextAccessor;



        public UserManager(IUserDal userDal, IJwtTokenGenerator jwtTokenGenerator, IUserDepartment userDepartment, IHttpContextAccessor httpContextAccessor)
        {
            _userDal = userDal;
            _jwtTokenGenerator = jwtTokenGenerator;
            _userDepartment = userDepartment;
            _httpContextAccessor = httpContextAccessor;

        }

        public Response<LoginResponseDto> Login(LoginDto model)
        {
            User user = _userDal.Get(x => x.Email == model.Email );
            if (user is null)
            {
                return new Response<LoginResponseDto>(false, "Kullanıcı Bulunamadı");
            }
            if (user.Password != model.Password)
            {
                return new Response<LoginResponseDto>(false, "Kullanıcı adı vey şifre hatalı");

            }
            UserDepartment ud = _userDepartment.Get(x => x.UserId== user.Id );

            string token = _jwtTokenGenerator.GenerateToken(user,ud?.DepartmentId);


            LoginResponseDto loginResponseDto = new LoginResponseDto
            {
                FirstName = user.Name,
                LastName = user.Surname,
                RoleId = user.RoleId.ToString(),
                IsActive = user.IsActive.ToString(),
                Token = token
            };

            return new Response<LoginResponseDto>(true, loginResponseDto);
        }


       public Response<bool> Register(RegisterDto registerModel)
        {
            int length = 10;
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+";
            var random = new Random();
            string password = new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());

            int roleId = (int)RoleEnums.Personel;
            if (registerModel.IsManager)
            {
                var existManager = _userDepartment.Get(x => x.User.IsActive && !x.User.IsDelete && x.DepartmentId == registerModel.DepartmentId && x.IsManager);
                if (existManager != null)
                {
                    return new Response<bool>(false, "Bu birimde zaten yönetici bulunmaktadır ! ");
                }
                roleId = (int)RoleEnums.Manager;

            }

            User user = new User
            {
                Email = registerModel.Email,
                Name = registerModel.Name,
                Surname = registerModel.LastName,
                Password = password,
                RoleId = roleId,
                CreatedAt = DateTime.Now,
                IsActive = false,
                IsDelete = false
            };
            _userDal.Add(user);
            bool isSuccess = _userDal.Complate();
            if (!isSuccess)
            {
                return new Response<bool>(false, "Bir hata oluştu");
            }

            UserDepartment userDepartment = new UserDepartment
            {
                DepartmentId = registerModel.DepartmentId,
                IsDelete = false,
                CreatedAt = DateTime.Now,
                IsManager = registerModel.IsManager,
                UserId = user.Id
            };
            _userDepartment.Add(userDepartment);

            isSuccess = _userDal.Complate();
            if (!isSuccess)
            {
                return new Response<bool>(false, "Bir Hata Oluştu");
            }

            return new Response<bool>(true);

        }
        public Response<List<PersonelListDto>> GerPersonelList(int page, int limit)
        {
            //var users = _userDepartment.GetList(x => x.User.RoleId != (int)RoleEnums.Admin, x => x.User, x => x.User.Role, x => x.Deparment);
            //var returnModel = users.Select(s => new PersonelListDto
            //{
            //    Birim = s.Deparment.Name,
            //    CreatedAt = s.User.CreatedAt.ToString("dd/MM/yyyy"),
            //    Email = s.User.Email,
            //    FirstName = s.User.Name,
            //    Id = s.UserId,
            //    IsActive = s.User.IsActive,
            //    IsDelete = s.User.IsDelete ? "Evet" : "---",
            //    LastName = s.User.Surname,
            //    RoleName = s.User.Role.Name
            //}).ToList();

            var query = _userDepartment.GetQueryable(x => x.User.RoleId != (int)RoleEnums.Admin);

            var departmentIdStr = _httpContextAccessor.HttpContext.User.FindFirstValue("DepartmentId");

            if (!string.IsNullOrEmpty(departmentIdStr))
            {
                int departmentId = Convert.ToInt32(departmentIdStr);
                query = query.Where(x => x.DepartmentId == departmentId);
            }

            if (limit < 10)
            {
                limit = 10;
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

            var returnModel = query.Select(s => new PersonelListDto
            {
                Birim = s.Deparment.Name,
                CreatedAt = s.User.CreatedAt.ToString("dd/MM/yyyy"),
                Email = s.User.Email,
                FirstName = s.User.Name,
                Id = s.UserId,
                IsActive = s.User.IsActive,
                IsDelete = s.User.IsDelete ? "Evet" : "---",
                LastName = s.User.Surname,
                RoleName = s.User.Role.Name,
                DepartmentId = s.DepartmentId,
                IsManager = s.IsManager
            }).ToList();
            return new Response<List<PersonelListDto>>(true, returnModel);


        }

        public Response<bool> UpdatePersonel(UpdatePersonelDto updateModel)
        {
            var user = _userDal.Get(x => x.Id == updateModel.Id);

            int roleId = (int)RoleEnums.Personel;
            if (updateModel.IsManager)
            {
                var existManager = _userDepartment.Get(x => x.User.IsActive && !x.User.IsDelete && x.DepartmentId == updateModel.DepartmentId && x.IsManager && x.UserId != updateModel.Id);
                if (existManager != null)
                {
                    return new Response<bool>(false, "Bu birimde zaten yönetici bulunmaktadır ! ");
                }
                roleId = (int)RoleEnums.Manager;
            }

            user.Name = updateModel.Name;
            user.Surname = updateModel.LastName;
            user.Email = updateModel.Email;
            user.RoleId = roleId;
            _userDal.Update(user);

            var userDepartment = _userDepartment.Get(x => x.UserId == updateModel.Id);
            userDepartment.DepartmentId = updateModel.DepartmentId;
            userDepartment.IsManager = updateModel.IsManager;
            _userDepartment.Update(userDepartment);

            bool isSuccess = _userDal.Complate();
            if (!isSuccess)
            {
                return new Response<bool>(false, "Bir hata oluştu");
            }
            return new Response<bool>(true);

        }

        public Response<bool> DeleteUser(int userId)
        {
            User user = _userDal.Get(x => x.Id == userId);
            UserDepartment userDepartment = _userDepartment.Get(x => x.UserId == userId);

            _userDal.Delete(user);
            _userDepartment.Delete(userDepartment);

            bool isSucces = _userDal.Complate();
            if (!isSucces)
            {
                return new Response<bool>(false, "Bir Hata Oluştu");
            }
            return new Response<bool>(true);
        }

        public Response<List<UserDto>> GetUsers()
        {
            

            var query = _userDepartment.GetQueryable(x => x.User.RoleId == (int)RoleEnums.Personel);

            var departmentIdStr = _httpContextAccessor.HttpContext.User.FindFirstValue("DepartmentId");
            

            if (!string.IsNullOrEmpty(departmentIdStr))
            {
                int departmentId = Convert.ToInt32(departmentIdStr);
                query = query.Where(x => x.DepartmentId == departmentId);
            }
            
            var returnModel = query.Select(s => new UserDto
            {
               Id = s.Id,
               Name = s.User.Name,
               LastName = s.User.Surname,
               FullName = s.User.Name + " " + s.User.Surname
               

            }).ToList();

            return new Response<List<UserDto>>(true, returnModel);
        }

        public Response<bool> UpdatePassword(PasswordDto password)
        {
            var user = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            int userId = Convert.ToInt32(user);
            User u = _userDal.Get(x => x.Id == userId);
            u.Password = password.Password;
            u.IsActive = true;

            _userDal.Update(u);
            bool isSuccess = _userDal.Complate();
            if (!isSuccess)
            {
                return new Response<bool>(false, "Bir hata oluştu");
            }
            return new Response<bool>(true);

        }
    }
}
