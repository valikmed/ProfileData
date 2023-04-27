using AutoMapper;
using Domain.Abstractions;
using Domain.Abstractions.Services;
using Domain.DTO;
using Domain.Entities;
using Domain.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ProfileData
{
    public class UserService : IUserService
    {
        private IUnitOfWork UnitOfWork;
        private IMapper _mapper;
        private UserValidator userValidator;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            UnitOfWork = unitOfWork;
            userValidator = new UserValidator();
        }
        public List<UserFullInfoDTO> GetAll()
        {
            return UnitOfWork.UserRepository.GetAll()
                .Select(item => _mapper.Map(item, new UserFullInfoDTO()))
                .ToList();
        }
        public List<UserShortInfoDTO> GetAllShortInfo()
        {
            return UnitOfWork.UserRepository.GetAll()
                .Include(user => user.Role)
                .Select(item => _mapper.Map(item, new UserShortInfoDTO()))
                .ToList();
        }

        public UserFullInfoDTO Get(int id)
        {
            var user = UnitOfWork.UserRepository.Get(id);
            return _mapper.Map(user, new UserFullInfoDTO());
        }

        public UserFullInfoDTO Add(UserFullInfoDTO UserDTO)
        {
            userValidator.ValidateAndThrow(UserDTO);
            UnitOfWork.UserRepository.Add(_mapper.Map(UserDTO, new User()));
            return UserDTO;
        }

        public void Remove(int id)
        {
            User user = UnitOfWork.UserRepository.Get(id);
            UnitOfWork.UserRepository.Remove(user.ID);
            if (user.AvatarID != null)
            {
                UnitOfWork.AvatarRepository.Remove((int)user.AvatarID);
            }
        }

        public UserFullInfoDTO Update(UserFullInfoDTO UserDTO)
        {
            userValidator.ValidateAndThrow(UserDTO);
            UnitOfWork.UserRepository.Update(_mapper.Map(UserDTO, new User()));
            return _mapper.Map(UnitOfWork.UserRepository.Get(UserDTO.ID), new UserFullInfoDTO());
        }
    }
}
