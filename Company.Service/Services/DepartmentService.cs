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
        private readonly IUnitOfWork _unitOfWork;

        //private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            //_departmentRepository = unitOfWork;
        }

        public void Add(Department entity)
        {
            var MappedDepartment = new Department
            {
                Code = entity.Code,
                Name = entity.Name,
                CreatedAt = DateTime.Now
            };

            _unitOfWork.departmentRepository.Add(MappedDepartment);
            _unitOfWork.Complete();
        }

        public void Delete(Department entity)
        {
            _unitOfWork.departmentRepository.Delete(entity);
        }

        public IEnumerable<Department> GetAll()
        {
            //var dept = departmentRepository.GetAll().Where(x=>x.IsDeleted != true);
            var dept = _unitOfWork.departmentRepository.GetAll();
            return dept;
        }

        public Department GetById(int? id)
        {
            if(id is null)
            {
                return null;
            }

            var dept = _unitOfWork.departmentRepository.GetById(id.Value);
            if(dept is null)
            {
                return null;
            }
            return dept;
        }

        public void Update(Department entity)
        {
            var dept = GetById(entity.Id);

            if (dept.Name == entity.Name)
            {
                if (GetAll().Any(x => x.Name == entity.Name))
                {
                    throw new Exception("Duplicated Department Name");
                }
            }
            dept.Name = entity.Name;
            dept.Code = entity.Code;

            _unitOfWork.departmentRepository.Update(dept);
            _unitOfWork.Complete();

        }
    }
}
