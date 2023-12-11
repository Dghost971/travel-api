document.getElementById("buttonallact").addEventListener("click", getAllActivities);
document.getElementById("buttonupcomingact").addEventListener("click", getUpcomingActivities);



function getAllActivities() {
  fetch(`http://localhost:5254/Activities/get-all-activities`)
    .then((response) => response.json())
    .then((data) => {
      displayActivityTypes(data);
    })
    .catch((error) => {
      console.error("Error fetching activity types:", error);
    });
}

function displayAllActivities(activites) {
  const dataList = document.getElementById("activitesList");
  dataList.innerHTML = "";

  activites.forEach((activites) => {
    const listItem = document.createElement("li");
    listItem.innerHTML = `
        <p><strong>Name:</strong> ${activites.name}</p>
        <p><strong>Description:</strong> ${activites.description}</p>
        <p><strong>Date:</strong> ${activites.date}</p>
        <p><strong>Start Time:</strong> ${activites.startTime}</p>
        <p><strong>End Time:</strong> ${activites.endTime}</p>
      `;
    dataList.appendChild(listItem);
  });
}

function addNewActivity() {
  document
    .getElementById("createActivityForm")
    .addEventListener("submit", function (event) {
      event.preventDefault();

      const activityName = document.getElementById("activityName").value;
      const activityDescription = document.getElementById(
        "activityDescription"
      ).value;
      const activityDate = document.getElementById("activityDate").value;
      const activityStartTime =
        document.getElementById("activityStartTime").value;
      const activityEndTime = document.getElementById("activityEndTime").value;
      const activityType = document.getElementById("activityType").value;
      const activityTypePattern = /[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}/;
      const activityTypeIdMatch = activityType.match(activityTypePattern);
  
      const activityTypeId = activityTypeIdMatch[0];
      const activityTypeName = activityType.substring(activityTypeId.length + 1);


      const activityData = {
        name: activityName,
        description: activityDescription,
        date: activityDate,
        startTime: activityStartTime,
        endTime: activityEndTime,
        activityType: {
            id: activityTypeId,
            name: activityTypeName
          }
      };

      const activityTypeNameInput = document.getElementById("activityName");

      fetch("http://localhost:5254/Activities/add-new-activities", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(activityData),
      })
        .then((response) => {
          if (!response.ok) {
            throw new Error("Error adding activity");
          }
          if (response.ok) {
            activityTypeNameInput.style.border = "2px solid green";
          } else {
            activityTypeNameInput.style.border = "2px solid red";
          }
          return response.json();
        })
        .then((data) => {
          console.log("Activity added successfully:", data);
          const activityResponse = document.getElementById("activityResponse");
          activityResponse.innerHTML = "<p>Activity created successfully!</p>";
        })
        .catch((error) => {
          console.error("Error:", error);
          const activityResponse = document.getElementById("activityResponse");
          activityResponse.innerHTML = "<p>Error creating activity</p>";
          activityTypeNameInput.style.border = "2px solid red"; // Change border on error
        });

      // Clear the form fields after submission (optional)
      document.getElementById("activityName").value = "";
      document.getElementById("activityDescription").value = "";
      document.getElementById("activityType").value = "";
      document.getElementById("activityDate").value = "";
      document.getElementById("activityStartTime").value = "";
      document.getElementById("activityEndTime").value = "";
    });
}

function fetchActivityTypes() {
  fetch("http://localhost:5254/ActivityType/get-all-activitytype")
    .then((response) => {
      if (!response.ok) {
        throw new Error("Error fetching Activity Types");
      }
      return response.json();
    })
    .then((data) => {
      displayActivityTypes(data.result);
    })
    .catch((error) => {
      console.error("Error fetching Activity Types:", error);
    });
}

function displayActivityTypes(activityTypes) {
  const activityTypeDropdown = document.getElementById("activityType");
  activityTypeDropdown.innerHTML = ""; // Clear previous options

  activityTypes.forEach((activityType) => {
    const option = document.createElement("option");
    option.value = `${activityType.id}-${activityType.name}`;
    option.textContent = activityType.name;
    activityTypeDropdown.appendChild(option);
  });
}

// Fetch and populate activity types on page load
window.addEventListener("load", fetchActivityTypes);

let currentActivitesId = null;

function getActivites() {
  const activitesId = document.getElementById("activitesId").value;
  fetch(
    `http://localhost:5254/Activities//get-activitytype-by-id/${activitesId}`
  )
    .then((response) => {
      if (!response.ok) {
        throw new Error("Error fetching activite");
      }
      return response.json();
    })
    .then((data) => {
      displayActivte(data);
      currentActivitesId = data.result.id; // Store the retrieved ID
    })
    .catch((error) => {
      console.error("Error fetching activity type:", error);
      document.getElementById("activitesResponse").innerText =
        "Error fetching activity type";
    });
}

function handleUpdate(
  currentActivitesId,
  newName,
  newDescription,
  newActivityType,
  newDate,
  newStartTime,
  newEndTime
) {
  if (!currentActivitesId) {
    console.error("No activity selected");
    return;
  }

  if (
    !newName ||
    !newDescription ||
    !newActivityType ||
    !newDate ||
    !newStartTime ||
    !newEndTime
  ) {
    console.error("Please fill in all fields");
    return;
  }

  const updatedActivity = {
    name: newName,
    description: newDescription,
    activityType: newActivityType,
    date: newDate,
    startTime: newStartTime,
    endTime: newEndTime,
  };

  fetch(
    `http://localhost:5254/Activities/update-activities/${currentActivitesId}`,
    {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(updatedActivity),
    }
  )
    .then((response) => {
      if (!response.ok) {
        throw new Error("Error updating activity");
      }
      console.log("Activity updated successfully");
      // Handle success scenarios here
    })
    .catch((error) => {
      console.error("Error updating activity:", error);
      // Handle error scenarios here
    });
}

function handleDelete(currentActivitesId) {
  if (!currentActivitesId) {
    console.error("No activity type selected");
    return;
  }

  fetch(
    `http://localhost:5254/Activities/delete-activities/${currentActivitesId}`,
    {
      method: "DELETE",
    }
  )
    .then((response) => {
      if (!response.ok) {
        throw new Error("Error deleting activites");
      }
      console.log("Activite deleted successfully");
      // Handle success scenarios here
    })
    .catch((error) => {
      console.error("Error deleting activite:", error);
      // Handle error scenarios here
    });
}

function displayActivte(activites) {
  const responseDiv = document.getElementById("activitesResponse");
  responseDiv.innerHTML = ""; // Clear previous data

  const activityDetailsDiv = document.createElement("div");
  activityDetailsDiv.innerHTML = `
    <h2>Activity Details:</h2>
    <p>Activity Name: ${activites.result.name}</p>
    <p>Activity Description: ${activites.result.description}</p>
    <p>Activity Type: ${activites.result.activityType}</p>
    <p>Activity Date: ${activites.result.date}</p>
    <p>Start Time: ${activites.result.startTime}</p>
    <p>End Time: ${activites.result.endTime}</p>
    `;
  responseDiv.appendChild(activityDetailsDiv);

  const updateForm = document.createElement("form");
  updateForm.id = "updateActivityForm";

  const newNameInput = document.createElement("input");
  newNameInput.type = "text";
  newNameInput.placeholder = "New Activity Name";
  newNameInput.value = activites.result.name; // Set the value from the fetched data
  updateForm.appendChild(newNameInput);

  const newDescriptionInput = document.createElement("input");
  newDescriptionInput.type = "text";
  newDescriptionInput.placeholder = "New Activity Description";
  newDescriptionInput.value = activites.result.description;
  updateForm.appendChild(newDescriptionInput);

  const newActivityTypeInput = document.createElement("input");
  newActivityTypeInput.type = "text";
  newActivityTypeInput.placeholder = "New Activity Type";
  newActivityTypeInput.value = activites.result.activityType;
  updateForm.appendChild(newActivityTypeInput);

  const newDateInput = document.createElement("input");
  newDateInput.type = "date";
  newDateInput.value = activites.result.date;
  updateForm.appendChild(newDateInput);

  const newStartTimeInput = document.createElement("input");
  newStartTimeInput.type = "time";
  newStartTimeInput.value = activites.result.startTime;
  updateForm.appendChild(newStartTimeInput);

  const newEndTimeInput = document.createElement("input");
  newEndTimeInput.type = "time";
  newEndTimeInput.value = activites.result.endTime;
  updateForm.appendChild(newEndTimeInput);

  const updateButton = document.createElement("button");
  updateButton.textContent = "Update Activity";
  updateButton.addEventListener("click", () => {
    handleUpdate(
      currentActivitesId,
      newNameInput.value,
      newDescriptionInput.value,
      newActivityTypeInput.value,
      newDateInput.value,
      newStartTimeInput.value,
      newEndTimeInput.value
    );
  });

  updateForm.appendChild(updateButton);

  responseDiv.appendChild(updateForm);

  const deleteButton = document.createElement("button");
  deleteButton.textContent = "Delete";
  deleteButton.addEventListener(
    "click",
    () => handleDelete(currentActivitesId) // Pass currentActivitesId here
  );
  responseDiv.appendChild(deleteButton);
}

function getUpcomingActivities() {
    fetch("http://localhost:5254/Activities/upcoming-activities")
      .then((response) => {
        if (!response.ok) {
          throw new Error("Error fetching upcoming activities");
        }
        return response.json();
      })
      .then((data) => {
        displayUpcomingActivities(data);
      })
      .catch((error) => {
        console.error("Error:", error);
        // Handle error scenarios here
      });
 }
  

function displayUpcomingActivities(activities) {
    const activitiesList = document.getElementById("activitiesList");
    activitiesList.innerHTML = "";
  
    activities.forEach((activity) => {
      const listItem = document.createElement("li");
      listItem.textContent = `${activities.name} - ${activities.date}`; // Display relevant details
      activitiesList.appendChild(listItem);
    });
}

function getActivitiesByType(type) {
    fetch(`http://localhost:5254/Activities/activities-by-type?type=${type}`)
      .then((response) => {
        if (!response.ok) {
          throw new Error("Error fetching activities by type");
        }
        return response.json();
      })
      .then((data) => {
        displayActivitiesByType(data);
      })
      .catch((error) => {
        console.error("Error:", error);
        // Handle error scenarios here
      });
}

function displayActivitiesByType(activites) {
    const activitiesList = document.getElementById("activitesbytypeList");
    activitiesList.innerHTML = ""; // Clear previous content
  
    const table = document.createElement("table");
    table.classList.add("activity-table");
  
    const headerRow = document.createElement("tr");
    const nameHeader = document.createElement("th");
    nameHeader.textContent = "Activity Name";
    headerRow.appendChild(nameHeader);
  
    const dateHeader = document.createElement("th");
    dateHeader.textContent = "Date";
    headerRow.appendChild(dateHeader);
  
    table.appendChild(headerRow);
  
    activites.forEach((activites) => {
      const row = document.createElement("tr");
  
      const nameCell = document.createElement("td");
      nameCell.textContent = activites.name;
      row.appendChild(nameCell);
  
      const dateCell = document.createElement("td");
      dateCell.textContent = activites.date; // Replace 'date' with the actual date property
      row.appendChild(dateCell);
  
      table.appendChild(row);
    });
  
    activitiesList.appendChild(table);
  }
  

// Call the function
document.addEventListener("DOMContentLoaded", function () {addNewActivity();});
