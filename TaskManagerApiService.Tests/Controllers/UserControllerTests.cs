using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Moq;
using NUnit.Framework;
using TaskManager.BusinessLayer;
using TaskManager.EntityLayer;
using TaskManagerApiService.Controllers;
using Task = TaskManager.EntityLayer.Task;

namespace TaskManagerApiService.Tests.Controllers
{
    [TestFixture]
    public class UserControllerTests
    {
        private Mock<IUserManager>  _userManager;
        [SetUp]
        public void Setup()
        {
            _userManager = new Mock<IUserManager>();
        }

        [Test]
        public void GetUsers_ReturnsListofUsers()
        {
            _userManager.Setup(x => x.GetUsers()).Returns(_data);
            UserController controller = new UserController(_userManager.Object);
            var actual =  controller.Get();

            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Any());
            Assert.IsTrue(actual.Count() == 2);
        }

        [Test]
        public void AddUser_ReturnsOkStatus()
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
            _userManager.Setup(x => x.AddUser(user)).Returns(true);
            UserController controller = new UserController(_userManager.Object);
            var actual = controller.PostUser(user);

            Assert.IsNotNull(actual);
        }

        [Test]
        public void UpdateUser_ReturnsOkStatus()
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
            _userManager.Setup(x => x.UpdateUser(1,user)).Returns(true);
            UserController controller = new UserController(_userManager.Object);
            var actual = controller.PutTask(1,user);

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
            _userManager.Setup(x => x.DeleteUser(1)).Returns(true);
            UserController controller = new UserController(_userManager.Object);
            var actual = controller.Delete(1);

            Assert.IsNotNull(actual);
        }



        private readonly IEnumerable<User> _data = new List<TaskManager.EntityLayer.User>()
        {
            new User {
                TaskId = 1010,
                ProjectId = 12,
                UserId = 10,
                EmployeeId = "tesfs",
                FirstName = "fname",
                LastName = "lanme"
               },
            new User {
                TaskId = 1010,
                ProjectId = 12,
                UserId = 10,
                EmployeeId = "tesfs",
                FirstName = "fname",
                LastName = "lanme"
                 }
        };
    }
}
