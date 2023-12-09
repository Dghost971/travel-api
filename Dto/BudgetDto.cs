using TravelAPI.Models;

namespace TravelAPI.Dto
{
    public class BudgetDTO
    {
        public Guid Id { get; set; }
        public int TotalAmount { get; set; }
        public int PlannedExpenses { get; set; }
        public int RealExpenses { get; set; }
    }

}