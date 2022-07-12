using CrudWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudWebApi.Data
{
   public interface IUsersData
    {

        List<UsersModel> GetAllUsers();
        UsersModel GetUserById(Guid id);
        UsersModel CreateUser(UsersModel usersModel);
        UsersModel EditUser(UsersModel usersModel);
        void DeleteUser(UsersModel usersModel);
    }
}
