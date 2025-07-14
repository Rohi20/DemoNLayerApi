using DemoNLayerApi.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoNLayerApi.Business.IServices
{
    public interface IUserService
    {
        Task CreateUser(User user);
        Task UpdateUser(User user);
        Task<List<User>> GetUsers();
        Task<User> GetUserByEmail(string email);
        Task<User> AuthenticateUser(string email, string password);
    }
}
