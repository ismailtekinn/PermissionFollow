using Premisson.Northwind.Core.Utils.Response;
using Premisson.Northwind.Entities.Concreate;
using Premisson.Northwind.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Premisson.Northwind.Business.Abstract
{
    public interface IUserService
    {
        Response<LoginResponseDto> Login(LoginDto model);
        Response<bool> Register(RegisterDto registerModel);
        Response<List<PersonelListDto>> GerPersonelList(int page, int limit);
        Response<bool> UpdatePersonel(UpdatePersonelDto updateModel);
        Response<bool> DeleteUser(int userId);
        Response<List<UserDto>> GetUsers();
        Response<LoginResponseDto> UpdatePassword(PasswordDto password);
        Response<List<MainInformationDto>> GetMainInformation();

    }
}
