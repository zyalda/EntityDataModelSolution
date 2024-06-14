using BusinessLayer.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace DataPresentation
{
    [TestClass]
    public class EmployeeViewModelTests
    {
        [TestMethod]
        public void People_OnRefreshPeople_IsPopulated()
        {
            // Arrange
            var reader = new FakeEmployeeReader();
            var viewModel = new EmployeeReader(reader);

            // Act
            //viewModel.RefreshEmployee();

            // Assert
            Assert.IsNotNull(viewModel.Employee);
            Assert.AreEqual(2, viewModel.Employee.Count());
        }

        [TestMethod]
        public void People_OnClearPeople_IsEmpty()
        {
            // Arrange
            var reader = new FakeEmployeeReader();
            var viewModel = new EmployeeReader(reader);
            viewModel.RefreshEmployee();
            Assert.AreNotEqual(0, viewModel.Employee.Count(),
                "Invalid arrange");

            // Act
            viewModel.ClearEmployee();

            // Assert
            Assert.AreEqual(0, viewModel.Employee.Count());
        }
    }
}
