using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public interface IEntity<T>
    {
        public T ID { get; set; }
    }
}