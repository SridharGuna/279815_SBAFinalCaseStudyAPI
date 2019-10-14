using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web.Http.Results;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using TaskManager.BusinessLayer;
using TaskManager.EntityLayer;
using TaskManager.EntityLayer.Models;
using TaskManagerApiService.Controllers;
 

namespace TaskManagerApiService.Tests.Controllers
{
    [TestFixture]
    public class TaskControllerTest
    {
        private Mock<ITaskManager> _taskManager;
        [SetUp]
        public void Setup()
        {   _taskManager =   new Mock<ITaskManager>();
        }

        
        [Test]
        public void GetTasks_ReturnsListofTask()
        {   _taskManager.Setup(x => x.GetTask()).Returns(_data);
            
            TasksController controller = new TasksController(_taskManager.Object);
            var actual = controller.GetTasks();

            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Any());
            Assert.IsTrue(actual.Count()==2);
        }

        [Test]
        public void GetTasksById_ReturnsNotFoundResult()
        {
            long id = 100;
            _taskManager.Setup(x => x.GetTask()).Returns(_data);
            TasksController controller = new TasksController(_taskManager.Object);
            var actual = controller.GetTask(id);

            Assert.IsNotNull(actual);
            Assert.That(actual,Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public void GetTasksById_ReturnsTask()
        {
            long id = 1;
            _taskManager.Setup(x => x.GetTaskById(1)).Returns(new Task
            {
                TaskId = 1,
                ParentId = 1,
                Priority = 10,
                TaskName = "test Task-1",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now
            });
            TasksController controller = new TasksController(_taskManager.Object);
            var actual = controller.GetTask(id);

            Assert.IsNotNull(actual);
           Assert.AreEqual(1,
               ((OkNegotiatedContentResult<Task>)actual).Content.TaskId);
        }

        [Test]
        public void PostTask_ReturnsOkResult()
        {
            _taskManager.Setup(x => x.AddTask(new Task
            {
                TaskId = 1,
                ParentId = 1,
                Priority = 10,
                TaskName = "test Task-1",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now
            })).Returns(true);

            TasksController controller = new TasksController(_taskManager.Object);
            var actual = controller.PostTask(new TaskViewModel
            {
              
                ParentTask = "1",
                Priority = 10,
                Task = "test Task-1",
                StartDate = DateTime.Now.ToString(),
                EndDate = DateTime.Now.ToString()
            });

            Assert.IsNotNull(actual);
            var response = actual as OkNegotiatedContentResult<HttpStatusCode>;
            Assert.AreEqual(response.Content, HttpStatusCode.OK);
        }

        [Test]
        public void DeleteTask_ReturnsOkResult()
        {
            _taskManager.Setup(x => x.DeleteTask(1)).Returns(new Task
            {
                TaskId = 1,
                ParentId = 1,
                Priority = 10,
                TaskName = "test Task-1",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now
            });

            TasksController controller = new TasksController(_taskManager.Object);
            var actual = controller.DeleteTask(1);

            Assert.IsNotNull(actual);
            Assert.AreEqual(actual,
                ((OkNegotiatedContentResult<Task>)actual));
        }

        [Test]
        public void PutTask_ReturnsNoContentResult()
        {
            _taskManager.Setup(x => x.UpdateTask(1, new Task
            {
                TaskId = 1,
                ParentId = 1,
                Priority = 10,
                TaskName = "test Task-1",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now
            })).Returns(true);
            var endflag = 0;
            var taskId = 1;

            TasksController controller = new TasksController(_taskManager.Object);
            var actual = controller.PutTask(endflag, taskId, new TaskViewModel
            {
                Task = "test Task-1",
                TaskId = "1",
                StartDate = DateTime.Now.ToString(),
                Priority = 10,
                EndDate = null,
                ParentTask = "0"
            });

            Assert.IsNotNull(actual);
        }

        public static IEnumerable<Task> list
        {
            get
            {
                yield return new Task
                {
                    TaskId = 2,
                    ParentId = 1,
                    Priority = 10,
                    TaskName = "test Task-2",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now
                };
                yield return new Task
                {
                    TaskId = 2,
                    ParentId = 1,
                    Priority = 10,
                    TaskName = "test Task-2",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now
                };
                yield return new Task
                {
                    TaskId = 2,
                    ParentId = 1,
                    Priority = 10,
                    TaskName = "test Task-2",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now
                };
            }
        }

        private readonly IEnumerable<Task> _data = new List<Task>()
        {
            new Task { TaskId = 1, ParentId = 0, Priority = 10, TaskName = "test Task-1", StartDate = DateTime.Now,
                EndDate = DateTime.Now},
            new Task {TaskId = 2, ParentId = 0, Priority = 10, TaskName = "test Task-2", StartDate = DateTime.Now,
                EndDate = DateTime.Now}
        };
    }
     
    public class MyFactoryClass
    {
        public  static   IEnumerable<Task> list = new List<Task>
        {

            new Task { TaskId = 2, ParentId = 1, Priority = 10, TaskName = "test Task-2", StartDate = DateTime.Now, EndDate = DateTime.Now },
            new Task { TaskId = 2, ParentId = 1, Priority = 10, TaskName = "test Task-2", StartDate = DateTime.Now, EndDate = DateTime.Now },
            new Task { TaskId = 2, ParentId = 1, Priority = 10, TaskName = "test Task-2", StartDate = DateTime.Now, EndDate = DateTime.Now }
        };
        public static IEnumerable TestCases
        {
            get
            {
                yield return new TestCaseData().Returns(list);
                // yield return new Task { TaskId = 2, ParentId = 1, Priority = 10, TaskName = "test Task-2", StartDate = DateTime.Now, EndDate = DateTime.Now };

            }
        }
    }
}
