namespace TravelAPI.Models
{
    public class Budget{

         public Guid Id { get; set; }
          
        public int TotalAmount { get; set; } //Total amount allocated for the budget.
        public int PlannedExpenses  { get; set; } // Planned expenses for the budget.
        
        public int RealExpenses { get; set; }// Actual expenses spent from the budget

    }
}