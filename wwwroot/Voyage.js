document.addEventListener('DOMContentLoaded', function () {
    fetch('http://localhost:5254/Destinations/get-all-destination') // Replace with your API endpoint
      .then(response => {
        if (!response.ok) {
          throw new Error('Failed to fetch destination countries');
        }
        return response.json();
      })
      .then(data => {
        const destinationCountrySelect = document.getElementById('destinationCountry');
        data.forEach(destination => {
          const option = document.createElement('option');
          option.value = destination.country; // Assuming 'country' is the field containing the country name
          option.textContent = destination.country;
          destinationCountrySelect.appendChild(option);
        });
      })
      .catch(error => {
        console.error('Error fetching destination countries:', error);
      });
  });