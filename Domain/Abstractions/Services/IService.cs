using System;
using System.Collections.Generic;
using System.Text;
using Domain.DTO;
using Microsoft.AspNetCore.Http;

//This interface can be implemented by any class that needs to perform
//CRUD (Create, Read, Update, and Delete) operations on the TEntity entity.

namespace ProfileData.Domain.Abstractions.Services
{
    public interface IService<TEntity> where TEntity : class
    {
        public List<TEntity> GetAll();

        public TEntity Update(TEntity entity);

    }
}