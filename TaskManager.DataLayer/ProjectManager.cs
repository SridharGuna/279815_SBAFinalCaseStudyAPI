using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using TaskManager.EntityLayer;
using TaskManager.EntityLayer.Models;

namespace TaskManager.DataLayer
{
    public class ProjectManager : IProjectManager
    {
        readonly TaskManagerEntities _db = new TaskManagerEntities();

        public IEnumerable<ProjectViewModel> GetProjects()
        {
            var pjt = _db.Projects.ToList();
            var task = _db.Tasks.ToList();
            var user = _db.Users.ToList();
            var query =
                from p in pjt
                join u in user on p.ManagerId equals u.UserId into f
                from c in f.DefaultIfEmpty()
                join t in task on p.ProjectId equals t.ProjectId into g
                
                select new ProjectViewModel
                {
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    ProjectId = p.ProjectId,
                    ManagerId = p.ManagerId,
                    Priority = p.Priority,
                    ProjectName = p.ProjectName,
                    IsCompleted = p.IsCompleted,
                    UserId = c?.UserId,
                    UserName = c?.FirstName,
                    TaskCount = g.Count(post => post.ProjectId == p.ProjectId)
                };
            var response = query.ToList();
           
            //var categoryCounts =
            //    from p in products
            //    group p by p.Category into g
            //    select new { Category = g.Key, ProductCount = g.Count() };
            return response.ToList();
        }

        public bool AddProject(ProjectViewModel tasksViewModel)
        {
           
            var task = new Project
            {
                ProjectId = 0,
                StartDate = tasksViewModel.StartDate,
                EndDate =  tasksViewModel.EndDate,
                ManagerId = tasksViewModel.ManagerId,
                ProjectName = tasksViewModel.ProjectName,
                IsCompleted = tasksViewModel.IsCompleted,
                Priority = tasksViewModel.Priority,
                TaskCount = 0
            };
            task.IsCompleted = false;
            _db.Projects.Add(task);

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

        public bool UpdateProject(long id, ProjectViewModel tasksViewModel)
        {
            var task = new Project
            {
                ProjectId = tasksViewModel.ProjectId,
                StartDate = tasksViewModel.StartDate,
                EndDate = tasksViewModel.EndDate,
                ManagerId = tasksViewModel.UserId,
                ProjectName = tasksViewModel.ProjectName,
                IsCompleted = tasksViewModel.IsCompleted,
                Priority = tasksViewModel.Priority,
                TaskCount = 0
            };
            _db.Entry(task).State = EntityState.Modified;
            task.IsCompleted = false;
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

        public bool DeleteProject(long id)
        {
            Project task = _db.Projects.Find(id);
            task.IsCompleted = true;
            _db.Entry(task).State = EntityState.Modified;
            if (task == null)
            {
                return false;
            }

            
            _db.SaveChanges();
            return true;
        }
    }
}
