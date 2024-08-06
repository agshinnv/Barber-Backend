using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<List<AppUser>> GetAll();
        Task<IList<string>> GetRoles(AppUser user);
    }
}
