using Premisson.Northwind.Data.Acces.Abstract;
using Premisson.Northwind.Entities.Concreate;
using System;
using System.Collections.Generic;
using System.Text;
using Premisson.Northwind.Data.Acces.Concreate.EntityFramework;

using System.Linq.Expressions;
using Premisson.Northwinds.DataAcces.EntityFramework;

namespace Premisson.Northwind.Data.Acces.Concreate.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User>, IUserDal
    {
        public EfUserDal(NorthwindContext context) : base(context)
        {

        }
    }
}
