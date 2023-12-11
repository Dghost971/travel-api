document
  .getElementById("ButtonAllDesti")
  .addEventListener("click", getAllDestination);

function getAllDestination() {
  fetch(`http://localhost:5254/Destinations/get-all-destination`)
    .then((response) => response.json())
    .then((data) => {
      displayAllDestinations(data);
    })
    .catch((error) => {
      console.error("Error fetching Destinations:", error);
    });
}

function displayAllDestinations(destinations) {
  const dataList = document.getElementById("destiList");
  dataList.innerHTML = "";

  if (!destinations || destinations.length === 0) {
    console.log("Invalid or empty destination data.");
    return;
  }

  destinations.forEach((destination) => {
    const listItem = document.createElement("li");
    listItem.textContent = destination.country;
    dataList.appendChild(listItem);
  });
}

let currentDestinationId;

function getDestination() {
  const DestinationId = document.getElementById("destinationId").value;
  fetch(
    `http://localhost:5254/Destinations/get-destination-by-id/${DestinationId}`
  )
    .then((response) => {
      if (!response.ok) {
        throw new Error("Error fetching activity type");
      }
      return response.json();
    })
    .then((data) => {
      displayDestination(data);
      currentDestinationId = data.id; // Store the retrieved ID
    })
    .catch((error) => {
      console.error("Error fetching destination:", error);
      document.getElementById("destinationResponse").innerText =
        "Error fetching activity type";
    });
}

function handleUpdate(currentDestinationId, newName) {
  if (!currentDestinationId) {
    console.error("No activity type selected");
    return;
  }

  if (!newName) {
    console.error("Please enter a new name");
    return;
  }

  fetch(
    `http://localhost:5254/Destinations/update-destination/${currentDestinationId}`,
    {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({ country: newName }),
    }
  )
    .then((response) => {
      if (!response.ok) {
        throw new Error("Error updating destination");
      }
      console.log("destination  updated successfully");
      // Handle success scenarios here
    })
    .catch((error) => {
      console.error("Error updating destination:", error);
      // Handle error scenarios here
    });
}

function handleDelete(currentDestinationId) {
  if (!currentDestinationId) {
    console.error("No activity type selected");
    return;
  }

  fetch(
    `http://localhost:5254/Destinations/delete-destination/${currentDestinationId}`,
    {
      method: "DELETE",
    }
  )
    .then((response) => {
      if (!response.ok) {
        throw new Error("Error deleting destination");
      }
      console.log("destination deleted successfully");
      // Handle success scenarios here
    })
    .catch((error) => {
      console.error("Error deleting destination:", error);
      // Handle error scenarios here
    });
}

function displayDestination(destination) {
  const responseDiv = document.getElementById("destinationResponse");
  responseDiv.innerHTML = ""; // Clear previous data

  const destinationDetails = document.createElement("div");
  destinationDetails.innerHTML = `
      <h3>Destination Details:</h3>
      <p>Couuntry: ${destination.country}</p>
    `;
  responseDiv.appendChild(destinationDetails);

  const updateInput = document.createElement("input");
  updateInput.type = "text";
  updateInput.placeholder = "Update Country";
  responseDiv.appendChild(updateInput);

  const updateButton = document.createElement("button");
  updateButton.textContent = "Update";
  updateButton.addEventListener(
    "click",
    () => handleUpdate(currentDestinationId, updateInput.value) // Pass currentDestinationId here
  );
  responseDiv.appendChild(updateButton);

  const deleteButton = document.createElement("button");
  deleteButton.textContent = "Delete";
  deleteButton.addEventListener(
    "click",
    () => handleDelete(currentDestinationId) // Pass currentDestinationId here
  );
  responseDiv.appendChild(deleteButton);
}

function createDestination() {
  const destinationCountryInput = document.getElementById(
    "newdestinationCountry"
  );
  const destinationResponse = document.getElementById("destinationResponse");

  const destinationCountry = destinationCountryInput.value.trim();

  fetch("http://localhost:5254/Destinations/create-new-destination", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      country: destinationCountry,
    }),
  })
    .then((response) => {
      if (response.ok) {
        destinationCountryInput.style.border = "2px solid green";
      } else {
        destinationCountryInput.style.border = "2px solid red";
      }
      return response.json();
    })
    .then((data) => {
      if (data.isSuccess) {
        destinationResponse.textContent = "Destination created successfully.";
      } else {
        destinationResponse.textContent = `Error: ${data.errorMessage}`;
      }
    })
    .catch((error) => {
      destinationResponse.textContent = `Error: ${error.message}`;
    });
}

function getPopularActivitiesForDestination() {
  const destinationIdInput = document.getElementById("popularactivitesdestinationId");
  const popularActivitiesList = document.getElementById("popularactivitesList");

  const destinationId = destinationIdInput.value;

  fetch(
    `http://localhost:5254/Destinations/popular-activities/${destinationId}`
  )
    .then((response) => response.json())
    .then((data) => {
      popularActivitiesList.innerHTML = "";
      data.forEach((activity) => {
        const listItem = document.createElement("li");
        listItem.textContent = activity.name;
        popularActivitiesList.appendChild(listItem);
      });
    })
    .catch((error) => {
      popularActivitiesList.innerHTML = `<li>Error fetching activities: ${error.message}</li>`;
    });
}
