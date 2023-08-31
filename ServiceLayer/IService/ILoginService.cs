using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.IService
{
    public interface ILoginService
    {
       

        Task<IEnumerable<LoginCredentials>> GetAll();
         LoginCredentials GetById(int id);
        LoginCredentials Insert(LoginCredentials login);
        Task<LoginCredentials> Update(int id, LoginCredentials login);
        LoginCredentials Delete(int id, LoginCredentials login);
        Task<LoginCredentials> GetLoginCredentialByUsername(string username);
        void UpdateLoggedUser(LoginCredentials loginCredentials);
    }
}
