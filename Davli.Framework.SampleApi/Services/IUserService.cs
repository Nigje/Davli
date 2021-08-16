using Davli.Framework.SampleApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Davli.Framework.SampleApi.Services
{
    public interface IUserService
    {
        /// <summary>
        /// Add new user to database.
        /// </summary>
        /// <param name="registerUserModel"></param>
        /// <returns></returns>
        Task<UserModel> AddNewUserAsync(RegisterUserModel registerUserModel);

        /// <summary>
        /// Update the user.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="registerUserModel"></param>
        /// <returns></returns>
        Task UpdateUserAsync(long userId, RegisterUserModel registerUserModel);

        /// <summary>
        /// Get all users.
        /// </summary>
        /// <returns></returns>
        Task<List<UserModel>> GetAllUserAsync();

        /// <summary>
        /// Delete the user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task DeleteUserAsync(long userId);

        /// <summary>
        /// Get the user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<UserModel> GetUserAsync(long userId);
    }
}
