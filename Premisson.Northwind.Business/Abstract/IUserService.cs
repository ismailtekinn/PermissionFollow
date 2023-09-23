using Premisson.Northwind.Entities.Concreate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Premisson.Northwind.Business.Abstract
{
    public interface IUserService
    {
        void Login(User user);
    }
}
