using Premisson.Northwind.Data.Acces.Abstract;
using Premisson.Northwind.Entities.Concreate;
using Premisson.Northwinds.DataAcces.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Premisson.Northwind.Data.Acces.Concreate.EntityFramework
{
    public class EfDeparmentDal : EfEntityRepositoryBase<Deparment>, IDeparmentDal
    {
        public EfDeparmentDal(NorthwindContext context) : base(context)
        {

        }
    }
}
