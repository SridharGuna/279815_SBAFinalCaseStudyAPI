using System.Collections.Generic;
using TaskManager.EntityLayer;
using Task = System.Threading.Tasks.Task;

namespace TaskManager.DataLayer
{
   public interface IUserManager
    {
        IEnumerable<User> GetUsers();
        bool AddUserask(User task);
        bool UpdateUser(long id, User task);
        bool DeleteUser(long id);
    }
}
