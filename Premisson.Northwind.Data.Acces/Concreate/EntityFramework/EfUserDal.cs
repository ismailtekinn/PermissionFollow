using Premisson.Northwind.Core.DataAcces.EntityFramework;
using Premisson.Northwind.Data.Acces.Abstract;
using Premisson.Northwind.Entities.Concreate;

using System;
using System.Collections.Generic;
using System.Text;

namespace Premisson.Northwind.Data.Acces.Concreate.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User,NorthwindContext>,IUserDal
    {
    }
}
