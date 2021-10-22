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
    public class PaymentsControllerTest
    {
        [TestMethod]
        public void CreatePayment()
        {
            // Arrange
            PaymentsController controller = new PaymentsController();
            //Used the USER_ID and TOKEN from Login
            var pay = new PAYMENT { USER_ID = 1, TOKEN = "38d3b826-eb14-41c7-b7ce-d7bddad8", ACCOUNT_NO = "1111111111",AMOUNT = 1000,REMARKS="BILLS PAYMENT" };
            // Act
            var result = controller.CreatePayment(pay);
            JSONViewModel res = JsonConvert.DeserializeObject<JSONViewModel>(result.Result);
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("100", res.StatusCode);
            Assert.AreEqual("Success", res.ResponseMessage);
        }
        [TestMethod]
        public void GetPaymentsByAccountNo()
        {
            // Arrange
            PaymentsController controller = new PaymentsController();
            //Used the USER_ID and TOKEN from Login
            var acct = new SearchViewModel { USER_ID = 1, TOKEN = "38d3b826-eb14-41c7-b7ce-d7bddad8", ACCOUNT_NO = "1111111111" };
            // Act
            var result = controller.GetPaymentsByAccountNo(acct);
            JSONViewModel res = JsonConvert.DeserializeObject<JSONViewModel>(result.Result);
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("100", res.StatusCode);
            Assert.AreEqual("Success", res.ResponseMessage);
        }
        
    }
}
