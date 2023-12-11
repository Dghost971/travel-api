document
  .getElementById("buttonallvoyage")
  .addEventListener("click", getAllVoyages);

function getAllVoyages() {
  fetch(`http://localhost:5254/Voyages/get-all-voyages`)
    .then((response) => response.json())
    .then((data) => {
      displayAllvoyages(data);
    })
    .catch((error) => {
      console.error("Error fetching voyages:", error);
    });
}

function displayAllvoyages(voyages) {
  const dataList = document.getElementById("voyageList");
  dataList.innerHTML = "";

  if (voyages && Array.isArray(voyages)) {
    voyages.forEach((voyage) => {
      const listItem = document.createElement("li");
      listItem.innerHTML = `
      <p><strong>Name:</strong> ${voyage.name}</p>
      <p><strong>Destination:</strong> ${voyage.destinationcountry}</p>
      <p><strong>Start Date:</strong> ${voyage.startdate}</p>
      <p><strong>End Date:</strong> ${voyage.enddate}</p>
      `;
      dataList.appendChild(listItem);
    });
  } else {
    console.error("Invalid or empty voyages data.");
  }
}

document.addEventListener("DOMContentLoaded", function () {
  fetch("http://localhost:5254/Destinations/get-all-destination") // Replace with your API endpoint
    .then((response) => {
      if (!response.ok) {
        throw new Error("Failed to fetch destination countries");
      }
      return response.json();
    })
    .then((data) => {
      const destinationCountrySelect =
        document.getElementById("destinationCountry");
      data.forEach((destination) => {
        const option = document.createElement("option");
        option.value = destination.country; // Assuming 'country' is the field containing the country name
        option.textContent = destination.country;
        destinationCountrySelect.appendChild(option);
      });
    })
    .catch((error) => {
      console.error("Error fetching destination countries:", error);
    });
});
