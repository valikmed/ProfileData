﻿using System;
using Domain.DTO;
using Domain.Entities;
using ProfileData.Domain.Abstractions.Services;

namespace Domain.Abstractions.Services
{
	public interface IUserService:IService<UserFullInfoDTO>
	{
        void Remove(Guid id);
        UserFullInfoDTO Get(Guid id);
        public List<UserShortInfoDTO> GetAllShortInfo();
        public UserFullInfoDTO Add(UserFullInfoDTO entity, string hashedPassword);
    }
}

