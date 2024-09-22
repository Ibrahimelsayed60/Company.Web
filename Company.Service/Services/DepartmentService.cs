using Company.Data.Models;
using Company.Repository.Interfaces;
using Company.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Service.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public void Add(Department employee)
        {
            _departmentRepository.Add(employee);
        }

        public void Delete(Department employee)
        {
            _departmentRepository.Delete(employee);
        }

        public IEnumerable<Department> GetAll()
        {
            var dept = _departmentRepository.GetAll();
            return dept;
        }

        public Department GetById(int? id)
        {
            if(id is null)
            {
                return null;
            }

            var dept = _departmentRepository.GetById(id.Value);
            if(dept is null)
            {
                return null;
            }
            return dept;
        }

        public void Update(Department employee)
        {
            throw new NotImplementedException();
        }
    }
}
