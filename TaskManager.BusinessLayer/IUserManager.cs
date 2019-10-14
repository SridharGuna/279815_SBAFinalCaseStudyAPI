using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager.EntityLayer;
using Task = System.Threading.Tasks.Task;

namespace TaskManager.BusinessLayer
{
   public interface IUserManager
    {
        IEnumerable<User> GetUsers();
        bool AddUser(User task);
        bool UpdateUser(long id, User task);
        bool DeleteUser(long id);
    }
}
