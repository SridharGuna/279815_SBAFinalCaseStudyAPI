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
    public class UserMangerTests
    {
        private IUserManager _taskManager;

        public UserMangerTests()
        {
            _taskManager =new BusinessLayer.UserManager();
        }

        [Test]
        public void AddUser_ReturnsOkResult()
        {
            var user = new User
            {
                TaskId = 1020,
                ProjectId = 1065,
                EmployeeId = "58FDSX",
                FirstName = "FirstName",
                LastName = "LastName"
            };
            var actual = _taskManager.AddUser(user);

            Assert.AreEqual(true, actual);
            Assert.IsTrue(actual);

        }

        [Test]
        public void GetUsers_ReturnsListOfUsers()
        {
            var actual = _taskManager.GetUsers();
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Count() > 0);
        }

        [Test]
        public void DeleteTask()
        {
            AddUser_ReturnsOkResult();
            var expected_UserID = 1010;
            var actual = _taskManager.DeleteUser(expected_UserID);

            Assert.IsNotNull(actual);
        }
    }
}
