using System;
using System.IO;
using AutoMapper;
using Domain.Abstractions;
using Domain.DTO;
using Domain.Entities;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using Microsoft.AspNetCore.Http;
using ProfileData.Domain.Abstractions.Services;
using static System.Net.Mime.MediaTypeNames;
using Application;
using Microsoft.EntityFrameworkCore;

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

        public AvatarDTO Add(AvatarDTO avatarDTO)
        {
            Avatar avatar = UnitOfWork.AvatarRepository.Add(_mapper.Map(avatarDTO, new Avatar()));
            UnitOfWork.Save();
            return _mapper.Map(avatar, new AvatarDTO());
        }
        
        public void Remove(int id)
        {
            UnitOfWork.AvatarRepository.Remove(id);
            UnitOfWork.Save();
        }

        public AvatarDTO Update(AvatarDTO avatarDTO)
        {
            UnitOfWork.AvatarRepository.Update(_mapper.Map(avatarDTO, new Avatar()));
            UnitOfWork.Save();
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
                        avatar = Add(new AvatarDTO { Image = lightStream.ToArray() });
                    }
                    UnitOfWork.Save();
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
