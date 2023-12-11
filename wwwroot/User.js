document.getElementById("Buttonalluser").addEventListener("click", getAllUsers);


function getAllUsers() {
  fetch(`http://localhost:5254/Users/get-all-user`)
    .then((response) => response.json())
    .then((data) => {
        displayAllusers(data);
    })
    .catch((error) => {
      console.error("Error fetching userss:", error);
    });
}

function displayAllusers(users) {
    const dataList = document.getElementById("userList");
    dataList.innerHTML = "";
  
    users.forEach((user) => {
      const listItem = document.createElement("li");
      listItem.innerHTML = `
        <p><strong>Username:</strong> ${user.username}</p>
        <p><strong>Email:</strong> ${user.email}</p>
        <p><strong>Phone:</strong> ${user.phonenumber}</p>
        <p><strong>Registration date:</strong> ${user.registrationdate}</p>
      `;
      dataList.appendChild(listItem);
    });
  }
  