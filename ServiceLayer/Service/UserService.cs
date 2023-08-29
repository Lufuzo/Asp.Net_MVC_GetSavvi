using Entities.Models;
using ServiceLayer.IService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Service
{
    public class UserService : IUserService
    {

        private readonly UserRepository usersRepository = new UserRepository();

        public UserService()
        { 
        
        }

        public UserService(UserRepository usersRepository)
        {
            this.usersRepository = usersRepository;
        }


        public Task<string> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Users> Get(Users users)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Users>> GetAll()
        {
           return usersRepository.GetAll();
        }

        public Task<Users> Insert(Users users)
        {
            return usersRepository.Insert(users);
        }

        public Task<string> Update(int id, Users users)
        {
            throw new NotImplementedException();
        }

       public bool IsIdNumberDuplicated(string idNumber)
        {
            return usersRepository.IsIdNumberDuplicated(idNumber);
        }
    }
}
