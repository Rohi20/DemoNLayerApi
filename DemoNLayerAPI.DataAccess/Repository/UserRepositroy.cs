using DemoNLayerApi.Data.IRepository;
using DemoNLayerApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoNLayerApi.Data.Repository
{
    public class UserRepositroy : IUserRepository
    {
        private readonly AppDBContext _dBContext;

        public UserRepositroy(AppDBContext dbContext)
        {
            _dBContext = dbContext;            
        }

        public async Task CreateUser(User user)
        {
            _dBContext.Users.Add(user);
            await _dBContext.SaveChangesAsync();
        }

        public async Task UpdateUser(User user)
        {
            _dBContext.Users.Update(user);
            await _dBContext.SaveChangesAsync();
        }

        public async Task<List<User>> GetAllUsers()
        {
            var users = await _dBContext.Users.ToListAsync();
            return users;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _dBContext.Users
                .Where(u => u.Profile.Email == email)
                .Include(c => c.Profile)
                .FirstOrDefaultAsync();

            if(user == null)
            {
                throw new Exception("User not found");
            }

            return user;
        }
    }
}
