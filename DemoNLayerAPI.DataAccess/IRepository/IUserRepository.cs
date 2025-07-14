using DemoNLayerApi.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoNLayerApi.Data.IRepository
{
    public interface IUserRepository 
    {
        Task CreateUser(User user);
        Task UpdateUser(User user);
        Task<List<User>> GetAllUsers();
        Task<User> GetUserByEmail(string email);
    }
}
