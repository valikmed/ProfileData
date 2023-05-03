using System;
using Domain.Abstractions.Repositories;

namespace Domain.Abstractions
{
    public interface IUnitOfWork : System.IDisposable
    {
        IUserRepository UserRepository { get; }
        IRoleRepository RoleRepository { get; }
        IAvatarRepository AvatarRepository { get; }
        void Save();
    }
}

