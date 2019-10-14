using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using System.Web.Http.Results;
using TaskManager.BusinessLayer;
using TaskManager.EntityLayer;
using TaskManager.EntityLayer.Models;


namespace TaskManagerApiService.Controllers
{
    [EnableCors(origins:"*",headers:"*",methods:"*")]
    public class TasksController : ApiController
    {
        private ITaskManager _taskManager;

        public TasksController(ITaskManager taskManager) {
            _taskManager = taskManager;
        }

        public TasksController() {
            _taskManager = new TaskManager.BusinessLayer.TaskManager();
        }
        

        // GET: api/Tasks
        [HttpGet]
        public IEnumerable<TaskViewModel> GetTasks()
        {
            TaskManagerEntities db = new TaskManagerEntities();
            var response = _taskManager.GetTask();
            var tasks = response.ToList();
            var user = db.Users.ToList();
            var project = db.Projects.ToList();
            var parent = db.Parent_Task.ToList();

            var responseTaskViewModel =
                from t in tasks
                join p in project on t.ProjectId equals p.ProjectId into pj
                from a in pj.DefaultIfEmpty()
                join u in user on t.UserId equals u.UserId into us
                from b in us.DefaultIfEmpty()
                join x in parent on t.ParentId equals x.ParentId into pa
                from c in pa.DefaultIfEmpty()
                select new TaskViewModel
                {
                    TaskId = t.TaskId.ToString(),
                    Task = t.TaskName,
                    Priority = t.Priority,
                    StartDate = t.StartDate?.ToString("yyyy-MM-dd"),
                    EndDate = t.EndDate?.ToString("yyyy-MM-dd"),
                    Status = t.status,
                    ParentId = Convert.ToInt32(t.ParentId),
                    ParentTask = c?.ParentTask,
                    ProjectId = t.ProjectId,
                    ProjectName = a?.ProjectName,
                    UserId = t.UserId,
                    UserName = b?.FirstName + " " + b?.LastName

                };
            return responseTaskViewModel.ToList();
        }

        // GET: api/Tasks/5
        [ResponseType(typeof(Task))]
        public IHttpActionResult GetTask(long id){
            Task task = _taskManager.GetTaskById(id);
            if (task == null) {
                return NotFound();
            }
           
            return Ok(task);
        }

        // PUT: api/Tasks/0/5
        [ResponseType(typeof(void))]
        [Route("api/Tasks/{isEndFlag}/{id}")]
        [HttpPut]
        public IHttpActionResult PutTask(int isEndFlag,long id, TaskViewModel task){
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            var tasks = new Task
            {
                Priority = task.Priority,
                TaskName = task.Task.Trim(),
                StartDate = DateConversion(task.StartDate),
                EndDate = DateConversion(task.EndDate) ,//(isEndFlag==1) ? DateTime.Now : (task.EndDate != null ? Convert.ToDateTime(task.EndDate)),
                TaskId = Convert.ToInt32(task.TaskId),
                ParentId = task.ParentId??0,//string.IsNullOrEmpty(task.ParentTask) ? default(long) : Convert.ToInt32(task.ParentTask)
                ProjectId = task.ProjectId,
                UserId = task.UserId,
                status = isEndFlag != 1

            };
            if (id != tasks.TaskId){
                return BadRequest();
            }
            _taskManager.UpdateTask(id, tasks);

            return Ok(HttpStatusCode.NoContent);
        }

        private DateTime? FormatDates(string endDate,int isEndFlag)
        {
            if (isEndFlag == 1) return DateTime.Now;
            if (!string.IsNullOrWhiteSpace(endDate)&& endDate !="null")
            {
                return Convert.ToDateTime(endDate);
            }

            return null;

        }

        // POST: api/Tasks
        [ResponseType(typeof(TaskViewModel))]
        [HttpPost]
        public IHttpActionResult PostTask(TaskViewModel task){
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var tasks = new Task{
                Priority = task.Priority,
                TaskName = task.Task,
                StartDate = DateConversion(task.StartDate) ,// task.StartDate != null ? Convert.ToDateTime(task.StartDate) : (DateTime?)null,
                EndDate = DateConversion(task.EndDate),//task.EndDate != null ? Convert.ToDateTime(task.EndDate) : (DateTime?) null,
                ParentId = task.ParentId??0,//string.IsNullOrEmpty(task.ParentTask) ? default(long) : Convert.ToInt32(task.ParentTask)
                ProjectId = task.ProjectId,
                UserId = task.UserId,
                status = true
            };
            _taskManager.AddTask(tasks);
            return Ok(HttpStatusCode.OK);
        }

        private DateTime? DateConversion(string date)
        {
            if (date != String.Empty && date != null)
            {
                return Convert.ToDateTime(date);
            }
            else
            {
                return null;
            }
        }

        // DELETE: api/Tasks/5
        [ResponseType(typeof(Task))]
        public IHttpActionResult DeleteTask(long id){
            Task task = _taskManager.DeleteTask(id);
            if (task.TaskId == 0)
                return NotFound();
            return Ok(task);
        }

    }
}