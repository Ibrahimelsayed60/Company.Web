using Company.Data.Models;
using Company.Repository.Interfaces;
using Company.Service.Dto;
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

        public void Add(DepartmentDto entity)
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

        public void Delete(DepartmentDto entity)
        {
            Department dept = new Department
            {
                Name = entity.Name,
                Code = entity.Code,
                CreatedAt = DateTime.Now,
                Id = entity.Id
            };
            _unitOfWork.departmentRepository.Delete(dept);
        }

        public IEnumerable<DepartmentDto> GetAll()
        {
            //var dept = departmentRepository.GetAll().Where(x=>x.IsDeleted != true);
            var dept = _unitOfWork.departmentRepository.GetAll();

            var MappedDept = dept.Select(x => new DepartmentDto
            {
                Code=x.Code,
                Name=x.Name,
                Id=x.Id
            });

            return MappedDept;
        }

        public DepartmentDto GetById(int? id)
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
            DepartmentDto deptDto = new DepartmentDto
            {
                Id = dept.Id,
                Code = dept.Code,
                Name = dept.Name,
            };
            return deptDto;
        }

        public void Update(DepartmentDto entity)
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
