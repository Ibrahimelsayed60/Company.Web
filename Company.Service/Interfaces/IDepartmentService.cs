using Company.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.Data.Models;
using Company.Service.Dto;

namespace Company.Service.Interfaces
{
    public interface IDepartmentService
    {
        DepartmentDto GetById(int? id);

        Department Get(int? id);

        IEnumerable<DepartmentDto> GetAll();

        void Add(DepartmentDto employee);

        void Update(Department employee);

        void Delete(Department employee);
    }
}
