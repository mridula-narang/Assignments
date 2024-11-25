using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeExpenseTrackingApp
{
    internal class Expense
    {
        internal enum ExpenseStatus
        {
            Pending,
            Approved,
            Rejected
        }
        public Expense()
        {
        }

        public Expense(int id, decimal amount, string description, string category, int eId, string status)
        {
            Id = id;
            Amount = amount;
            Description = description;
            Category = category;
            EId = eId;
            Status = "Pending";
            ClosedDate = null;
        }

        public int Id { get; set; } //id of the expense
        public decimal Amount { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public string Category { get; set; } = string.Empty;
        public int EId { get; set; } //id of the employee who made the expense
        public string Status { get; set; } = string.Empty; //status of the expense
        public DateTime? ClosedDate { get; set; } //date when the expense was approved or rejected

        public bool ChangeStatus(string newStatus)
        {
            if (Status == "Approved" || Status == "Rejected")
            {
                Console.WriteLine("This status has either been approved or rejected. Please add another expense");
                return false;
            }
            Status = newStatus;
            if (newStatus == "Approved" || newStatus == "Rejected")
            {
                ClosedDate = DateTime.Now;
                Console.WriteLine($"Expense with id {Id} is amrked as {newStatus}");
            }
            return true;

        }
        // add an expense
        public void AddExpense()
        {
            Console.WriteLine("Enter Expense Id: ");
            Id = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter Expense Amount: ");
            Amount = Convert.ToDecimal(Console.ReadLine());

            Console.WriteLine("Enter Expense Description: ");
            Description = Console.ReadLine() ?? "";

            Console.WriteLine("Enter Expense Category: ");
            Category = Console.ReadLine() ?? "";

            Console.WriteLine("Enter Employee Id: ");
            EId = Convert.ToInt32(Console.ReadLine());

            CreatedDate = DateTime.Now;
            Status = "Pending"; // Default status when creating an expense
            Console.WriteLine("Expense has been successfully added.");
        }
        //view expense details
        public void ViewExpense()
        {
            Console.WriteLine($"Expense Id: {Id}");
            Console.WriteLine($"Amount: {Amount}");
            Console.WriteLine($"Description: {Description}");
            Console.WriteLine($"Category: {Category}");
            Console.WriteLine($"Employee Id: {EId}");
            Console.WriteLine($"Status: {Status}");
            Console.WriteLine($"Created Date: {CreatedDate}");
            if (ClosedDate != null)
            {
                Console.WriteLine($"Closed Date: {ClosedDate}");
            }
        }

        //approve or reject an expense
        public void ApproveOrRejectExpense()
        {
            Console.WriteLine("Enter new status (Approved/Rejected): ");
            string newStatus = Console.ReadLine();

            if (newStatus == "Approved" || newStatus == "Rejected")
            {
                ChangeStatus(newStatus);
            }
            else
            {
                Console.WriteLine("Invalid status entered.");
            }
        }
        public override string ToString()
        {
            var expenseDetails = $"Expense Id: {Id}\nAmount: {Amount:C}\nDescription: {Description}\nCategory: {Category}\nEmployee Id: {EId}\nStatus: {Status}\nCreated Date: {CreatedDate}\nClosed Date: ";
            expenseDetails += ClosedDate == null ? "-" : ClosedDate.ToString();
            return expenseDetails;
        }
    }
}
