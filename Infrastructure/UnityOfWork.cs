using System;
using Application;
using Application.Repositories;
using Domain.Abstractions;
using Domain.Abstractions.Repositories;
using Infrastructure.Repositories;

namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private IUserRepository userRepository;
        private IRoleRepository roleRepository;
        private IAvatarRepository avatarRepository;


        private ProfileDataContext _context;
        public UnitOfWork(ProfileDataContext context)
        {
            _context = context;
        }
        public IUserRepository UserRepository
        {
            get
            {
                return userRepository ??= new UserRepository(_context);
            }
        }
        public IRoleRepository RoleRepository
        {
            get
            {
                return roleRepository ??= new RoleRepository(_context);
            }
        }
        public IAvatarRepository AvatarRepository
        {
            get
            {
                return avatarRepository ??= new AvatarRepository(_context);
            }
        }

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
                _context = null;
            }
        }
    }
}

