using System;
using Domain.DTO;
using ProfileData.Domain.Abstractions.Services;

namespace Domain.Abstractions.Services
{
	public interface IUserService:IService<UserFullInfoDTO>
	{
		public List<UserShortInfoDTO> GetAllShortInfo();
	}
}

