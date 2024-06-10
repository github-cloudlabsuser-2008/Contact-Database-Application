using CRUD_application_2.Controllers;
using CRUD_application_2.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CRUD_application_2.Tests.Controllers
{
    [TestClass]
    public class UserControllerTests
    {
        [TestMethod]
        public void Index_ReturnsViewWithUserList()
        { 
            // Arrange
            var controller = new UserController();
            var userList = new List<User>
            {
                new User { Id = 1, Name = "John", Email = "john@example.com", Age = 25 },
                new User { Id = 2, Name = "Jane", Email = "jane@example.com", Age = 30 }
            };
            UserController.userlist = userList;

            // Act
            var result = controller.Index() as ViewResult;
            var model = result.Model as List<User>;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(userList.Count, model.Count);
            Assert.AreEqual(userList[0].Name, model[0].Name);
            Assert.AreEqual(userList[1].Email, model[1].Email);
        }

        [TestMethod]
        public void Details_ExistingUserId_ReturnsViewWithUser()
        {
            // Arrange
            var controller = new UserController();
            var userList = new List<User>
            {
                new User { Id = 1, Name = "John", Email = "john@example.com", Age = 25 },
                new User { Id = 2, Name = "Jane", Email = "jane@example.com", Age = 30 }
            };
            UserController.userlist = userList;
            var userId = 1;

            // Act
            var result = controller.Details(userId) as ViewResult;
            var model = result.Model as User;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(userList[0].Name, model.Name);
            Assert.AreEqual(userList[0].Email, model.Email);
        }

        [TestMethod]
        public void Details_NonExistingUserId_ReturnsHttpNotFound()
        {
            // Arrange
            var controller = new UserController();
            var userList = new List<User>
            {
                new User { Id = 1, Name = "John", Email = "john@example.com", Age = 25 },
                new User { Id = 2, Name = "Jane", Email = "jane@example.com", Age = 30 }
            };
            UserController.userlist = userList;
            var userId = 3;

            // Act
            var result = controller.Details(userId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public void Create_ValidUser_RedirectsToIndex()
        {
            // Arrange
            var controller = new UserController();
            var userList = new List<User>();
            UserController.userlist = userList;
            var user = new User { Id = 1, Name = "John", Email = "john@example.com", Age = 25 };

            // Act
            var result = controller.Create(user) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void Edit_ExistingUserId_ValidUser_RedirectsToIndex()
        {
            // Arrange
            var controller = new UserController();
            var userList = new List<User>
            {
                new User { Id = 1, Name = "John", Email = "john@example.com", Age = 25 },
                new User { Id = 2, Name = "Jane", Email = "jane@example.com", Age = 30 }
            };
            UserController.userlist = userList;
            var userId = 1;
            var user = new User { Id = 1, Name = "Updated John", Email = "updatedjohn@example.com", Age = 26 };

            // Act
            var result = controller.Edit(userId, user) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Updated John", userList.First(u => u.Id == userId).Name);
            Assert.AreEqual("updatedjohn@example.com", userList.First(u => u.Id == userId).Email);
        }

        [TestMethod]
        public void Edit_NonExistingUserId_ReturnsHttpNotFound()
        {
            // Arrange
            var controller = new UserController();
            var userList = new List<User>
            {
                new User { Id = 1, Name = "John", Email = "john@example.com", Age = 25 },
                new User { Id = 2, Name = "Jane", Email = "jane@example.com", Age = 30 }
            };
            UserController.userlist = userList;
            var userId = 3;
            var user = new User { Id = 3, Name = "New User", Email = "newuser@example.com", Age = 20 };

            // Act
            var result = controller.Edit(userId, user);

            // Assert
            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public void Delete_ExistingUserId_RedirectsToIndex()
        {
            // Arrange
            var controller = new UserController();
            var userList = new List<User>
            {
                new User { Id = 1, Name = "John", Email = "john@example.com", Age = 25 },
                new User { Id = 2, Name = "Jane", Email = "jane@example.com", Age = 30 }
            };
            UserController.userlist = userList;
            var userId = 1;

            // Act
            var result = controller.Delete(userId, null) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsFalse(userList.Any(u => u.Id == userId));
        }

        [TestMethod]
        public void Delete_NonExistingUserId_ReturnsHttpNotFound()
        {
            // Arrange
            var controller = new UserController();
            var userList = new List<User>
            {
                new User { Id = 1, Name = "John", Email = "john@example.com", Age = 25 },
                new User { Id = 2, Name = "Jane", Email = "jane@example.com", Age = 30 }
            };
            UserController.userlist = userList;
            var userId = 3;

            // Act
            var result = controller.Delete(userId, null);

            // Assert
            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public void Reset_ClearsUserList_RedirectsToIndex()
        {
            // Arrange
            var controller = new UserController();
            var userList = new List<User>
            {
                new User { Id = 1, Name = "John", Email = "john@example.com", Age = 25 },
                new User { Id = 2, Name = "Jane", Email = "jane@example.com", Age = 30 }
            };
            UserController.userlist = userList;

            // Act
            var result = controller.Reset() as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual(0, userList.Count);
        }
    }
}
