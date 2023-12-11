document
  .getElementById("buttonallbudget")
  .addEventListener("click", getAllBudgets);

function getAllBudgets() {
  fetch(`http://localhost:5254/Budgets/get-all-budget`)
    .then((response) => response.json())
    .then((data) => {
      displayAllbudgets(data);
    })
    .catch((error) => {
      console.error("Error fetching Budgets:", error);
    });
}

function displayAllbudgets(budgets) {
  const dataList = document.getElementById("budgetList");
  dataList.innerHTML = "";

  const table = document.createElement("table");
  table.innerHTML = `
      <thead>
        <tr>
          <th>Total Amount</th>
          <th>Planned Expenses</th>
          <th>Real Expenses</th>
        </tr>
      </thead>
      <tbody id="budgetListBody">
      </tbody>
    `;

  const dataListBody = table.querySelector("#budgetListBody");

  budgets.forEach((budget) => {
    const row = document.createElement("tr");
    row.innerHTML = `
        <td>${budget.totalAmount}</td>
        <td>${budget.plannedExpenses}</td>
        <td>${budget.realExpenses}</td>
      `;
    dataListBody.appendChild(row);
  });

  dataList.appendChild(table);
}

let currentBudgetId;

function getBudget() {
  const BudgetId = document.getElementById("budgetId").value;
  fetch(`http://localhost:5254/Budgets/get-budget-by-id/${BudgetId}`)
    .then((response) => {
      if (!response.ok) {
        throw new Error("Error fetching activity type");
      }
      return response.json();
    })
    .then((data) => {
      displayBudget(data);
      currentBudgetId = data.id; // Store the retrieved ID
    })
    .catch((error) => {
      console.error("Error fetching budget:", error);
      document.getElementById("budgetsResponse").innerText =
        "Error fetching budget";
    });
}

function displayBudget(budgets) {
  const responseDiv = document.getElementById("budgetsResponse");
  responseDiv.innerHTML = ""; // Clear previous data

  const budgetDetailsDiv = document.createElement("div");
  budgetDetailsDiv.innerHTML = `
      <h2>Budget Details:</h2>
      <p>Total: ${budgets.totalAmount}</p>
      <p>Planned Expenses: ${budgets.plannedExpenses}</p>
      <p>Real Expenses: ${budgets.realExpenses}</p>
      `;
  responseDiv.appendChild(budgetDetailsDiv);

  const updateForm = document.createElement("form");
  updateForm.id = "updateBudgetForm";

  const newTotalAmountInput = document.createElement("input");
  newTotalAmountInput.type = "number";
  newTotalAmountInput.placeholder = "Update Total Amount";
  newTotalAmountInput.value = budgets.totalAmount; // Set the value from the fetched data
  updateForm.appendChild(newTotalAmountInput);

  const newPlannedExpensesInput = document.createElement("input");
  newPlannedExpensesInput.type = "number";
  newPlannedExpensesInput.placeholder = "Update Planned Expenses";
  newPlannedExpensesInput.value = budgets.plannedExpenses;
  updateForm.appendChild(newPlannedExpensesInput);

  const newRealExpensesInput = document.createElement("input");
  newRealExpensesInput.type = "number";
  newRealExpensesInput.placeholder = "Update Real expenses";
  newRealExpensesInput.value = budgets.realExpenses;
  updateForm.appendChild(newRealExpensesInput);

  const updateButton = document.createElement("button");
  updateButton.textContent = "Update";
  updateButton.type = "button"; 
  updateButton.addEventListener("click", () => {
    handleUpdate(
      currentBudgetId,
      newTotalAmountInput.value,
      newPlannedExpensesInput.value,
      newRealExpensesInput.value
    );
  });

  updateForm.appendChild(updateButton);

  responseDiv.appendChild(updateForm);

  const deleteButton = document.createElement("button");
  deleteButton.textContent = "Delete";
  deleteButton.addEventListener(
    "click",
    () => handleDelete(currentBudgetId) // Pass currentBudgetId here
  );
  responseDiv.appendChild(deleteButton);
}

function handleUpdate(
  currentBudgetId,
  newTotalAmount,
  newPlannedExpenses,
  newRealExpenses
) {
  if (!currentBudgetId) {
    console.error("No budget selected");
    return;
  }
  if (
    !newTotalAmount ||
    !newPlannedExpenses ||
    !newRealExpenses
  ) {
    console.error("Please fill in all fields");
    return;
  }

  const updatedBudget = {
    totalAmount: newTotalAmount,
    plannedExpenses: newPlannedExpenses,
    realExpenses: newRealExpenses,
  };

  fetch(`http://localhost:5254/Budgets/update-budget/${currentBudgetId}`, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({updatedBudget}),
  })
    .then((response) => {
      if (!response.ok) {
        throw new Error("Error updating budget");
      }
      console.log("Budget updated successfully");
      // Handle success scenarios here
    })
    .catch((error) => {
      console.error("Error updating budget:", error);
      // Handle error scenarios here
    });
}

function handleDelete(currentBudgetId) {
  if (!currentBudgetId) {
    console.error("No activity type selected");
    return;
  }

  fetch(`http://localhost:5254/Budgets/delete-budget/${currentBudgetId}`, {
    method: "DELETE",
  })
    .then((response) => {
      if (!response.ok) {
        throw new Error("Error deleting budget");
      }
      console.log("Budget deleted successfully");
      // Handle success scenarios here
    })
    .catch((error) => {
      console.error("Error deleting budget:", error);
      // Handle error scenarios here
    });
}

function createBudget() {
  const totalAmountInput = document.getElementById("totalAmount");
  const plannedExpensesInput = document.getElementById("plannedExpenses");
  const budgetResponse = document.getElementById("budgetResponse");

  const totalAmount = totalAmountInput.value.trim();
  const plannedExpenses = plannedExpensesInput.value.trim();

  fetch("http://localhost:5254/Budgets/create-new-budget", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      totalamount: totalAmount,
      plannedexpenses: plannedExpenses,
    }),
  })
    .then((response) => {
      if (response.ok) {
        totalAmountInput.style.border = "2px solid green";
        plannedExpensesInput.style.border = "2px solid green";
      } else {
        totalAmountInput.style.border = "2px solid red";
        plannedExpensesInput.style.border = "2px solid red";
      }
      return response.json();
    })
    .then((data) => {
      if (data.isSuccess) {
        budgetResponse.textContent = "Budget created successfully.";
      } else {
        budgetResponse.textContent = `Error: ${data.errorMessage}`;
      }
    })
    .catch((error) => {
      budgetResponse.textContent = `Error: ${error.message}`;
    });
}

function RemainingMoney() {
  const budgetIdInput = document.getElementById("moneybudgetId");
  const remainingBudgetDiv = document.getElementById("remainningbudget");

  const budgetId = budgetIdInput.value;

  fetch(`http://localhost:5254/Budgets/remaining-expenses/${budgetId}`)
    .then((response) => {
      if (response.ok) {
        return response.json();
      } else {
        throw new Error("Error fetching remaining budget");
      }
    })
    .then((data) => {
      remainingBudgetDiv.textContent = `Remaining Budget: $${data}`;
    })
    .catch((error) => {
      remainingBudgetDiv.textContent = `Error: ${error.message}`;
    });
}
