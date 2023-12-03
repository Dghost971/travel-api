using System;
using System.Collections.Generic;
using System.Linq;
using TravelAPI.Models;
using TravelAPI.DBContexts;
using TravelAPI.Services;
using TravelAPI.Controllers;


namespace TravelAPI.Services
{
    public class BudgetService
    {
        private readonly TravelDbContext _dbContext; // DbContext injecté

        public BudgetService(TravelDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        // Récupère tous les budgets depuis le DbContext
        public IEnumerable<Budget> GetAllBudgets()
        {
            return _dbContext.Budget.ToList();
        }

        // Récupère un budget par ID depuis le DbContext
        public Budget GetBudgetById(Guid id)
        {
            return _dbContext.Budget.FirstOrDefault(b => b.Id == id);
        }

        // Ajoute un nouveau budget au DbContext
        public ServiceActionResult<Budget> CreateBudget(Budget budget)
        {
            budget.Id = Guid.NewGuid();
            _dbContext.Budget.Add(budget);
            _dbContext.SaveChanges(); // Sauvegarde les modifications dans la base de données
            return ServiceActionResult<Budget>.FromSuccess(budget);
        }

        // Met à jour un budget existant dans le DbContext
        public ServiceActionResult<Budget> UpdateBudget(Guid id, Budget updatedBudget)
        {
            var existingBudget = _dbContext.Budget.FirstOrDefault(b => b.Id == id);
            if (existingBudget == null)
            {
                return ServiceActionResult<Budget>.FromError("Budget not found", ServiceActionErrorReason.NotFound);
            }

            existingBudget.TotalAmount = updatedBudget.TotalAmount;
            existingBudget.PlannedExpenses = updatedBudget.PlannedExpenses;
            existingBudget.RealExpenses = updatedBudget.RealExpenses;

            _dbContext.SaveChanges(); // Sauvegarde les modifications dans la base de données

            return ServiceActionResult<Budget>.FromSuccess(existingBudget);
        }

        // Supprime un budget du DbContext
        public ServiceActionResult<Budget> DeleteBudget(Guid id)
        {
            var existingBudget = _dbContext.Budget.FirstOrDefault(b => b.Id == id);
            if (existingBudget == null)
            {
                return ServiceActionResult<Budget>.FromError("Budget not found", ServiceActionErrorReason.NotFound);
            }

            _dbContext.Budget.Remove(existingBudget);
            _dbContext.SaveChanges(); // Sauvegarde les modifications dans la base de données

            return ServiceActionResult<Budget>.FromSuccess(existingBudget);
        }

        // Calcule les dépenses restantes pour un budget donné
        public ServiceActionResult<int> CalculateRemainingExpenses(Guid id)
        {
            var budget = _dbContext.Budget.FirstOrDefault(b => b.Id == id);
            if (budget == null)
            {
                return ServiceActionResult<int>.FromError("Budget not found", ServiceActionErrorReason.NotFound);
            }

            var remainingExpenses = budget.TotalAmount - budget.RealExpenses;
            return ServiceActionResult<int>.FromSuccess(remainingExpenses);
        }
    }
}