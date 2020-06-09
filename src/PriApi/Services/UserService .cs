﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PriApi.Entities;
using PriApi.Helpers;

namespace PriApi.Services
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        IEnumerable<User> GetAll();
    }

    public class UserService : IUserService
    {
        private readonly SuperAdminDefaultOptions _superAdminDefaultOptions;

        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private List<User> _users;

        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings, 
            IOptions<SuperAdminDefaultOptions> superAdminDefaultOptions)
        {
            _appSettings = appSettings.Value;
            _superAdminDefaultOptions = superAdminDefaultOptions.Value;

            _users = new List<User>
            {
                new User { Id = 1, FirstName = _superAdminDefaultOptions.FirstName, 
                    LastName = _superAdminDefaultOptions.LastName,
                    Username =  _superAdminDefaultOptions.Username,
                    Password =  _superAdminDefaultOptions.Password
                }
            };
        }

        public User Authenticate(string username, string password)
        {
            var user = _users.SingleOrDefault(x => x.Username == username && x.Password == password);

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(20),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return user;
        }

        public IEnumerable<User> GetAll()
        {
            return _users;
        }
    }
}