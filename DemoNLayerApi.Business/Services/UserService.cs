using DemoNLayerApi.Data.Exceptions;
using DemoNLayerApi.Business.IServices;
using DemoNLayerApi.Data.IRepository;
using DemoNLayerApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoNLayerApi.Business.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task CreateUser(User user)
        {
           await _repository.CreateUser(user);
        }

        public async Task UpdateUser(User user)
        {
            await _repository.UpdateUser(user);
        }

        public async Task<List<User>> GetUsers()
        {
            return await _repository.GetAllUsers();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _repository.GetUserByEmail(email);
        }

        public async Task<User> AuthenticateUser(string email, string password)
        {
            User user = await _repository.GetUserByEmail(email);

            if (user == null)
            {
                throw new NotFoundException("User not found with this email");
            }

            if (user.Profile.Password != password)
            {
                throw new CustomException("Password is incorrect");

            }

            return user;
        }

    }
}
