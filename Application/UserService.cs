﻿using AutoMapper;
using Domain.Abstractions;
using Domain.Abstractions.Services;
using Domain.DTO;
using Domain.Entities;
using Domain.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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

        public UserFullInfoDTO Get(Guid id)
        {
            var user = UnitOfWork.UserRepository.Get(id);
            return _mapper.Map(user, new UserFullInfoDTO());
        }

        //public UserFullInfoDTO Add(UserFullInfoDTO UserDTO)
        //{
        //    userValidator.ValidateAndThrow(UserDTO);
        //    UnitOfWork.UserRepository.Add(_mapper.Map(UserDTO, new User()));
        //    UnitOfWork.Save();
        //    return UserDTO;
        //}
        public void Remove(Guid id)
        {
            User user = UnitOfWork.UserRepository.Get(id);
            UnitOfWork.UserRepository.Remove(user.ID);
            if (user.AvatarID.HasValue)
            {
                UnitOfWork.AvatarRepository.Remove(user.AvatarID.Value);
            }
            UnitOfWork.Save();
        }



        public UserFullInfoDTO Update(UserFullInfoDTO UserDTO)
        {
            userValidator.ValidateAndThrow(UserDTO);
            UnitOfWork.UserRepository.Update(_mapper.Map(UserDTO, new User()));
            UnitOfWork.Save();
            return _mapper.Map(UnitOfWork.UserRepository.Get(UserDTO.ID), new UserFullInfoDTO());
        }
        public UserFullInfoDTO Add(UserFullInfoDTO userDTO, string hashedPassword)
        {
            userDTO.HashedPassword = hashedPassword;
            userValidator.ValidateAndThrow(userDTO);
            UnitOfWork.UserRepository.Add(_mapper.Map(userDTO, new User()));
            UnitOfWork.Save();
            return userDTO;
        }

        public void SaveUsernameAndPassword(Guid id, string username, string hashedPassword)
        {
            User user = UnitOfWork.UserRepository.Get(id);
            user.Username = username;
            user.HashedPassword = hashedPassword;
            UnitOfWork.UserRepository.Update(user);
            UnitOfWork.Save();
        }
    }
}
