using DomainLayer.Data;
using Entities.Models;
using ServiceLayer.IService;
using System;
using System.Collections.Generic;
using System.Data;
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

        public Users Delete(int id, Users users)
        {
            var record = _usersContext.Users.Find(id);

              _usersContext.Users.Remove(record);

             _usersContext.SaveChanges();

            return record;


        }

        public  Users Get(int id)
        {


            // var record = await Task.Run(() => _usersContext.Users.Find(id));
            var record =  _usersContext.Users.Find(id);


            return record;

        }

        public async Task<IEnumerable<Users>> GetAll()
        {
            return await Task.Run(() => _usersContext.Users.ToList());
        }

        public Users Insert(Users users)
        {
            try
            {

                 _usersContext.Users.Add(users);
                 _usersContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return users;
        }

        public Users Update(int id, Users users)
        {

            var record = _usersContext.Users.Find(id);
            try
            {
                if (id != record.usersId)
                {
                    throw new Exception("Record does not exist in the database");
                
                }
                else
                { 

                    record.Name = users.Name;
                    record.Email = users.Email; 
                    record.Surname = users.Surname;
                    record.IdNumber = users.IdNumber;
                    record.Mobile = users.Mobile;

                    _usersContext.Entry(record).State = EntityState.Modified;
                    _usersContext.SaveChanges();


                }
           

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return users;

        }


        public bool IsIdNumberDuplicated(string idNumber)
        {
            return _usersContext.Users.Any(a => a.IdNumber == idNumber);
        }

    }
}
