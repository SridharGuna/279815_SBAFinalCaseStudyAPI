using System.Collections.Generic;
using TaskManager.EntityLayer;
using TaskManager.EntityLayer.Models;

namespace TaskManager.DataLayer
{
    public interface IProjectManager
    {
        IEnumerable<ProjectViewModel> GetProjects();
        bool AddProject(ProjectViewModel task);
        bool UpdateProject(long id, ProjectViewModel task);
        bool DeleteProject(long id);
    }
}
