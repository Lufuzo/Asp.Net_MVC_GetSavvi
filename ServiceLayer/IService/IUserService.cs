using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.IService
{
    public interface IUserService
    {
        Task<IEnumerable<Users>> GetAll();
        Task<Users> Get(Users users);
        Task<Users> Insert(Users users);
        Task<string> Update(int id, Users users);
        Task<string> Delete(int id);

        bool IsIdNumberDuplicated(string idNumber);
    }
}
