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
    public class AccountsControllerTest
    {
        [TestMethod]
        public void CreateAccount()
        {
            // Arrange
            AccountsController controller = new AccountsController();
            //Used the USER_ID and TOKEN from Login
            var acct = new ACCOUNT { USER_ID = 1, TOKEN = "38d3b826-eb14-41c7-b7ce-d7bddad8", ACCOUNT_NO = "1111111111",ACCOUNT_BALANCE = 10000000,FIRSTNAME="Juan", MIDDLENAME = "dela", LASTNAME = "Cruz" };
            // Act
            var result = controller.CreateAccount(acct);
            JSONViewModel res = JsonConvert.DeserializeObject<JSONViewModel>(result.Result);
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("100", res.StatusCode);
            Assert.AreEqual("Success", res.ResponseMessage);
        }
        [TestMethod]
        public void GetAccount()
        {
            // Arrange
            AccountsController controller = new AccountsController();
            //Used the USER_ID and TOKEN from Login
            var acct = new SearchViewModel { USER_ID = 1, TOKEN = "38d3b826-eb14-41c7-b7ce-d7bddad8", ACCOUNT_NO = "1111111111" };
            // Act
            var result = controller.GetAccount(acct);
            JSONViewModel res = JsonConvert.DeserializeObject<JSONViewModel>(result.Result);
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("100", res.StatusCode);
            Assert.AreEqual("Success", res.ResponseMessage);
        }
    }
}
