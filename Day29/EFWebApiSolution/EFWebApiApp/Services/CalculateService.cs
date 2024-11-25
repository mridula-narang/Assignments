using EFWebApiApp.Interfaces;

namespace EFWebApiApp.Services
{
    public class CalculateService : ICalculate
    {
        public int Add(double a, double b)
        {
            return (int)(a + b);
        }
        public int Multiply(double a, double b)
        {
            return (int)(a * b);
        }
    }
}
