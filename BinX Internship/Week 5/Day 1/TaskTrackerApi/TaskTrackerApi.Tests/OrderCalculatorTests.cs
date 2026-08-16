using TaskTrackerApi.Services.Classes;

namespace TaskTrackerApi.Tests
{
    public class OrderCalculatorTests
    {
        [Fact]
        public void CalculateTotal_WithValidPriceAndQuantity_ReturnsCorrectTotal()
        {
            // Arrange
            var calculator = new OrderCalculator();
            decimal unitPrice = 25m;
            int quantity = 4;

            // Act
            decimal result = calculator.CalculateTotal(unitPrice, quantity);

            // Assert
            Assert.Equal(100m, result);
        }

        [Fact]
        public void CalculateTotal_WithZeroQuantity_ReturnsZero()
        {
            // Arrange
            var calculator = new OrderCalculator();
            decimal unitPrice = 25m;
            int quantity = 0;

            // Act
            decimal result = calculator.CalculateTotal(unitPrice, quantity);

            // Assert
            Assert.Equal(0m, result);
        }

        [Fact]
        public void CalculateTotal_WithDecimalPrice_ReturnsCorrectTotal()
        {
            // Arrange
            var calculator = new OrderCalculator();
            decimal unitPrice = 19.99m;
            int quantity = 3;

            // Act
            decimal result = calculator.CalculateTotal(unitPrice, quantity);

            // Assert
            Assert.Equal(59.97m, result);
        }

        [Theory]
        [InlineData(10, 2, 20)]
        [InlineData(15, 3, 45)]
        [InlineData(25, 0, 0)]
        public void CalculateTotal_WithDifferentInputs_ReturnsCorrectTotal(int unitPrice, int quantity, int expected)
        {
            // Arrange
            var calculator = new OrderCalculator();

            // Act
            decimal result = calculator.CalculateTotal(unitPrice, quantity);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}