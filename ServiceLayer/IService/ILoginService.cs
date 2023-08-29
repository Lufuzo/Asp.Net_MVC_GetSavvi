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
         Task<LoginCredentials> GetById(int id);
        Task<LoginCredentials> Insert(LoginCredentials login);
        Task<LoginCredentials> Update(int id, LoginCredentials login);
        Task<string> Delete(int id);
        Task<LoginCredentials> GetLoginCredentialByUsername(string username);
        void UpdateLoggedUser(LoginCredentials loginCredentials);
    }
}
