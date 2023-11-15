using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
using System.Security.Cryptography;
using System.Text;

namespace Premisson.Northwind.Business.Concreate
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;
        private readonly IUserDepartment _userDepartment;
        private readonly IDayoffDal _dayoffDal;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserManager(IUserDal userDal, IDayoffDal dayoffDal, IJwtTokenGenerator jwtTokenGenerator, IUserDepartment userDepartment, IHttpContextAccessor httpContextAccessor)
        {
            _userDal = userDal;
            _jwtTokenGenerator = jwtTokenGenerator;
            _userDepartment = userDepartment;
            _httpContextAccessor = httpContextAccessor;
            _dayoffDal = dayoffDal;
        }

        public Response<LoginResponseDto> Login(LoginDto model)
        {
            User user = _userDal.Get(x => x.Email == model.Email);
            if (user is null)
            {
                return new Response<LoginResponseDto>(false, "Kullanıcı Bulunamadı");
            }

            PasswordVerificationResult verifyPassword = VerifyHashedPassword(user.Password, model.Password);
            if (verifyPassword != PasswordVerificationResult.Success)
            {
                return new Response<LoginResponseDto>(false, "Kullanıcı adı veya şifre hatalı");
            }
            UserDepartment ud = _userDepartment.Get(x => x.UserId == user.Id);
            string token = _jwtTokenGenerator.GenerateToken(user, ud?.DepartmentId);

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
            //todo : gerçek senaryoda random şifre oluşturup kullanıcıya mail atılıyor, test için 1234 eklendi
            //int length = 10;
            //string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+";
            //var random = new Random();
            //string password = new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());

            byte[] passwordHashByte = HashPasswordV2("1234");
            string passwordHash = Convert.ToBase64String(passwordHashByte);

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
                Password = passwordHash,
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

        public Response<LoginResponseDto> UpdatePassword(PasswordDto password)
        {
            var user = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            int userId = Convert.ToInt32(user);
            User u = _userDal.Get(x => x.Id == userId);

            byte[] passwordHashByte = HashPasswordV2(password.Password);
            string passwordHash = Convert.ToBase64String(passwordHashByte);

            u.Password = passwordHash;
            u.IsActive = true;

            _userDal.Update(u);
            bool isSuccess = _userDal.Complate();
            if (!isSuccess)
            {
                return new Response<LoginResponseDto>(false, "Bir hata oluştu");
            }

            UserDepartment ud = _userDepartment.Get(x => x.UserId == u.Id);
            string token = _jwtTokenGenerator.GenerateToken(u, ud?.DepartmentId);

            LoginResponseDto loginResponseDto = new LoginResponseDto
            {
                FirstName = u.Name,
                LastName = u.Surname,
                RoleId = u.RoleId.ToString(),
                IsActive = u.IsActive.ToString(),
                Token = token
            };

            return new Response<LoginResponseDto>(true, loginResponseDto);

        }

        public Response<List<MainInformationDto>> GetMainInformation()
        {
            var _roleId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
            var _departmentId = _httpContextAccessor.HttpContext.User.FindFirstValue("DepartmentId");

            int roleId = Convert.ToInt32(_roleId);

            if (roleId != (int)RoleEnums.Personel)
            {
                var userQuery = _userDal.GetQueryable(x => !x.IsDelete);
                var dayoffQuery = _dayoffDal.GetQueryable();
                if (!string.IsNullOrEmpty(_departmentId))
                {
                    int departmentId = Convert.ToInt32(_departmentId);
                    userQuery = userQuery.Where(x => x.UserDepartment.DepartmentId == departmentId);
                }

                int totalCount = userQuery.Count();
                int dayoffCount = dayoffQuery.Where(x => x.End_Date >= DateTime.Now && x.IsApprove.HasValue && x.IsApprove.Value).Count();
                int waitDayoffCount = dayoffQuery.Where(x => x.End_Date >= DateTime.Now && (x.IsApprove.HasValue && !x.IsApprove.Value || !x.IsApprove.HasValue)).Count();

                int currentMonth = DateTime.Now.Month;
                int currentMonthDayoffCount = dayoffQuery.Where(x => x.Start_Date.Month == currentMonth && x.End_Date.Month >= currentMonth && x.IsApprove.HasValue && x.IsApprove.Value).Count();

                List<MainInformationDto> returnModel = new List<MainInformationDto>()
           {
               new MainInformationDto("Mevcut İzinli Sayısı", dayoffCount),
               new MainInformationDto("Toplam Personel", totalCount),
               new MainInformationDto("Onay Bekleyen İzin Talebi", waitDayoffCount),
               new MainInformationDto("Toplam İzin Sayısı / Mevcut Ay", currentMonthDayoffCount),
           };

                return new Response<List<MainInformationDto>>(true, returnModel);
            }
            return new Response<List<MainInformationDto>>(true, new List<MainInformationDto>());
        }

        #region PRIVATE METHODS
        private bool VerifyHashedPasswordV2(byte[] hashedPassword, string password)
        {
            const KeyDerivationPrf Pbkdf2Prf = KeyDerivationPrf.HMACSHA1; // default for Rfc2898DeriveBytes
            const int Pbkdf2IterCount = 1000; // default for Rfc2898DeriveBytes
            const int Pbkdf2SubkeyLength = 256 / 8; // 256 bits
            const int SaltSize = 128 / 8; // 128 bits

            // We know ahead of time the exact length of a valid hashed password payload.
            if (hashedPassword.Length != 1 + SaltSize + Pbkdf2SubkeyLength)
            {
                return false; // bad size
            }

            byte[] salt = new byte[SaltSize];
            Buffer.BlockCopy(hashedPassword, 1, salt, 0, salt.Length);

            byte[] expectedSubkey = new byte[Pbkdf2SubkeyLength];
            Buffer.BlockCopy(hashedPassword, 1 + salt.Length, expectedSubkey, 0, expectedSubkey.Length);

            // Hash the incoming password and verify it
            byte[] actualSubkey = KeyDerivation.Pbkdf2(password, salt, Pbkdf2Prf, Pbkdf2IterCount, Pbkdf2SubkeyLength);
            return CryptographicOperations.FixedTimeEquals(actualSubkey, expectedSubkey);
        }

        private byte[] HashPasswordV2(string password)
        {
            var rng = RandomNumberGenerator.Create();
            const KeyDerivationPrf Pbkdf2Prf = KeyDerivationPrf.HMACSHA1; // default for Rfc2898DeriveBytes
            const int Pbkdf2IterCount = 1000; // default for Rfc2898DeriveBytes
            const int Pbkdf2SubkeyLength = 256 / 8; // 256 bits
            const int SaltSize = 128 / 8; // 128 bits

            // Produce a version 2 (see comment above) text hash.
            byte[] salt = new byte[SaltSize];
            rng.GetBytes(salt);
            byte[] subkey = KeyDerivation.Pbkdf2(password, salt, Pbkdf2Prf, Pbkdf2IterCount, Pbkdf2SubkeyLength);

            var outputBytes = new byte[1 + SaltSize + Pbkdf2SubkeyLength];
            outputBytes[0] = 0x00; // format marker
            Buffer.BlockCopy(salt, 0, outputBytes, 1, SaltSize);
            Buffer.BlockCopy(subkey, 0, outputBytes, 1 + SaltSize, Pbkdf2SubkeyLength);
            return outputBytes;
        }

        private PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            byte[] decodedHashedPassword = Convert.FromBase64String(hashedPassword);

            // read the format marker from the hashed password
            if (decodedHashedPassword.Length == 0)
            {
                return PasswordVerificationResult.Failed;
            }
            if (VerifyHashedPasswordV2(decodedHashedPassword, providedPassword))
            {
                // This is an old password hash format - the caller needs to rehash if we're not running in an older compat mode.
                return PasswordVerificationResult.Success;
            }
            else
            {
                return PasswordVerificationResult.Failed;
            }
        }
        #endregion
    }
}
