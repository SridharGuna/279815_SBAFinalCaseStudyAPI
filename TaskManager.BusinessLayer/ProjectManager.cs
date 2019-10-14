using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.EntityLayer;
using TaskManager.EntityLayer.Models;
using Task = System.Threading.Tasks.Task;

namespace TaskManager.BusinessLayer
{
    public class ProjectManager : IProjectManager
    {
        private DataLayer.IProjectManager _taskManager;
        public ProjectManager()
        {
            _taskManager = new DataLayer.ProjectManager();
        }
        public IEnumerable<ProjectViewModel> GetProject()
        {
            return _taskManager.GetProjects();
        }

        public bool AddProject(ProjectViewModel task)
        {
            return _taskManager.AddProject(task);
        }

        public bool UpdateProject(long id, ProjectViewModel task)
        {
            return _taskManager.UpdateProject(id, task);
        }

        public bool DeleteProject(long id)
        {
            return _taskManager.DeleteProject(id);
        }
    }
}
