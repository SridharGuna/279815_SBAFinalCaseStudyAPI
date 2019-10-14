using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using TaskManager.EntityLayer;

namespace TaskManagerApiService.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ParentTaskController : ApiController
    {
        TaskManagerEntities _db = new TaskManagerEntities();
        // GET: api/ParentTask
        public IEnumerable<Parent_Task> Get()
        {

            return _db.Parent_Task.ToList();
        }

        // GET: api/ParentTask/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ParentTask
        public IHttpActionResult Post(Parent_Task parenttask)
        {
            var flag = true;
            try
            {

               _db.Parent_Task.Add(parenttask);
                _db.SaveChanges();
                flag = true;
            }
            catch (Exception e)
            {
                flag = false;
                Console.WriteLine(e);
                throw;
            }
            return Ok(flag ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
        }

        // PUT: api/ParentTask/5
        public IHttpActionResult Put(int id, Parent_Task parenttask)
        {
            return Ok(HttpStatusCode.NoContent);
        }

        // DELETE: api/ParentTask/5
        public IHttpActionResult Delete(int id)
        {
            return Ok(HttpStatusCode.OK);
        }
    }
}
