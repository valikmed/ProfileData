using System;
using Domain.Entities;
using System.Collections.Generic;
using System.Text;

namespace Domain.Abstractions.Repositories
{
	public interface IAvatarRepository:IRepository<Avatar>
	{
        Avatar Get(Guid id);
        void Remove(Guid id);
    }
}

