using System;
using Domain.DTO;
using Domain.Entities;
using ProfileData.Domain.Abstractions.Services;

namespace Domain.Abstractions.Services
{
	public interface IRoleService: IService<RoleDTO>
    {
        public RoleDTO Get(int id);
        public void Remove(int id);

    }
}

