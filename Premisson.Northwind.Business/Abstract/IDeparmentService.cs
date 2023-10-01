using Premisson.Northwind.Entities.Concreate;
using Premisson.Northwind.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Premisson.Northwind.Business.Abstract
{
    public interface IDeparmentService
    {
        List<DepartmentDto> GetDepartments();
    }
}
