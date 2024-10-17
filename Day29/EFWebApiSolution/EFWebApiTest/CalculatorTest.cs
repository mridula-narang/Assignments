using EFWebApiApp.Interfaces;
using EFWebApiApp.Services;
namespace EFWebApiTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase(10, 20, 30)]
        [TestCase(int.MaxValue, -20, (int.MaxValue))]
        [TestCase(int.MinValue, 20, (int.MinValue))]
        [TestCase(0, 0, 0)]
        public void AddTest(int n1, int n2, int result)
        {
            // Arrange
            int num1 = n1, num2 = n2;
            ICalculate calculate = new CalculateService();
            // Act
            int actualResult = calculate.Add(num1, num2);
            // Assert
            Assert.AreEqual(result, actualResult);
        }
    }
}