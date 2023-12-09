document
  .getElementById("fetchButton")
  .addEventListener("click", getAllActivityTypes);

function getAllActivityTypes() {
  fetch(`http://localhost:5254/ActivityType/get-all-activitytype`)
    .then((response) => response.json())
    .then((data) => {
      displayActivityTypes(data.result);
    })
    .catch((error) => {
      console.error("Error fetching activity types:", error);
    });
}

function displayActivityTypes(activityTypes) {
  const dataList = document.getElementById("activityTypeList");
  dataList.innerHTML = "";

  activityTypes.forEach((activityType) => {
    const listItem = document.createElement("li");
    listItem.textContent = activityType.name;
    dataList.appendChild(listItem);
  });
}

let currentActivityTypeId = null;
let currentActivityName = null;

function getActivityType() {
  const activityTypeId = document.getElementById("activityTypeId").value;
  fetch(
    `http://localhost:5254/ActivityType/get-activitytype-by-id/${activityTypeId}`
  )
    .then((response) => {
      if (!response.ok) {
        throw new Error("Error fetching activity type");
      }
      return response.json();
    })
    .then((data) => {
      displayActivityType(data);
      currentActivityTypeId = data.result.id; // Store the retrieved ID
      currentActivityName = data.result.name; // Store the retrieved name
    })
    .catch((error) => {
      console.error("Error fetching activity type:", error);
      document.getElementById("activityTypeResponse").innerText =
        "Error fetching activity type";
    });
}

function handleUpdate(currentActivityTypeId, newName) {
  if (!currentActivityTypeId) {
    console.error("No activity type selected");
    return;
  }

  if (!newName) {
    console.error("Please enter a new name");
    return;
  }

  fetch(
    `http://localhost:5254/ActivityType/update-activitytype/${currentActivityTypeId}`,
    {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({ name: newName }),
    }
  )
    .then((response) => {
      if (!response.ok) {
        throw new Error("Error updating activity type");
      }
      console.log("Activity type updated successfully");
      // Handle success scenarios here
    })
    .catch((error) => {
      console.error("Error updating activity type:", error);
      // Handle error scenarios here
    });
}

function handleDelete(currentActivityTypeId) {
  if (!currentActivityTypeId) {
    console.error("No activity type selected");
    return;
  }

  fetch(
    `http://localhost:5254/ActivityType/delete-activitytype/${currentActivityTypeId}`,
    {
      method: "DELETE",
    }
  )
    .then((response) => {
      if (!response.ok) {
        throw new Error("Error deleting activity type");
      }
      console.log("Activity type deleted successfully");
      // Handle success scenarios here
    })
    .catch((error) => {
      console.error("Error deleting activity type:", error);
      // Handle error scenarios here
    });
}

function displayActivityType(activityType) {
  const responseDiv = document.getElementById("activityTypeResponse");
  responseDiv.innerHTML = ""; // Clear previous data

  const activityTypeDetails = document.createElement("div");
  activityTypeDetails.innerHTML = `
    <h3>Activity Type Details:</h3>
    <p>Name: ${activityType.result.name}</p>
  `;
  responseDiv.appendChild(activityTypeDetails);

  const updateInput = document.createElement("input");
  updateInput.type = "text";
  updateInput.placeholder = "New Activity Type Name";
  responseDiv.appendChild(updateInput);

  const updateButton = document.createElement("button");
  updateButton.textContent = "Update";
  updateButton.addEventListener(
    "click",
    () => handleUpdate(currentActivityTypeId, updateInput.value) // Pass currentActivityTypeId here
  );
  responseDiv.appendChild(updateButton);

  const deleteButton = document.createElement("button");
  deleteButton.textContent = "Delete";
  deleteButton.addEventListener(
    "click",
    () => handleDelete(currentActivityTypeId) // Pass currentActivityTypeId here
  );
  responseDiv.appendChild(deleteButton);
}

function createActivityType() {
  const createActivityTypeForm = document.getElementById(
    "createActivityTypeForm"
  );
  const activityTypeNameInput = document.getElementById("activityTypeName");
  const activityTypeResponse = document.getElementById("activityTypeResponse");

  createActivityTypeForm.addEventListener("submit", function (event) {
    event.preventDefault();

    const activityTypeName = activityTypeNameInput.value.trim();

    fetch("http://localhost:5254/ActivityType/create-new-activitytype", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        name: activityTypeName,
      }),
    })
      .then((response) => {
        if (response.ok) {
          activityTypeNameInput.style.border = "2px solid green";
        } else {
          activityTypeNameInput.style.border = "2px solid red";
        }
        return response.json();
      })
      .then((data) => {
        if (data.isSuccess) {
          activityTypeResponse.textContent =
            "Activity Type created successfully.";
        } else {
          activityTypeResponse.textContent = `Error: ${data.errorMessage}`;
        }
      })
      .catch((error) => {
        activityTypeResponse.textContent = `Error: ${error.message}`;
      });
  });
}

function getRelatedActivities() {
  const activityTypeId = document.getElementById("activityTypeId").value;
  fetch(
    `http://localhost:5254/ActivityType/related-activities/${activityTypeId}`
  )
    .then((response) => {
      if (!response.ok) {
        throw new Error("Error fetching related activities");
      }
      return response.json();
    })
    .then((data) => {
      displayRelatedActivities(data);
    })
    .catch((error) => {
      console.error("Error fetching related activities:", error);
      document.getElementById("relatedActivityList").innerText =
        "Error fetching related activities";
    });
}

function displayRelatedActivities(relatedActivities) {
  const relatedActivityList = document.getElementById("relatedActivityList");
  relatedActivityList.innerHTML = ""; // Clear previous data

  if (!relatedActivities || relatedActivities.length === 0) {
    const noActivities = document.createElement("li");
    noActivities.textContent = "No activities related";
    relatedActivityList.appendChild(noActivities);
  } else {
    relatedActivities.forEach((activity) => {
      const listItem = document.createElement("li");
      listItem.textContent = `Name: ${activity.name}`;
      relatedActivityList.appendChild(listItem);
    });
  }
}

function getActivityStats() {
  const activityTypeId = document.getElementById("activityTypeId").value;
  fetch(
    `http://localhost:5254/ActivityType/stats/${activityTypeId}`
  )
    .then((response) => {
      if (!response.ok) {
        throw new Error("Error fetching activity type statistics");
      }
      return response.json();
    })
    .then((data) => {
      displayActivityStats(data);
    })
    .catch((error) => {
      console.error("Error fetching activity type statistics:", error);
      document.getElementById("activityStats").innerText =
        "Error fetching activity type statistics";
    });
}

function displayActivityStats(stats) {
  const statsDiv = document.getElementById("activityStats");
  statsDiv.innerHTML = ""; // Clear previous data

  const statsDetails = document.createElement("div");
  statsDetails.innerHTML = `
    <h3>Activity Type Statistics:</h3>
    <p>Number of Activities: ${stats.result.totalActivities}</p>
  `;
  statsDiv.appendChild(statsDetails);
}

// Call the function when the DOM content is loaded
document.addEventListener("DOMContentLoaded", function () {
  createActivityType();
});
