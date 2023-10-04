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
using System.Text;

namespace Premisson.Northwind.Business.Concreate
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;
        private readonly IUserDepartment _userDepartment;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        

        public UserManager(IUserDal userDal, IJwtTokenGenerator jwtTokenGenerator,IUserDepartment userDepartment)
        {
            _userDal = userDal;
            _jwtTokenGenerator = jwtTokenGenerator;
            _userDepartment = userDepartment;

        }

        public Response<LoginResponseDto> Login(LoginDto model)
        {
            User user = _userDal.Get(x => x.Email == model.Email);
            if (user is null)
            {
                return new Response<LoginResponseDto>(false, "Kullanıcı Bulunamadı");
            }
            if (user.Password != model.Password)
            {
                return new Response<LoginResponseDto>(false,"Kullanıcı adı vey şifre hatalı");

            }
            string token = _jwtTokenGenerator.GenerateToken(user);


            LoginResponseDto loginResponseDto = new LoginResponseDto
            {
                FirstName = user.Name,
                LastName = user.Surname,
                RoleId = user.RoleId.ToString(),
                Token = token
            };

            return new Response<LoginResponseDto>(true, loginResponseDto);
        }


        Response<bool> IUserService.Register(RegisterDto registerModel)
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
        //public Response<List<PersonelListDto>> GerPersonelList()
        //{
        //    var users = _userDepartment.GetList(x => x.User.RoleId != (int)RoleEnums.Admin, x => x.User, x => x.User.Role, x => x.Deparment);
        //    var returnModel = users.Select(s => new PersonelListDto
        //    {
        //        Birim = s.Deparment.Name,
        //        CreatedAt= s.User.CreatedAt.ToString("dd/MM/yyyy"),
        //        Email = s.User.Email,
        //        FirstName = s.User.Name,
        //        Id = s.UserId,
        //        IsActive = s.User.IsActive,
        //        IsDelete = s.User.IsDelete ? "Evet" : "---",
        //        LastName = s.User.Surname,
        //        RoleName = s.User.Role.Name
        //    }).ToList();
        //    return new Response<List<PersonelListDto>>(true, returnModel);


        //}

    }
}
