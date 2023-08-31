using DomainLayer.Data;
using Entities.Models;
using ServiceLayer.IService;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Service
{
    public class LoginRepository : ILoginRepository
    {
        private readonly LoginContext _loginContext = new LoginContext();

        public LoginRepository()
        {

        }
        public LoginRepository(LoginContext loginContext)
        {
            _loginContext = loginContext;
        }

        public LoginCredentials Delete(int id, LoginCredentials login)
        {
            var record = _loginContext.LoginEntities.Find(id);

            _loginContext.LoginEntities.Remove(record);

            _loginContext.SaveChanges();

            return record;
        }

        public LoginCredentials GetById(int id)
        {

            var record =  _loginContext.LoginEntities.Find(id);

            return record;

        }

        public async Task<IEnumerable<LoginCredentials>> GetAll()
        {
            return await Task.Run(() => _loginContext.LoginEntities.ToList());
        }

        public LoginCredentials Insert(LoginCredentials login)
        {

            try {
                _loginContext.LoginEntities.Add(login);
                _loginContext.SaveChanges();

            }
            catch (Exception ex)
            {
                //  mess = "Failed  to create user";  
                throw ex;
            }
         

            return login;

        }

        public async  Task<LoginCredentials> Update(int id, LoginCredentials logins)
        {


            try
            { 
                   var record = _loginContext.LoginEntities.Find(id);
                
                    if (id != record.loginId)
                    {
                        throw new Exception("Record does not exist in the database");

                    }
                    else
                    {
                        var cred = await Task.Run(() => _loginContext.Entry(record).State = EntityState.Modified);
                        _loginContext.SaveChanges();

                    }

            }
             catch (Exception ex)
                {
                    throw ex;
                }

                return logins;   

        }
        #region Addition Methods
        public async Task<LoginCredentials> GetLoginCredentialByUsername(string username)
        { 

           
            var cred = await Task.Run(() => _loginContext.LoginEntities.FirstOrDefault(q => q.UserName == username));
                return cred;
           
        }
        public void UpdateLoggedUser(LoginCredentials loginCredentials)
        {
           try
            {
               _loginContext.Entry(loginCredentials).State = EntityState.Modified;
                 _loginContext.SaveChanges();
                
            }
            catch (Exception ex)
            {
                throw ex;
            
            }
             
        }


        #endregion
    }
}
