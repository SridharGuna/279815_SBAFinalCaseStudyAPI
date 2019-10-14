using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using TaskManager.BusinessLayer;
using TaskManager.EntityLayer;

namespace TaskManagerApiService.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UserController : ApiController
    {
        private IUserManager _userManager;

        public UserController(IUserManager taskManager)
        {
            _userManager = taskManager;
        }

        public UserController()
        {
            _userManager = new TaskManager.BusinessLayer.UserManager();
        }

        // GET: api/User
        public IEnumerable<User> Get()
        {
            return _userManager.GetUsers();
        }

        // POST: api/User
        [ResponseType(typeof(User))]
        [HttpPost]
        public IHttpActionResult PostUser(User user)
        {
            var response = _userManager.AddUser(user);
            return Ok(response ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
        }

        // PUT: api/User/5

        [HttpPut]
        public IHttpActionResult PutTask(long id, User user)
        {
            user.UserId = id;
            var response = _userManager.UpdateUser(id, user);
            return Ok(response ? HttpStatusCode.NoContent : HttpStatusCode.BadRequest);
        }

        // DELETE: api/User/5
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var response = _userManager.DeleteUser(id);
            return Ok(response ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
        }

        // GET: api/User/5
        public string Get(int id)
        {
            return "value";
        }
    }
}
