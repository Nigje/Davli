using Davli.Framework.AspNet.Mvc;
using Davli.Framework.SampleApi.Controllers.Dtos;
using Davli.Framework.SampleApi.Models;
using Davli.Framework.SampleApi.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Davli.Framework.SampleApi.Controllers
{
    /// <summary>
    /// Simple crud.
    /// </summary>
    [Route("api/user")]
    [ApiController]
    public class UserController : DavliControllerBase
    {
        //*********************************************************************************************************
        //Variables:
        private readonly IUserService _userService;
        //*********************************************************************************************************
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        //*********************************************************************************************************
        /// <summary>
        /// Add new user to database.
        /// </summary>
        /// <param name="registerUserDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<UserDto> Add([FromBody] RegisterUserDto registerUserDto)
        {
            RegisterUserModel registerUserModel = new RegisterUserModel { Name = registerUserDto.Name };
            UserModel userModel = await _userService.AddNewUserAsync(registerUserModel);
            UserDto userDto = new UserDto { Id = userModel.Id, Name = userModel.Name };
            return userDto;
        }

        //*********************************************************************************************************
        /// <summary>
        /// Update exist user.
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="registerUserDto"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{UserId}")]
        public async Task Update([FromRoute] long UserId, [FromBody] RegisterUserDto registerUserDto)
        {
            RegisterUserModel registerUserModel = new RegisterUserModel { Name = registerUserDto.Name };
            await _userService.UpdateUserAsync(UserId, registerUserModel);
        }

        //*********************************************************************************************************
        /// <summary>
        /// Delete the user.
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{UserId}")]
        public async Task Delete([FromRoute] long UserId)
        {
            await _userService.DeleteUserAsync(UserId);
        }

        //*********************************************************************************************************
        /// <summary>
        /// Get all users.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<UserDto>> GetAll()
        {
            List<UserModel> userModels = await _userService.GetAllUserAsync();
            List<UserDto> userDtos = new List<UserDto>();
            foreach (UserModel user in userModels)
            {
                userDtos.Add(new UserDto { Id = user.Id, Name = user.Name });
            }
            return userDtos;
        }

        //*********************************************************************************************************
        /// <summary>
        /// Get specific user.
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{UserId}")]
        public async Task<UserDto> Get([FromRoute] long UserId)
        {
            UserModel userModel = await _userService.GetUserAsync(UserId);
            return new UserDto { Id = userModel.Id, Name = userModel.Name };
        }
        //*********************************************************************************************************
    }
}
