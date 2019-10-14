
using System.Collections.Generic;
using TaskManager.EntityLayer;
namespace TaskManager.BusinessLayer
{
    public interface  ITaskManager
    {
        /*Task operations*/
        IEnumerable<Task> GetTask();
        Task GetTaskById(long Id);
        bool AddTask(Task task);
        bool UpdateTask(long id, Task task);
        Task DeleteTask(long id);
    }
}
