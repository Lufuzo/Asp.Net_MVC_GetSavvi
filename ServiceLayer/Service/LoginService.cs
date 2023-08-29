﻿using Entities.Models;
using ServiceLayer.IService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Service
{
    public class LoginService : ILoginService 
    {

        private readonly LoginRepository loginRepository  = new LoginRepository();


        public LoginService()
        { 
        
        }
        public LoginService(LoginRepository loginRepository)
        {
            this.loginRepository = loginRepository;
        }


        public Task<string> Delete(int id)
        {
            return loginRepository.Delete(id);
        }

        public Task<LoginCredentials> GetById(int id)
        {
            return loginRepository.GetById(id);
        }

        public Task<IEnumerable<LoginCredentials>> GetAll()
        {
            return loginRepository.GetAll();
        }

        public Task<LoginCredentials> Insert(LoginCredentials login)
        {
            return loginRepository.Insert(login);
        }

        public Task<LoginCredentials> Update(int id, LoginCredentials login)
        {
            return  loginRepository.Update(id, login);
        }
        public  Task<LoginCredentials> GetLoginCredentialByUsername(string username)
        {
            return loginRepository.GetLoginCredentialByUsername(username);
        }
        public void UpdateLoggedUser(LoginCredentials loginCredentials)
        {

            loginRepository.UpdateLoggedUser(loginCredentials);
        }
    }
}
