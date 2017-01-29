using System.Collections.Generic;
using BLL.Interface.Entities;

namespace BLL.Interface.Services
{
    public interface IService<T> where T : class
    {
        T GetEntity(int id);
        IEnumerable<T> GetAllEntities();
        void Create(T e);
        void Delete(T e);

        void Update(T e);

    }
}


