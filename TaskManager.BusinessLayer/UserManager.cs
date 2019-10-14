using System.Collections.Generic;
using TaskManager.EntityLayer;

namespace TaskManager.BusinessLayer
{
    public class UserManager : IUserManager
    {
        private DataLayer.IUserManager _userManager;
        public UserManager()
        {
            _userManager = new DataLayer.UserManager();
        }
        public IEnumerable<User> GetUsers()
        {
            return _userManager.GetUsers();
        }

        public bool AddUser(User task)
        {
            return _userManager.AddUserask(task);
        }

        public bool UpdateUser(long id, User task)
        {
            return _userManager.UpdateUser(id, task);
        }

        public bool DeleteUser(long id)
        {
            return _userManager.DeleteUser(id);
        }
    }
}
