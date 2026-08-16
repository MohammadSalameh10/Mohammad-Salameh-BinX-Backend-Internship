namespace TaskTrackerApi.Services.Classes
{
    public class OrderCalculator
    {
        public decimal CalculateTotal(decimal unitPrice, int quantity)
        {
            return unitPrice * quantity;
        }
    }
}