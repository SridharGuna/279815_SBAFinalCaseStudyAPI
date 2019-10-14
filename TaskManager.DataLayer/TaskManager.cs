using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using TaskManager.EntityLayer;
namespace TaskManager.DataLayer
{
   public class TaskManager : ITaskManager
    {
        readonly TaskManagerEntities _db = new TaskManagerEntities();
        public IEnumerable<Task> GetTask()
        {
            return _db.Tasks.ToList();
        }

        public Task GetTaskById(long Id)
        {
           return _db.Tasks.Find(Id);
        }

        public bool AddTask(Task task)
        {

            var user = _db.Users.Find(task.UserId);
            _db.Entry(user).State = EntityState.Modified;
            
                user.TaskId = task.TaskId;
                user.ProjectId = task.ProjectId??0;
           
            task.status = true;
            _db.Tasks.Add(task);


            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                return  false;
            }
            return true;
        }

        public bool UpdateTask(long id, Task task)
        {

            var user = _db.Users.Find(task.UserId);
            _db.Entry(user).State = EntityState.Modified;
            if (user != null)
            {
                user.TaskId = task.TaskId;
                user.ProjectId = task.ProjectId;
            }

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

        public Task DeleteTask(long id)
        {
            Task task = _db.Tasks.Find(id);
            _db.Entry(task).State = EntityState.Modified;
            task.status = false;
            if (task == null)
            {
                return new Task();
            }
           
            _db.SaveChanges();
            return task;
        }

        private bool TaskExists(long id)
        {
            return _db.Tasks.Count(e => e.TaskId == id) > 0;
        }
    }
}
