using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager.EntityLayer;
using TaskManager.EntityLayer.Models;
using Task = System.Threading.Tasks.Task;

namespace TaskManager.BusinessLayer
{
    public interface IProjectManager
    {
        IEnumerable<ProjectViewModel> GetProject();
        bool AddProject(ProjectViewModel task);
        bool UpdateProject(long id, ProjectViewModel task);
        bool DeleteProject(long id);
    }
}
