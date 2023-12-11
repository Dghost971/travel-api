using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using TravelAPI.Models;
using TravelAPI.DBContexts;
using TravelAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace TravelAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BudgetsController : ControllerBase
    {
        private readonly BudgetService _budgetService;

        public BudgetsController(BudgetService budgetService)
        {
            _budgetService = budgetService ?? throw new ArgumentNullException(nameof(budgetService));
        }

        // GET: api/budgets
        [HttpGet("get-all-budget")]
        public ActionResult<IEnumerable<Budget>> GetAllBudgets()
        {
            var budgets = _budgetService.GetAllBudgets();
            return Ok(budgets);
        }

        // GET: api/budgets/{id}
        [HttpGet("get-budget-by-id/{id}")]
        public ActionResult<Budget> GetBudgetById(Guid id)
        {
            var budget = _budgetService.GetBudgetById(id);
            if (budget == null)
            {
                return NotFound();
            }
            return Ok(budget);
        }

        // POST: api/budgets
        [HttpPost("create-new-budget")]
        public ActionResult<Budget> CreateBudget(Budget budget)
        {
            var createdBudget = _budgetService.CreateBudget(budget);
            return CreatedAtAction(nameof(GetBudgetById), new { id = createdBudget.Result.Id }, createdBudget.Result);
        }

        // PUT: api/budgets/{id}
        [HttpPut("update-budget/{id}")]
        public IActionResult UpdateBudget(Guid id, Budget updatedBudget)
        {
            var result = _budgetService.UpdateBudget(id, updatedBudget);
            if (!result.IsSuccess)
            {
                return NotFound();
            }
            return NoContent();
        }

        // DELETE: api/budgets/{id}
        [HttpDelete("delete-budget/{id}")]
        public IActionResult DeleteBudget(Guid id)
        {
            var result = _budgetService.DeleteBudget(id);
            if (!result.IsSuccess)
            {
                return NotFound();
            }
            return NoContent();
        }

        // GET: Budgets/remaining-expenses/{id}
        [HttpGet("remaining-expenses/{id}")]
        public ActionResult<int> CalculateRemainingExpenses(Guid id)
        {
            var remainingExpenses = _budgetService.CalculateRemainingExpenses(id);
            if (!remainingExpenses.IsSuccess)
            {
                return NotFound();
            }
            return Ok(remainingExpenses.Result);
        }
    }
}



