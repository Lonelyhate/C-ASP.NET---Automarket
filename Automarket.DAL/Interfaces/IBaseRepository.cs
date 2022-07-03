using System.Collections.Generic;
using System.Threading.Tasks;
using Automarket.Domain.Models;

namespace Automarket.DAL.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task<bool> Create(T model);

        Task<T> Get(int id);

        Task<List<T>> Select();

        Task<bool> Delete(T model);

        Task<T> Update(T model);
    }
}