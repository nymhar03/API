using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project.WebApi;
using Project.WebApi.Controllers;
using Project.Model;
using Newtonsoft.Json;
using Project.WebApi.Models;

namespace Project.Tests.Controllers
{
    [TestClass]
    public class UsersControllerTest
    {
        [TestMethod]
        public void CreateUserWithEmptyOrNullUsername()
        {
            // Arrange
            UsersController controller = new UsersController();
            var user = new USER { PASSWORD = "12345", CONFIRM_PASSWORD = "12346" };
            // Act
            var result = controller.CreateUser(user);
            JSONViewModel res = JsonConvert.DeserializeObject<JSONViewModel>(result.Result);
            // Assert
            Assert.IsNotNull(result);
            Assert.AreNotEqual("100", res.StatusCode);
            Assert.AreNotEqual("Success", res.ResponseMessage);
        }
        [TestMethod]
        public void CreateUserWithEmptyOrNullPassword()
        {
            // Arrange
            UsersController controller = new UsersController();
            var user = new USER { PASSWORD = "12345", CONFIRM_PASSWORD = "12346" };
            // Act
            var result = controller.CreateUser(user);
            JSONViewModel res = JsonConvert.DeserializeObject<JSONViewModel>(result.Result);
            // Assert
            Assert.IsNotNull(result);
            Assert.AreNotEqual("100", res.StatusCode);
            Assert.AreNotEqual("Success", res.ResponseMessage);
        }
        [TestMethod]
        public void CreateUserWithDifferentPasswords()
        {
            // Arrange
            UsersController controller = new UsersController();
            var user = new USER { USERNAME = "mark", PASSWORD = "12345", CONFIRM_PASSWORD = "12346" };
            // Act
            var result = controller.CreateUser(user);
            JSONViewModel res = JsonConvert.DeserializeObject<JSONViewModel>(result.Result);
            // Assert
            Assert.IsNotNull(result);
            Assert.AreNotEqual("100", res.StatusCode);
            Assert.AreNotEqual("Success", res.ResponseMessage);
        }
        [TestMethod]
        public void CreateUserWithCorrectInfo()
        {
            // Arrange
            UsersController controller = new UsersController();
            var user = new USER { USERNAME = "mark", PASSWORD = "12345", CONFIRM_PASSWORD = "12345" };
            // Act
            var result = controller.CreateUser(user);
            JSONViewModel res = JsonConvert.DeserializeObject<JSONViewModel>(result.Result);
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("100", res.StatusCode);
            Assert.AreEqual("Success", res.ResponseMessage);
        }
        
        [TestMethod]
        public void AuthenticateUserWithNoUsername()
        {
            // Arrange
            UsersController controller = new UsersController();
            var user = new USER { PASSWORD = "12345" };
            // Act
            var result = controller.AuthenticateUser(user);
            JSONViewModel res = JsonConvert.DeserializeObject<JSONViewModel>(result.Result);
            // Assert
            Assert.IsNull(res.Data);
            Assert.AreNotEqual("100", res.StatusCode);
            Assert.AreNotEqual("Success", res.ResponseMessage);
        }
        [TestMethod]
        public void AuthenticateUserWithNoPassword()
        {
            // Arrange
            UsersController controller = new UsersController();
            var user = new USER { USERNAME = "mark" };
            // Act
            var result = controller.AuthenticateUser(user);
            JSONViewModel res = JsonConvert.DeserializeObject<JSONViewModel>(result.Result);
            // Assert
            Assert.IsNull(res.Data);
            Assert.AreNotEqual("100", res.StatusCode);
            Assert.AreNotEqual("Success", res.ResponseMessage);
        }
        [TestMethod]
        public void AuthenticateUserWithCorrectInfo()
        {
            // Arrange
            UsersController controller = new UsersController();
            var user = new USER { USERNAME = "mark", PASSWORD = "12345" };
            // Act
            var result = controller.AuthenticateUser(user);
            JSONViewModel res = JsonConvert.DeserializeObject<JSONViewModel>(result.Result);
            // Assert
            Assert.IsNotNull(res.Data);
            Assert.AreEqual("100", res.StatusCode);
            Assert.AreEqual("Success", res.ResponseMessage);
        }
    }
}
