using AutoMapper;
using Domain.Abstractions;
using Domain.Abstractions.Services;
using Domain.DTO;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProfileData
{
    public class RoleService : IRoleService
    {
        private IUnitOfWork UnitOfWork;
        private IMapper _mapper;

        public RoleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            UnitOfWork = unitOfWork;
        }
        public List<RoleDTO> GetAll()
        {
            return UnitOfWork.RoleRepository.GetAll()
                .Select(item => _mapper.Map(item, new RoleDTO())).ToList();
        }

        public RoleDTO Get(int id)
        {
            var user = UnitOfWork.RoleRepository.Get(id);
            return _mapper.Map(user, new RoleDTO());
        }

        public RoleDTO Add(RoleDTO roleDTO)
        {
            UnitOfWork.RoleRepository.Add(_mapper.Map(roleDTO, new Role()));
            return roleDTO;
        }

        public void Remove(int id)
        {
            UnitOfWork.RoleRepository.Remove(id);
            UnitOfWork.Save();

        }

        public RoleDTO Update(RoleDTO roleDTO)
        {
            UnitOfWork.RoleRepository.Update(_mapper.Map(roleDTO, new Role()));
            UnitOfWork.Save();
            return _mapper.Map(UnitOfWork.RoleRepository.Get(roleDTO.ID), new RoleDTO());
        }
    }
}

