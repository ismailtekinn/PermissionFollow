using Premisson.Northwind.Business.Abstract;
using Premisson.Northwind.Data.Acces.Abstract;
using Premisson.Northwind.Entities.Concreate;
using Premisson.Northwind.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Premisson.Northwind.Business.Concreate
{
    public class DeparmentManager : IDeparmentService
    {
        private readonly IDeparmentDal _deparmentDal;
        public DeparmentManager(IDeparmentDal deparmentDal)
        {
            _deparmentDal = deparmentDal;
        }

        public List<DepartmentDto> GetDepartments()
        {
            List<Deparment> deparments = _deparmentDal.GetList();

            var returnModel = deparments.Select(x => new DepartmentDto
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();

            return returnModel;

        }
    }
}
