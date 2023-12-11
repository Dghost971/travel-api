document.getElementById("Buttonallcom").addEventListener("click", getAllComments);

function getAllComments() {
  fetch(`http://localhost:5254/Comments/get-all-comment`)
    .then((response) => response.json())
    .then((data) => {
        displayAllComments(data);
    })
    .catch((error) => {
      console.error("Error fetching Comments:", error);
    });
}

function displayAllComments(comments) {
    const dataList = document.getElementById("commentsList");
    dataList.innerHTML = "";
  
    comments.forEach((comment) => {
        const listItem = document.createElement("li");
        listItem.innerHTML = `
            <p><strong>Content:</strong> ${comment.content}</p>
            <p><strong>User:</strong> ${budget.username}</p>
            <p><strong>Date:</strong> ${budget.date}</p>
          `;
        dataList.appendChild(listItem);
    });
  }