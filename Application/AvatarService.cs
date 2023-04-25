using System;
using AutoMapper;
using Domain.Abstractions;
using Domain.DTO;
using Domain.Entities;
using ProfileData.Domain.Abstractions.Services;

namespace Infrastructure
{
    public class AvatarService : IAvatarService
    {
        private IMapper _mapper;
        private IUnitOfWork UnitOfWork;

        public AvatarService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            UnitOfWork = unitOfWork;
        }

        public AvatarDTO Add(AvatarDTO avatarDTO)
        {
            Avatar avatar = UnitOfWork.AvatarRepository.Add(_mapper.Map(avatarDTO, new Avatar()));
            return _mapper.Map(avatar, new AvatarDTO());
        }

        public AvatarDTO Get(int id)
        {
            var user = UnitOfWork.AvatarRepository.Get(id);
            return _mapper.Map(user, new AvatarDTO());
        }

        public List<AvatarDTO> GetAll()
        {
            return UnitOfWork.AvatarRepository.GetAll()
                .Select(item => _mapper.Map(item, new AvatarDTO())).ToList();
        }

        public void Remove(int id)
        {
            UnitOfWork.AvatarRepository.Remove(id);
        }

        public AvatarDTO Update(AvatarDTO avatarDTO)
        {
            UnitOfWork.AvatarRepository.Update(_mapper.Map(avatarDTO, new Avatar()));
            return _mapper.Map(UnitOfWork.AvatarRepository.Get(avatarDTO.ID), new AvatarDTO());
        }
    }
}


