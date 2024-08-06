﻿using Domain.Models;
using Repository.Repositories.Interfaces;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;  
        }

        public async Task<List<AppUser>> GetAll()
        {
            return await _userRepository.GetAll();
        }

        public async Task<IList<string>> GetRoles(AppUser user)
        {
            return await _userRepository.GetRoles(user);
        }
    }
}
