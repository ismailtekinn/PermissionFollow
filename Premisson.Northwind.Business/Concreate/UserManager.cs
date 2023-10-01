using Premisson.Northwind.Business.Abstract;
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
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        

        public UserManager(IUserDal userDal, IJwtTokenGenerator jwtTokenGenerator)
        {
            _userDal = userDal;
            _jwtTokenGenerator = jwtTokenGenerator;

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

        public Response<RegisterResponseDto> Register(RegisterDto registerModel)
        {


            int length = 10;
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+";
            var random = new Random();
            string password = new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());

            RegisterResponseDto registerResponseDto = new RegisterResponseDto
            {
                

                Name = user.Name,
                LastName = user.Surname,
                Email = user.Email,
                Password = password,
                RoleId = user.RoleId,
                CreatedAt = DateTime.Now,
                IsActive = false,
                IsDelete = false

            };

            return new Response<RegisterResponseDto>(true, registerResponseDto);
        }
    }
}
