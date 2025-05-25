window.onload = function() {
    let table = document.getElementById("table");
    let saveButton = document.getElementById("saveButton");
    let savedChanges = true;

    function loadTable() {
        let xmlhttp = new XMLHttpRequest();
        xmlhttp.onreadystatechange = function() {
            if (this.readyState == 4 && this.status == 200) {
                let response = JSON.parse(this.responseText);
                table.innerHTML = "<tr><th>ID</th><th>Title</th><th>Platform</th><th>Genre</th><th>Publisher</th><th>Release Date</th><th>Stock</th><th>Price $</th></tr>";
                for (let i = 0; i < response.length; i++) {
                    let row = table.insertRow();
                    row.className = "table-row";
                    row.insertCell(0).innerText = response[i].id; 
                    row.insertCell(1).innerText = response[i].title;
                    row.insertCell(2).innerText = response[i].platform;
                    row.insertCell(3).innerText = response[i].genre;
                    row.insertCell(4).innerText = response[i].publisher;
                    row.insertCell(5).innerText = response[i].release_date;
                    row.insertCell(6).innerText = response[i].stock;
                    row.insertCell(7).innerText = response[i].price;
                }
            }
        };
        xmlhttp.open("GET", "get_data.php", true);
        xmlhttp.send();
    }

    loadTable();

    function saveData() {
        let form = document.getElementById('formular');
        let xmlhttp = new XMLHttpRequest();
        xmlhttp.onreadystatechange = function() {
            if (this.readyState == 4 && this.status == 200) {
                loadTable();
                savedChanges = true;
            }
        };
        xmlhttp.open("PUT", "save_data.php", true);
        xmlhttp.setRequestHeader("Content-type", "application/json");
        xmlhttp.send(JSON.stringify({
            id: form.elements['id'].value,
            title: form.elements['title'].value,
            platform: form.elements['platform'].value,
            genre: form.elements['genre'].value,
            publisher: form.elements['publisher'].value,
            release_date: form.elements['release Date'].value,
            stock: form.elements['stock'].value,
            price: form.elements['price'].value
        }));
    }

    table.addEventListener('click', function(event) {
        let targetRow = event.target.closest('.table-row');
        if (targetRow) {

            if( targetRow.getElementsByTagName('td')[0].innerHTML == document.getElementById('formular').elements['id'].value) 
                return; 

            if(savedChanges == false) {
                if (!confirm("You have unsaved changes. Do you want to continue?")) { 
                   return;
                }
            }

            let cells = targetRow.getElementsByTagName('td');
            let form = document.getElementById('formular');
            if (form && cells.length >= 8) {
                form.elements['id'].value = cells[0].innerText;
                form.elements['title'].value = cells[1].innerText;
                form.elements['platform'].value = cells[2].innerText;
                form.elements['genre'].value = cells[3].innerText;
                form.elements['publisher'].value = cells[4].innerText;
                form.elements['release Date'].value = cells[5].innerText;
                form.elements['stock'].value = cells[6].innerText;
                form.elements['price'].value = cells[7].innerText;
            }

            saveButton.disabled = true;
            savedChanges = true;
        }
    });

    let inputs = document.querySelectorAll('#formular input');
    inputs.forEach(input => {
        input.addEventListener('input', function() {
            let hasContent = Array.from(inputs).some(input => input.value.trim() !== '');
            saveButton.disabled = !hasContent;
            savedChanges = false; 
        });
    });

    saveButton.addEventListener('click', function() {
        saveData();
        saveButton.disabled = true;
        alert("Data saved successfully!");
    });
}
