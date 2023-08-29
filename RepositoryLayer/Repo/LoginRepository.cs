﻿using DomainLayer.Data;
using Entities.Models;
using ServiceLayer.IService;
using System;
using System.Collections.Generic;
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
        LoginContext _loginContext = new LoginContext();

        public LoginRepository()
        {

        }
        public LoginRepository(LoginContext loginContext)
        {
            _loginContext = loginContext;
        }

        public Task<string> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<LoginCredentials> GetById(int id)
        {

            var record = await Task.Run(() => _loginContext.LoginEntities.Find(id));

            return record;

        }

        public async Task<IEnumerable<LoginCredentials>> GetAll()
        {
            return await Task.Run(() => _loginContext.LoginEntities.ToList());
        }

        public async Task<LoginCredentials> Insert(LoginCredentials login)
        {
            string mess = string.Empty;

            try {
                await Task.Run(() => _loginContext.LoginEntities.Add(login));

            }
            catch (Exception ex)
            {
                //  mess = "Failed  to create user";  
                throw ex;
            }
            _loginContext.SaveChanges();

            return login;

        }

        public async  Task<LoginCredentials> Update(int id, LoginCredentials logins)
        {


            //try
            //{
            var cred =  await Task.Run(() => _loginContext.Entry(logins).State = EntityState.Modified);
             _loginContext.SaveChanges();
            return logins;


            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}

            //string mess = string.Empty;
            //mess = "success";

            //return logins;

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