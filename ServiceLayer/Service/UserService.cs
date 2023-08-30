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


        public Users Delete(int id, Users users)
        {
           return usersRepository.Delete(id, users);
        }

        public Users Get(int id)
        {
            return usersRepository.Get(id);
        }

        public Task<IEnumerable<Users>> GetAll()
        {
           return usersRepository.GetAll();
        }

        public Users Insert(Users users)
        {
            return usersRepository.Insert(users);
        }

        public Users Update(int id, Users users)
        {
            return usersRepository.Update(id,users);
        }

       public bool IsIdNumberDuplicated(string idNumber)
        {
            return usersRepository.IsIdNumberDuplicated(idNumber);
        }
    }
}
