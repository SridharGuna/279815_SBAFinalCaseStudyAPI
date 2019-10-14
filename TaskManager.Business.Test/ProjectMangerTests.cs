using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using TaskManager.BusinessLayer;
using TaskManager.EntityLayer;
using TaskManager.EntityLayer.Models;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert; 

namespace TaskManager.Business.Test
{
    [TestFixture]
    public class ProjectMangerTests
    {
        private IProjectManager _taskManager;

        public ProjectMangerTests()
        {
            _taskManager =new BusinessLayer.ProjectManager();
        }

        [Test]
        public void AddProject_ReturnOkResult()
        {
            var project = new ProjectViewModel
            {
               StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1),
                ProjectId = 1020,
                ManagerId = 2563,
                Priority = 10,
                ProjectName = "Project Name"
            };
            var actual = _taskManager.AddProject(project);

            Assert.AreEqual(true, actual);
            Assert.IsTrue(actual);

        }

        [Test]
        public void GetProject_ReturnsResults()
        {
          var actual =  _taskManager.GetProject();
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Count()>0);
        }

        [Test]
        public void DeleteProject_ReturnsNotNull()
        {
            AddProject_ReturnOkResult();
            var expected_TaskID = 1;
            var actual = _taskManager.DeleteProject(expected_TaskID);

            Assert.IsNotNull(actual);
        }
    }
}
