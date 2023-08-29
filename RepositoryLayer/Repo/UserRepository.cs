using DomainLayer.Data;
using Entities.Models;
using ServiceLayer.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Service
{
    public class UserRepository : IUserRepository
    {
        UsersContext _usersContext = new UsersContext();

        public UserRepository()
        {}
        public UserRepository(UsersContext usersContext)
        {
            _usersContext = usersContext;


        }

        public Task<string> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Users> Get(Users users)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Users>> GetAll()
        {
            return await Task.Run(() => _usersContext.Users.ToList());
        }

        public async Task<Users> Insert(Users users)
        {
            try
            {

                await Task.Run(() => _usersContext.Users.Add(users));
                 _usersContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return users;
        }

        public Task<string> Update(int id, Users users)
        {
            throw new NotImplementedException();
        }


        public bool IsIdNumberDuplicated(string idNumber)
        {
            return _usersContext.Users.Any(a => a.IdNumber == idNumber);
        }

    }
}
