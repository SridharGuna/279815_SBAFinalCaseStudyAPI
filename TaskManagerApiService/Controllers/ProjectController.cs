using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using TaskManager.BusinessLayer;
using TaskManager.EntityLayer;
using TaskManager.EntityLayer.Models;

namespace TaskManagerApiService.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ProjectController : ApiController
    {
        private IProjectManager _projectManager;
        TaskManagerEntities _db = new TaskManagerEntities();

        public ProjectController(IProjectManager taskManager)
        {
            _projectManager = taskManager;
        }

        public ProjectController()
        {
            _projectManager = new TaskManager.BusinessLayer.ProjectManager();
        }

        // GET: api/Project
        public IEnumerable<ProjectViewModel> Get()
        {
            var response = _projectManager.GetProject();
            
            //var user = _db.Users.ToList();
            //var project = _db.Projects.ToList();
            

            //var responseTaskViewModel =
            //    from p in project 
            //    join u in user on p.ManagerId  equals u.UserId into us
            //    from b in us.DefaultIfEmpty()
            //    select new ProjectViewModel
            //    {
            //        ProjectId = p.ProjectId,
            //        Priority = p.Priority,
            //        StartDate = p.StartDate,
            //        EndDate = p.EndDate,
            //        ProjectName = p.ProjectName,
            //        UserId = b?.UserId,
            //        UserName = b?.FirstName + " " + b?.LastName,
            //        ManagerId = p.ManagerId,
            //        IsCompleted = p.IsCompleted,
                    
            //    };
            return response;
        }

        // POST: api/Project
        [ResponseType(typeof(User))]
        [HttpPost]
        public IHttpActionResult Post(ProjectViewModel project)
        {
            project.IsCompleted = false;
            var response = _projectManager.AddProject(project);
            return Ok(response ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
        }

        // PUT: api/Project/5
        [HttpPut]
        public IHttpActionResult Put(long id, ProjectViewModel project)
        {
            var response = _projectManager.UpdateProject(id, project);
            return Ok(response ? HttpStatusCode.NoContent : HttpStatusCode.BadRequest);
        }

        // DELETE: api/Project/5
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var response = _projectManager.DeleteProject(id);
            return Ok(response ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
        }

        // GET: api/Project/5
        public string Get(int id)
        {
            return "value";
        }
    }
}
