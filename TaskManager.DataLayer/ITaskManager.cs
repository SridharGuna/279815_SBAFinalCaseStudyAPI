using System.Collections.Generic;
using TaskManager.EntityLayer;


namespace TaskManager.DataLayer
{
    public interface  ITaskManager
    {
        IEnumerable<Task> GetTask();
        Task GetTaskById(long Id);
        bool AddTask(Task task);
        bool UpdateTask(long id, Task task);
        Task DeleteTask(long id);
    }
}
