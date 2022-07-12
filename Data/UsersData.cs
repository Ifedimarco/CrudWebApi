using CrudWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudWebApi.Data
{
    public class UsersData : IUsersData
    {
        private ApplicationDbContext _applicationDbContext;
        public UsersData(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public UsersModel CreateUser(UsersModel usersModel)
        {
            usersModel.Id = Guid.NewGuid();
            _applicationDbContext.UsersModels.Add(usersModel);
            _applicationDbContext.SaveChanges();
            return usersModel;
        }

        public void DeleteUser(UsersModel usersModel)
        {
            _applicationDbContext.UsersModels.Remove(usersModel);
            _applicationDbContext.SaveChanges();
        }

        public UsersModel EditUser(UsersModel usersModel)
        {
            var existingUser = _applicationDbContext.UsersModels.Find(usersModel.Id);
            if (existingUser != null)
            {
                existingUser.Name = usersModel.Name;
                existingUser.Email = usersModel.Email;
                existingUser.Role = usersModel.Role;
                _applicationDbContext.UsersModels.Update(existingUser);
                _applicationDbContext.SaveChanges();
            }
            return usersModel;
        }

        public List<UsersModel> GetAllUsers()
        {
            return _applicationDbContext.UsersModels.ToList();
        }

        public UsersModel GetUserById(Guid id)
        {
            var usersModel = _applicationDbContext.UsersModels.Find(id);
            return usersModel;
        }
    }
}
