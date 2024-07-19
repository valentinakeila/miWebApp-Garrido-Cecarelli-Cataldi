using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UserDto? GetUserById(int id)
        {
            var user = _userRepository.GetById(id);

            if (user == null)
                throw new NotFoundException(nameof(User), id);

            return UserDto.Create(user);
        }

        public List<UserDto?> GetAllUsers()
        {
            var usersList = _userRepository.List();

            return UserDto.CreateList(usersList);
        }

        public UserDto CreateNewUser(UserCreateRequest userCreateRequest)
        {
            var user = _userRepository.GetUserByEmail(userCreateRequest.Email);

            if (user != null)
                throw new Exception();

            var newUser = new User();
            newUser.FirstName = userCreateRequest.FirstName;
            newUser.LastName = userCreateRequest.LastName;
            newUser.Email = userCreateRequest.Email;
            newUser.Password = userCreateRequest.Password;
            newUser.Role = userCreateRequest.Role;

            return UserDto.Create(_userRepository.Add(newUser));
        }

        public void ModifyUserData(int id, UserUpdateRequest userUpdateRequest)
        {
            var user = _userRepository.GetById(id);

            if (user == null)
                throw new NotFoundException(nameof(User), id);

            var auxUser = _userRepository.GetUserByEmail(userUpdateRequest.Email);

            if (auxUser != null)
                throw new Exception("El email que intenta utilizar ya existe en la base de datos");

            if (userUpdateRequest.FirstName != string.Empty) user.FirstName = userUpdateRequest.FirstName;

            if (userUpdateRequest.LastName != string.Empty) user.LastName = userUpdateRequest.LastName;

            if (userUpdateRequest.Email != string.Empty) user.Email = userUpdateRequest.Email;

            if (userUpdateRequest.Password != string.Empty) user.Password = userUpdateRequest.Password;

            _userRepository.Update(user);
        }

        public void DeleteUser(int id)
        {
            var user = _userRepository.GetById(id);

            if (user == null)
                throw new NotFoundException(nameof(User), id);

            _userRepository.Delete(user);
        }

        public List<UserDto> GetUsersByRole(UserRole role)
        {
            var usersList = _userRepository.GetUsersByRole(role);

            if (usersList == null)
                throw new NotFoundException(nameof(User), role);

            return UserDto.CreateList(usersList);
        }

        public UserDto? GetUserByEmail(string email)
        {
            var user = _userRepository.GetUserByEmail(email);

            if (user == null)
                throw new NotFoundException(nameof(User), email);

            return UserDto.Create(user);

        }
    }
}