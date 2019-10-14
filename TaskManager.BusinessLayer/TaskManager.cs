using System;
using System.Collections.Generic;
using TaskManager.EntityLayer;


namespace TaskManager.BusinessLayer
{
    public class TaskManager : ITaskManager
    {
        private DataLayer.ITaskManager _taskManager;
        public TaskManager()
        {
            _taskManager =  new DataLayer.TaskManager();
        }

        public Task GetTaskById(long Id)
        {
          return  _taskManager.GetTaskById(Id);
        }

        public bool AddTask(Task task)
        {
          return  _taskManager.AddTask(task);
        }

        public bool UpdateTask(long id, Task task)
        {
          return  _taskManager.UpdateTask(id, task);
        }

        public Task DeleteTask(long id)
        {
           return _taskManager.DeleteTask(id);
        }

        public IEnumerable<Task> GetTask()
        {
            return _taskManager.GetTask();
        }
    }
}
