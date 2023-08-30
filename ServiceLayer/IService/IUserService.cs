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
        Users Get(int id);
        Users Insert(Users users);
        Users Update(int id, Users users);
        Users Delete(int id, Users users);

        bool IsIdNumberDuplicated(string idNumber);
    }
}
