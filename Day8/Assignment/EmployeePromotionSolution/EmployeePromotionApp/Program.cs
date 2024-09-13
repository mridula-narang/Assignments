namespace EmployeePromotionApp
{
    internal class Program
    {
        EmployeePromotion employeePromotion = new EmployeePromotion();
        static void Main(string[] args)
        {
            var program = new Program();
            program.employeePromotion.InputEmployeeDetails();
            //program.employeePromotion.DisplayPromotionOrder(); //method to be called for easy question 1
            //program.employeePromotion.FindEmployeePosition(); //method to be called for easy question 2
            //program.employeePromotion.RemoveSpace(); //method to be called for easy question 3
            program.employeePromotion.SortEmployeeNames(); //method to be called for easy question 4
            
        }
    }
}
