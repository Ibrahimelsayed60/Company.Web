﻿using AutoMapper;
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
        private readonly IMapper _mapper;

        //private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            //_departmentRepository = unitOfWork;
        }

        public void Add(DepartmentDto entity)
        {
            //var MappedDepartment = new Department
            //{
            //    Code = entity.Code,
            //    Name = entity.Name,
            //    CreatedAt = DateTime.Now
            //};
            Department department = _mapper.Map<DepartmentDto,Department>(entity);
            _unitOfWork.departmentRepository.Add(department);
            _unitOfWork.Complete();
        }

        public void Delete(Department entity)
        {
            //Department dept = new Department
            //{
            //    Name = entity.Name,
            //    Code = entity.Code,
            //    CreatedAt = DateTime.Now,
            //    Id = entity.Id
            //};
            //Department dept = _mapper.Map<DepartmentDto,Department>(entity);
            _unitOfWork.departmentRepository.Delete(entity);
            _unitOfWork.Complete();
        }

        public Department Get(int? id)
        {

            var dept = _unitOfWork.departmentRepository.GetById(id.Value);
            if(dept is null)
                return null;
            return dept;
        }

        public IEnumerable<DepartmentDto> GetAll()
        {
            //var dept = departmentRepository.GetAll().Where(x=>x.IsDeleted != true);
            var dept = _unitOfWork.departmentRepository.GetAll();

            //var MappedDept = dept.Select(x => new DepartmentDto
            //{
            //    Code = x.Code,
            //    Name = x.Name,
            //    Id = x.Id
            //});

            //DepartmentDto dto = _mapper.Map<Department, DepartmentDto>(dept.FirstOrDefault());

            IEnumerable<DepartmentDto> result = _mapper.Map< IEnumerable<Department> ,IEnumerable <DepartmentDto>>(dept);
            return result;
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
            //DepartmentDto deptDto = _mapper.Map<DepartmentDto>(dept);
            return deptDto;
        }

        public void Update(Department entity)
        {
            var dept = Get(entity.Id);

            if (dept.Name == entity.Name)
            {
                if (GetAll().Any(x => x.Name == entity.Name))
                {
                    throw new Exception("Duplicated Department Name");
                }
            }
            dept.Name = entity.Name;
            dept.Code = entity.Code;

            //Department dept1 = _mapper.Map<DepartmentDto>(dept);
            _unitOfWork.departmentRepository.Update(dept);
            _unitOfWork.Complete();

        }
    }
}
