using Davli.Framework.DI;
using Davli.Framework.Exceptions;
using Davli.Framework.Orm;
using Davli.Framework.SampleApi.Models;
using Davli.Framework.SampleApi.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Davli.Framework.SampleApi.Services.Impl
{
    public class UserService : IUserService, ITransientLifetime
    {
        //*********************************************************************************************************
        //Variables:
        private readonly IUnitOfWork _unitOfWork;
        //*********************************************************************************************************
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        //*********************************************************************************************************
        public async Task<UserModel> AddNewUserAsync(RegisterUserModel registerUserModel)
        {
            User user = new User { Name = registerUserModel.Name };
            await _unitOfWork.GenericRepository<User>().AddAsync(user);
            await _unitOfWork.SaveAsync();
            return new UserModel { Id = user.Id, Name = user.Name };
        }
        //*********************************************************************************************************
        public async Task DeleteUserAsync(long userId)
        {
            User user = await _unitOfWork.GenericRepository<User>().FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
                throw new DavliExceptionNotFound("User not found.");
            await _unitOfWork.GenericRepository<User>().RemoveAsync(x => x.Id == userId);
        }
        //*********************************************************************************************************
        public async Task<List<UserModel>> GetAllUserAsync()
        {
            //Todo: Implement get all in generic repository. 
            List<User> users = await _unitOfWork.GenericRepository<User>().Where(x => x.Id > -1).ToListAsync();

            List<UserModel> userModels = new List<UserModel>();
            foreach (User user in users)
            {
                userModels.Add(new UserModel { Id = user.Id, Name = user.Name });
            }
            return userModels;
        }
        //*********************************************************************************************************
        public async Task<UserModel> GetUserAsync(long userId)
        {
            User user = await _unitOfWork.GenericRepository<User>().FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
                throw new DavliExceptionNotFound("User not found.");
            return new UserModel { Id = user.Id, Name = user.Name };
        }
        //*********************************************************************************************************
        public async Task UpdateUserAsync(long userId, RegisterUserModel registerUserModel)
        {
            User user = await _unitOfWork.GenericRepository<User>().FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
                throw new DavliExceptionNotFound("User not found.");
            user.Name = registerUserModel.Name;
            await _unitOfWork.SaveAsync();
        }
        //*********************************************************************************************************
    }
}
