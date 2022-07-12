using CrudWebApi.Data;
using CrudWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudWebApi.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUsersData _usersData;
        public UsersController(IUsersData usersData)
        {
            _usersData = usersData;
        }
        [HttpGet]
        [Route("api/get-all-users")]
        public IActionResult GetAllUsers()
        {
            return Ok(_usersData.GetAllUsers());
        }

        [HttpGet]
        [Route("api/get-user-by-id/{id}")]
        public IActionResult GetUserById(Guid id)
        {
            var usersModel = _usersData.GetUserById(id);
            if (usersModel != null)
            {
                return Ok(usersModel);
            }
            return NotFound($"user with id:{id} was found");
        }
        [HttpPost]
        [Route("create-user")]
        public IActionResult CreateUser(UsersModel usersModel)
        {
            _usersData.CreateUser(usersModel);
            return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + usersModel.Id, usersModel);
        }
        [HttpPatch]
        [Route("edit-user/{id}")]
        public IActionResult EditUser(Guid id, UsersModel usersModel)
        {
            var existingUser = _usersData.GetUserById(id);
            if (existingUser != null)
            {
                usersModel.Id = existingUser.Id;
                _usersData.EditUser(usersModel);
                return Ok();
            }
            return Ok(usersModel);
        }

        [HttpDelete]
        [Route("delete-user/{id}")]
        public IActionResult DeleteUser(Guid id)
        {
            var user = _usersData.GetUserById(id);
            if (user != null)
            {
                _usersData.DeleteUser(user);
                return Ok();
            }
            return NotFound($"user with id:{id} was found");
        }
    }
}
