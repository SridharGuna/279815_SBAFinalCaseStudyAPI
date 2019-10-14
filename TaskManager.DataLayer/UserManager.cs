using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using TaskManager.EntityLayer;

namespace TaskManager.DataLayer
{
    public class UserManager : IUserManager
    {
        readonly TaskManagerEntities _db = new TaskManagerEntities();

        public IEnumerable<User> GetUsers()
        {
            return _db.Users.ToList();
        }

        public bool AddUserask(User task)
        {
            _db.Users.Add(task);

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                return false;
            }

            return true;
        }

        public bool UpdateUser(long id, User task)
        {
            _db.Entry(task).State = EntityState.Modified;

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }

            return true;
        }

        public bool DeleteUser(long id)
        {
            EntityLayer.User task = _db.Users.Find(id);
            if (task == null)
            {
                return false;
            }

            _db.Users.Remove(task);
            _db.SaveChanges();
            return true;
        }
    }
}
