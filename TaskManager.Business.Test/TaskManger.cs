using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using TaskManager.BusinessLayer;
using TaskManager.EntityLayer;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert; 

namespace TaskManager.Business.Test
{
    [TestFixture]
    public class TaskManger
    {
        private ITaskManager _taskManager;

        public TaskManger()
        {
            _taskManager =new BusinessLayer.TaskManager();
        }

        [Test]
        public void AddTask()
        {
            var task = new Task
            {
                TaskId = 1010,
                ParentId = 1,
                Priority = 10,
                TaskName = "test Task-1",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                UserId = 1
            };
            var actual = _taskManager.AddTask(task);

            Assert.AreEqual(true, actual);
            Assert.IsTrue(actual);

        }

        [Test]
        public void GetTask()
        {
          var actual =  _taskManager.GetTask();
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Count()>0);
        }

        [Test]
        public void DeleteTask()
        {
            AddTask();
            var expected_TaskID = 1;
            var actual = _taskManager.DeleteTask(expected_TaskID);

            Assert.IsNotNull(actual);
        }
    }
}
