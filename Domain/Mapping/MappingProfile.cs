using System;
using AutoMapper;
using Domain.Entities;
using Domain.DTO;

namespace Domain.Mapping
{
	public class MappingProfile:Profile
	{
		public MappingProfile()
		{
			CreateMap<User, UserShortInfoDTO>()
			.ForMember(shortInfo => shortInfo.FullName, fullInfo => fullInfo
			.MapFrom(item => item.FirstName + " " + item.LastName + " " + item.MiddleName))

			.ForMember(shortInfo => shortInfo.Age, fullInfo => fullInfo
			.MapFrom(item => User.GetAge(item.BirthDate)))

			.ForMember(shortInfo => shortInfo.Role, fullInfo => fullInfo
			.MapFrom(item => item.Role.RoleName));

			CreateMap<User, UserFullInfoDTO>().ReverseMap();
			CreateMap<Role, RoleDTO>().ReverseMap();
			CreateMap<Avatar, AvatarDTO>().ReverseMap();

        }

    }
}

