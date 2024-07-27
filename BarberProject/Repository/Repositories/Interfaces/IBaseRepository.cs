using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task Create (T entity);
        Task Delete (T entity);
        Task Edit (T entity);
        Task <T> GetById (int id);
        Task <IEnumerable<T>> GetAll ();
    }
}
