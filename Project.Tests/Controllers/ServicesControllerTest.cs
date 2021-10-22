using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project.WebApi.Controllers;

namespace WebApplication1.Tests.Controllers
{
    [TestClass]
    public class ServicesControllerTest
    {
        [TestMethod]
        public void BubbleSort()
        {
            UserController controller = new UserController();
            var sortModel = new SortModel {InputValue="bubble",SortType="Bubble Sort" };
            // Act
            var result = controller.Sort(sortModel);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("bbbelu", result);
            Assert.AreNotEqual("bubble", result);
        }
        [TestMethod]
        public void QuickOrMergeSort()
        {
            ServicesController controller = new ServicesController();
            var sortModel = new SortModel { InputValue = "quick", SortType = "Quick/Merge Sort" };
            // Act
            var result = controller.Sort(sortModel);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("cikqu", result);
            Assert.AreNotEqual("Quick", result);
        }
    }
}
