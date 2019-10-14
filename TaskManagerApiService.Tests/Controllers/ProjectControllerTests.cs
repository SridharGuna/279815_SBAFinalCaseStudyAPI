using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using TaskManager.BusinessLayer;
using TaskManager.EntityLayer;
using TaskManager.EntityLayer.Models;
using TaskManagerApiService.Controllers;

namespace TaskManagerApiService.Tests.Controllers
{
    [TestFixture]
    public class ProjectControllerTests
    {
        private Mock<IProjectManager> _projectManager;
        [SetUp]
        public void Setup()
        {
            _projectManager = new Mock<IProjectManager>();
        }
        [Test]
        public void GetUsers_ReturnsListofUsers()
        {
            _projectManager.Setup(x => x.GetProject()).Returns(_data);
            ProjectController controller = new ProjectController(_projectManager.Object);
            var actual = controller.Get();

            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Any());
            Assert.IsTrue(actual.Count() == 2);
        }

        [Test]
        public void AddUser_ReturnsOkStatus()
        {
            var user = new ProjectViewModel
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1),
                ProjectId = 1010,
                ManagerId = 10,
                ProjectName = "test",
                Priority = 01
            };
            _projectManager.Setup(x => x.AddProject(user)).Returns(true);
            ProjectController controller = new ProjectController(_projectManager.Object);
            var actual = controller.Post(user);

            Assert.IsNotNull(actual);
        }

        [Test]
        public void UpdateUser_ReturnsOkStatus()
        {
            var user = new ProjectViewModel
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1),
                ProjectId = 1010,
                ManagerId = 10,
                ProjectName = "test",
                Priority = 01
            };
            _projectManager.Setup(x => x.UpdateProject(1, user)).Returns(true);
            ProjectController controller = new ProjectController(_projectManager.Object);
            var actual = controller.Put(1, user);

            Assert.IsNotNull(actual);
        }

        [Test]
        public void DeleteUser_ReturnsOkStatus()
        {
            var user = new User
            {
                TaskId = 1010,
                ProjectId = 12,
                UserId = 10,
                EmployeeId = "tesfs",
                FirstName = "fname",
                LastName = "lanme"
            };
            _projectManager.Setup(x => x.DeleteProject(1)).Returns(true);
            ProjectController controller = new ProjectController(_projectManager.Object);
            var actual = controller.Delete(1);

            Assert.IsNotNull(actual);
        }



        private readonly IEnumerable<ProjectViewModel> _data = new List<ProjectViewModel>()
        {
            new ProjectViewModel {
              StartDate = DateTime.Now,
                EndDate =  DateTime.Now.AddDays(1),
                ProjectId = 1010,
                ManagerId = 10,
                ProjectName = "test",
                Priority = 01
               },
            new ProjectViewModel {
                StartDate = DateTime.Now,
                EndDate =  DateTime.Now.AddDays(1),
                ProjectId = 1010,
                ManagerId = 10,
                ProjectName = "test",
                Priority = 01
                 }
        };
    }
}
