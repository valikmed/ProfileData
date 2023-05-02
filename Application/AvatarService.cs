using System;
using System.IO;
using AutoMapper;
using Domain.Abstractions;
using Domain.DTO;
using Domain.Entities;
using SixLabors.ImageSharp;
using Microsoft.AspNetCore.Http;
using ProfileData.Domain.Abstractions.Services;

namespace Infrastructure
{
    public class AvatarService : IAvatarService
    {
        private IAvatarService _service;
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

        public AvatarDTO Upload(IFormFile file)
        {
            try
            {
                if (file.Length > 0)
                {
                    AvatarDTO avatar;
                    using (MemoryStream stream = new MemoryStream())
                    {
                        file.CopyTo(stream);
                        byte[] imageByteArray = stream.ToArray();
                        SixLabors.ImageSharp.Image image = SixLabors.ImageSharp.Image.Load(imageByteArray);
                        image.Mutate(x => x.Resize(256, 256));
                        MemoryStream lightStream = new MemoryStream();
                        image.SaveAsJpeg(lightStream);
                        avatar = _service.Add(new AvatarDTO { Image = lightStream.ToArray() });
                    }
                    return avatar;
                }
                else
                {
                    throw new ArgumentException("File is empty", nameof(file));
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error uploading avatar", ex);
            }
        }
    }
}
